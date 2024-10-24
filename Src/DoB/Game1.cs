using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DoB.GameObjects;
using DoB.Drawers;
using DoB.Behaviors;
using DoB.Utility;
using System.IO;
//using System.Xaml;
using DoB.Xaml;
using DoB.Components;
using System.Globalization;
using DoB.Extensions;
using Windows.Data.Xml.Dom;
//using System.Diagnostics;


namespace DoB
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;
		public SpriteFont font;
		public RenderTarget2D renderTarget;
		public RenderTarget2D renderTargetTmp;

        private RenderTarget2D render;
        public Screen Screen;

        public static int TargetWidth;
        public static int TargetHeight;
        public static string LANGUAGE = "EN";
        public static float VOLUME_MUSIC = 1f;
        public static float VOLUME_SFX = 0.7f;
        public static bool CAN_PAUSE = true;
        public static bool IS_PAUSED = false;
        //public const int DEFAULT_WIDTH = 800;//400;//500;
        //public const int DEFAULT_HEIGHT = 1280;//640;//840;

        //RnD
        public float Scale = 0.8f;//1.75f;

        public List<GameObject> Objects = new List<GameObject>();
		public List<IComponent> ShmupComponents = new List<IComponent>();
		public Player Player = null;

        public static Game1 Instance { get; private set; }


        public Rectangle GameplayRectangle { get; set; }
		public Rectangle DrawingRectangle { get; set; }

		public int StageIndex { get; set; }
		public Stages Stages { get; set; }
		public Stage CurrentStage
		{
			get
			{
				return ( StageIndex >= 0 && StageIndex < Stages.Count ) ? Stages[StageIndex] : null;
			}
		}

		private int debug_collChecksPeak = 0;
		private bool debug_showBulletPaths = false;

		private Cooldown nextStageDelayCooldown = new Cooldown();
		private bool isStageEnding = false;

		private Cooldown stageTransitionEffectCooldown = new Cooldown();
		private Texture2D stageTransitionEffect;
		private Config cfg = new Config(); //

		public Game1()
		{
			//RnD: (Game)
			graphics = new GraphicsDeviceManager((Game)this);

            //RnD
            this.graphics.GraphicsProfile = GraphicsProfile.Reach;

			Content.RootDirectory = "Content";

			
            //RnD
            string xaml = File.ReadAllText("Config.xaml");

            //-----------------------------------------------------------------
            //cfg = (Config)XamlServices.Parse(xaml);
            var doc = new XmlDocument();
            doc.LoadXml(xaml);


            IXmlNode xmlContent = default;
            object Result = default;

            XmlNodeList tags = doc.GetElementsByTagName("x:String");

            if (tags.Count > 0)
            {
                xmlContent = tags.First();
				Result = xmlContent.InnerText;
                //bad1 - firstContent.Attributes.GetNamedItem("StageDataFile").InnerText;
                //bad2 - xmlContent.InnerText;

                cfg["StageDataFile"] = (object)Result;
            }


            tags = doc.GetElementsByTagName("x:Int32");			

            if (tags.Count > 0)
            {
				xmlContent = tags[0];//tags.First();
                Result = xmlContent.InnerText;

                cfg["ResolutionW"] = (object)Result;

                xmlContent = tags[1];
				Result = xmlContent.InnerText;

                cfg["ResolutionH"] = (object)Result;

            }

            tags = doc.GetElementsByTagName("x:Boolean");

            if (tags.Count > 0)
            {
                xmlContent = tags.First();
                Result = xmlContent.InnerText;

                cfg["IsFullScreen"] = (object)Result;
            }

            //Result:
            //cfg["StageDataFile"] = "StageData\\Stages.xaml";
            //cfg["ResolutionW"] = 640;
            //cfg["ResolutionH"] = 400;
            //cfg["IsFullScreen"] = false;

            //-----------------------------------------------------------------


            GameplayRectangle = new Rectangle(0, 0, 
				1280, 720); //the size of the gameplay area, regardless of the actually rendered resolution
           
			object W = cfg["ResolutionW"];
            object H = cfg["ResolutionH"];

            int W2 = 0;
            int H2 = 0;

            string W1 = (string)cfg["ResolutionW"];
            string H1 = (string)cfg["ResolutionH"];

            bool checkInt = Int32.TryParse(W1, out W2);
            checkInt = Int32.TryParse(H1, out H2);        

            DrawingRectangle = new Rectangle(0, 0, (int)W2, (int)H2);

			graphics.PreferredBackBufferWidth = DrawingRectangle.Width;
			graphics.PreferredBackBufferHeight = DrawingRectangle.Height;

			bool F2 = false;
            string F1 = (string)cfg["IsFullScreen"];
            checkInt = Boolean.TryParse(F1, out F2);
			graphics.IsFullScreen = (bool)F2;

            //graphics.ApplyChanges();

            //RnD
            Game1.Instance = this;
        }//Game1

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
		{
            PresentationParameters presentationParameters
                     = this.graphics.GraphicsDevice.PresentationParameters;

            GameObject.Game = this;

			//RnD
			Game1.TargetWidth = this.graphics.PreferredBackBufferWidth; //> 1400 ?
			//    this.graphics.PreferredBackBufferWidth - 500 :
			//    this.graphics.PreferredBackBufferWidth - 250;//800;//400;// 500;
			Game1.TargetHeight = this.graphics.PreferredBackBufferHeight;// > 1000 ?
            //    this.graphics.PreferredBackBufferHeight - 250 :
            //    this.graphics.PreferredBackBufferHeight;//1280;//640;//840;

            this.render = new RenderTarget2D(
                this.graphics.GraphicsDevice, Game1.TargetWidth, Game1.TargetHeight);
            
			//RnD
			//this.Screen = new Screen(this, Game1.TargetWidth, Game1.TargetHeight, this.Scale);

            base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch( GraphicsDevice );

			InitializeRenderTarget();
			renderTargetTmp = new RenderTarget2D( GraphicsDevice, 
				DrawingRectangle.Width, DrawingRectangle.Height );

			font = Content.Load<SpriteFont>( "SpriteFont1" );
			stageTransitionEffect = Content.Load<Texture2D>( "white" );

			//TODO

			string storagePath = (string)cfg["StageDataFile"];

            string xaml = File.ReadAllText(storagePath );

            //Stages = (Stages)XamlServices.Parse( xaml );

            //TEMP
            Stages = new Stages();

            List<IComponent> ComponentsList = new List<IComponent>();

			// segment 1
            EnemySpawner E1_1 = new EnemySpawner()
			{
				CooldownMs = (double)6000,
				Count = 4,
				ReferenceName = "Stage1RC-BEnemy1",
				X = (double)1380,
				Y = (double)50
			};

            EnemySpawner E2_1 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy2",
                X = (double)1380,
                Y = (double)360
            };

            EnemySpawner E3_1 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy3",
                X = (double)1380,
                Y = (double)670
            };

            EnemySpawner E4_1 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
				DelayMs = 26000,
                ReferenceName = "Stage1RC-ABEnemy1",
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E5_1 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                DelayMs = 26000,
                ReferenceName = "Stage1RC-ABEnemy2",
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E6_1 = new EnemySpawner()
            {
                Count = 1,
                DelayMs = 54000,
                ReferenceName = "Stage1RC-TopBoss",
                X = (double)1380,
                Y = (double)50
            };

            EnemySpawner E7_1 = new EnemySpawner()
            {
                Count = 1,
                DelayMs = 54000,
                ReferenceName = "Stage1RC-BottomBoss",
                X = (double)1380,
                Y = (double)670
            };


            ComponentsList.Add(new Segment() 
			{ 
				WaitForEvent = "", 
				Components = [E1_1, E2_1, E3_1, E4_1, E5_1, E6_1, E7_1], 
				DelayMs = default
			});

            // ++++++++++++++++++++++

            // segment 1
            EnemySpawner E1_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy1B",
                X = (double)1380,
                Y = (double)50
            };

            EnemySpawner E2_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy2B",
                X = (double)1380,
                Y = (double)360
            };

            EnemySpawner E3_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy3B",
                X = (double)1380,
                Y = (double)670
            };

            EnemySpawner E4_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                ReferenceName = "Stage1RC-ABEnemy1",
				DelayMs = 26000,
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E5_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                ReferenceName = "Stage1RC-ABEnemy2",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E6_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 2,
                ReferenceName = "Stage1RC-ABEnemy3",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E7_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 2,
                ReferenceName = "Stage1RC-ABEnemy4",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E8_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage1RC-RingsBoss",
                DelayMs = 54000,
                X = (double)1100,
                Y = (double)360
            };

            EnemySpawner E9_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage1RC-FroggerBoss",
                WaitForEvent = "Stage1RC-RingsBossDied",
                X = (double)1100,
                Y = (double)360
            };

            EnemySpawner E10_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage2ZEnemy4",
                WaitForEvent = "Stage1RC-FroggerBossDied",
                X = (double)1400,
                Y = (double)360
            };


            ComponentsList.Add(new Segment()
            {
                WaitForEvent = "Stage1RC-TopBossDied;Stage1RC-BottomBossDied",
                Components = [E1_2, E2_2, E3_2, E4_2, E5_2, E6_2, E7_2, E8_2, E9_2, E10_2],
                DelayMs = default
            });
            // ++++++++++++++++++++++
            // level 1
            Stage Stage1 = new Stage()
			{
                IsEnded = false,
                BackgroundTexture = default,
                BackgroundTextures = "Background_sil;Background_sil_buildings;Background_sil_foreground",
                BackgroundTextureArray = ["Background_sil","Background_sil_buildings","Background_sil_foreground"],
                EndsOnEvent = "Stage2BBossDied",
				Components = ComponentsList//[Components.Segment, Components.Segment]
            };

			
			Stages.Add(Stage1);

			
            // level 2
            Stage Stage2 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = default,
                BackgroundTextures = "Background_sil2;Background_sil_foreground2",
                BackgroundTextureArray = ["Background_sil2", "Background_sil_foreground2"],
                EndsOnEvent = "Stage2ZEnemy4Died",
                //Components = [DoB.Components.Segment, DoB.Components.Segment, DoB.Components.Segment]
            };

            Stages.Add(Stage2);

            // level 3
            Stage Stage3 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = "Background_castle",
                BackgroundTextures = default,
                BackgroundTextureArray = default,
                EndsOnEvent = "MiddleBossDied",
                //Components = [DoB.Components.EnemySpawner, DoB.Components.EnemySpawner, DoB.Components.EnemySpawner, DoB.Components.EnemySpawner...]
            };

            Stages.Add(Stage3);

            // level 4
            Stage Stage4 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = "sky",
                BackgroundTextures = default,
                BackgroundTextureArray = default,
                EndsOnEvent = "",
				//Components = []
            };

            Stages.Add(Stage4);
			

            //----------------------------------------------------------
            /* Goal: reconstruct XamlServices.Parse method

	        var doc = new XmlDocument();
            doc.LoadXml(xaml);


            IXmlNode xmlContent = default;
            object Result = default;

            XmlNodeList tags = doc.GetElementsByTagName("c:Stage");

            if (tags.Count > 0)
            {
				xmlContent = tags[0];//.First();

                XmlNodeList childnodes = xmlContent.ChildNodes;


                // +++++++++++++++++++++++++

                IXmlNode xmlContent1 = childnodes[0]; // First
                object Result1 = default;

                //XmlNodeList tags1 = xmlContent1.GetXml("c:EnemySpawner");

				//if (xmlContent1.Attributes.Count > 0)
				{
				//Debug.WriteLine(xmlContent1.ToString());
				}
                    // +++++++++++++++++++++++++


                    //bad1 - firstContent.Attributes.GetNamedItem("StageDataFile").InnerText;
                    //bad2 - xmlContent.InnerText;

                    //cfg["StageDataFile"] = (object)Result;
                }
			*/
            //----------------------------------------------------------

            StageIndex = 0;
			this.ShmupComponents.Add( Stages[StageIndex] );

			LoadStageTextures();

			Player = new Player { X = 300, Y = 360 };
			Objects.Add( Player );
		}

		private void LoadStageTextures()
		{
			if (Stages[StageIndex].BackgroundTextures != null)
			{
				bgTextures = new List<Texture2D>();
				bgX = new List<double>();
				for (int i = 0; i < Stages[StageIndex].BackgroundTextureArray.Length; i++)
				{
					bgTextures.Add
						(Content.Load<Texture2D>(Stages[StageIndex].BackgroundTextureArray[i]));
					
					bgX.Add(0);
				}
			}
			else
			{
				bgTextures = new List<Texture2D> 
				{ Content.Load<Texture2D>(Stages[StageIndex].BackgroundTexture) };
				
				bgX = new List<double> { 0 };
			}
		}

		private void InitializeRenderTarget()
		{
			if( !debug_showBulletPaths )
			{
				renderTarget = new RenderTarget2D( 
					GraphicsDevice, GameplayRectangle.Width, GameplayRectangle.Height );
			}
			else
			{
				renderTarget = new RenderTarget2D( GraphicsDevice,
					GameplayRectangle.Width, GameplayRectangle.Height,
					false, SurfaceFormat.Color, DepthFormat.None, 
					0, RenderTargetUsage.PreserveContents );
			}
		}

		internal void Debug_ToggleShowBulletPaths()
		{
			debug_showBulletPaths = !debug_showBulletPaths;
			InitializeRenderTarget();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			//
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>

		protected override void Update( GameTime gameTime )
		{
			// Allows the game to exit
			if( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState().IsKeyDown(Keys.Escape) )
				this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) 
				&& Keyboard.GetState().IsKeyDown(Keys.Enter))
			{
				graphics.IsFullScreen = !graphics.IsFullScreen;
				graphics.ApplyChanges();
			}
				

			Objects.RemoveAll( c => c.IsMarkedForRemoval );

			for( int i = 0; i < ShmupComponents.Count; i++ )
			{
				ShmupComponents[i].Update( gameTime );
			}

			for( int i = 0; i < Objects.Count; i++ )
			{
				Objects[i].Update( gameTime );
			}

			var debug_collChecks = 0;
			for( int i = 0; i < Objects.Count; i++ )
			{
				if( !( Objects[i] is Collideable ) || !( (Collideable)Objects[i] ).IsFriendly )
					continue;

				var ao = (Collideable)Objects[i];
				for( int j = 0; j < Objects.Count; j++ )
				{
					if( !( Objects[j] is Collideable ) || ( (Collideable)Objects[j] ).IsFriendly )
						continue;

					var bo = (Collideable)Objects[j];
					debug_collChecks++;
					if( Vector2.Distance( new Vector2( (float)ao.X, (float)ao.Y ), 
						new Vector2( (float)bo.X, (float)bo.Y ) ) 
						< ( (float)ao.CollisionRadius + (float)bo.CollisionRadius ) )
					{
						ao.CollideWith( bo );
						bo.CollideWith( ao );
					}
				}
			}

			debug_collChecksPeak = Math.Max( debug_collChecks, debug_collChecksPeak );

			if( !isStageEnding && Stages[StageIndex].IsEnded )
			{
				EndStage();
			}

			nextStageDelayCooldown.Update( gameTime.ElapsedMs() );
			if( isStageEnding && nextStageDelayCooldown.IsElapsed )
			{
				NextStage();
			}

			stageTransitionEffectCooldown.Update( gameTime.ElapsedMs() );

			base.Update( gameTime );
		}

		private void EndStage()
		{
			EventBroker.IsEnabled = false;
			ShmupComponents.Remove( Stages[StageIndex] );
			ClearHostileObjects<Collideable>();
			isStageEnding = true;
			nextStageDelayCooldown.Reset( 2000 );
		}

		private void NextStage()
		{
			if( ++StageIndex < Stages.Count )
			{
				EventBroker.IsEnabled = true;
				ShmupComponents.Add( Stages[StageIndex] );
				LoadStageTextures();
				stageTransitionEffectCooldown.Reset( 5000 );
				isStageEnding = false;
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw( GameTime gameTime )
		{
			DrawGameToTexture( gameTime );

			spriteBatch.Begin();
			spriteBatch.Draw( renderTarget, DrawingRectangle, Color.White );
			spriteBatch.End();

			base.Draw( gameTime );
		}

		private void DrawGameToTexture( GameTime gameTime )
		{
			GraphicsDevice.SetRenderTarget( renderTarget );
			spriteBatch.Begin();

			if (!debug_showBulletPaths)
			{
				for (int i = 0; i < bgTextures.Count; i++)
				{
					var x = bgX[i];
					DrawBackground(gameTime, bgTextures[i], -600 - i * 150, 1601, ref x);
					bgX[i] = x;
				}
			}

			for( int i = 0; i < Objects.Count; i++ )
			{
				Objects[i].Draw( gameTime, spriteBatch );
			}

			spriteBatch.DrawString( font,
				$"Health: {Player.Health.Amount} | Multiplier: {GameSpeedManager.DifficultyMultiplier.ToString("N2", CultureInfo.InvariantCulture)}x | Score: {ScoreKeeper.Score.ToString("N0", CultureInfo.InvariantCulture)}", 
				new Vector2( 650, 6 ), 
				Color.White );

			if ( !stageTransitionEffectCooldown.IsElapsed )
			{
				spriteBatch.Draw( stageTransitionEffect, GameplayRectangle, 
					Color.FromNonPremultiplied( 255, 255, 255,
					(int)( 255 * ( 1 - stageTransitionEffectCooldown.Progress ) ) ) );
			}

			spriteBatch.End();
			GraphicsDevice.SetRenderTarget( null );
		}

		List<Texture2D> bgTextures = null;
		List<double> bgX = null;

		private void DrawBackground( GameTime gameTime, Texture2D bgTexture,
			double v, int textureWidth, ref double x, int y = 0 )
		{
			x += (double)GameSpeedManager.ApplySpeed( v,
				gameTime.ElapsedGameTime.TotalMilliseconds );

			if( x < -textureWidth )
				x = 0;
			var color = Player.IsPaybackActive ? Color.FromNonPremultiplied( 255, 50, 255, 255 )
				: Player.IsManaActive ? Color.FromNonPremultiplied( 50, 50, 255, 255 ) : Color.White;
			spriteBatch.Draw( bgTexture, new Rectangle( (int)x, y, textureWidth, 768 ), color );
			spriteBatch.Draw( bgTexture, new Rectangle( (int)( x < 0 
				? x + textureWidth : x - textureWidth ), y, textureWidth, 768 ), color );
		}

		DateTime debug_lastSkipTime = DateTime.MinValue;
		internal void Debug_SkipStage()
		{
			if( ( DateTime.Now - debug_lastSkipTime ).TotalSeconds > 0.5 )
			{
				debug_lastSkipTime = DateTime.Now;
				EndStage();
			}
		}

		public void ClearBullets()
		{
			ClearHostileObjects<Bullet>();
		}


		internal void ClearHostileObjects<T>() where T:Collideable //It's internal so that debugcontrols can use it
		{
			foreach( T obj in Objects.Where( o => o is T && !( o as T ).IsFriendly ).ToList() )
			{
				obj.RemoveSelf();
			}
		}
	}
}
