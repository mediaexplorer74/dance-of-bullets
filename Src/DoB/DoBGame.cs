// Decompiled with JetBrains decompiler
// Type: DoB.DoBGame
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Components;
using DoB.Extensions;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xaml;

#nullable disable
namespace DoB
{
  public class DoBGame : Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SpriteFont font;
    private RenderTarget2D renderTarget;
    private RenderTarget2D renderTargetTmp;
    public List<GameObject> Objects = new List<GameObject>();
    public List<IComponent> ShmupComponents = new List<IComponent>();
    public Player Player;
    private int debug_collChecksPeak;
    private bool debug_showBulletPaths;
    private Cooldown nextStageDelayCooldown = new Cooldown();
    private bool isStageEnding;
    private Cooldown stageTransitionEffectCooldown = new Cooldown();
    private Texture2D stageTransitionEffect;
    private Config cfg;
    private List<Texture2D> bgTextures;
    private List<double> bgX;
    private DateTime debug_lastSkipTime = DateTime.MinValue;

    public Rectangle GameplayRectangle { get; set; }

    public Rectangle DrawingRectangle { get; set; }

    public int StageIndex { get; set; }

    public Stages Stages { get; set; }

    public Stage CurrentStage
    {
      get
      {
        return this.StageIndex < 0 || this.StageIndex >= this.Stages.Count ? (Stage) null : this.Stages[this.StageIndex];
      }
    }

    public DoBGame()
    {
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.Content.RootDirectory = "Content";
      this.cfg = (Config) XamlServices.Parse(File.ReadAllText("Config.xaml"));
      this.GameplayRectangle = new Rectangle(0, 0, 1280, 720);
      this.DrawingRectangle = new Rectangle(0, 0, (int) this.cfg["ResolutionW"], (int) this.cfg["ResolutionH"]);
      this.graphics.PreferredBackBufferWidth = this.DrawingRectangle.Width;
      this.graphics.PreferredBackBufferHeight = this.DrawingRectangle.Height;
      this.graphics.IsFullScreen = (bool) this.cfg["IsFullScreen"];
      this.graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
      GameObject.Game = this;
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      this.InitializeRenderTarget();
      this.renderTargetTmp = new RenderTarget2D(this.GraphicsDevice, this.DrawingRectangle.Width, this.DrawingRectangle.Height);
      this.font = this.Content.Load<SpriteFont>("SpriteFont1");
      this.stageTransitionEffect = this.Content.Load<Texture2D>("white");
      this.Stages = (Stages) XamlServices.Parse(File.ReadAllText((string) this.cfg["StageDataFile"]));
      this.StageIndex = 0;
      this.ShmupComponents.Add((IComponent) this.Stages[this.StageIndex]);
      this.LoadStageTextures();
      Player player = new Player();
      player.X = 300.0;
      player.Y = 360.0;
      this.Player = player;
      this.Objects.Add((GameObject) this.Player);
    }

    private void LoadStageTextures()
    {
      if (this.Stages[this.StageIndex].BackgroundTextures != null)
      {
        this.bgTextures = new List<Texture2D>();
        this.bgX = new List<double>();
        for (int index = 0; index < this.Stages[this.StageIndex].BackgroundTextureArray.Length; ++index)
        {
          this.bgTextures.Add(this.Content.Load<Texture2D>(this.Stages[this.StageIndex].BackgroundTextureArray[index]));
          this.bgX.Add(0.0);
        }
      }
      else
      {
        this.bgTextures = new List<Texture2D>()
        {
          this.Content.Load<Texture2D>(this.Stages[this.StageIndex].BackgroundTexture)
        };
        this.bgX = new List<double>() { 0.0 };
      }
    }

    private void InitializeRenderTarget()
    {
      if (!this.debug_showBulletPaths)
        this.renderTarget = new RenderTarget2D(this.GraphicsDevice, this.GameplayRectangle.Width, this.GameplayRectangle.Height);
      else
        this.renderTarget = new RenderTarget2D(this.GraphicsDevice, this.GameplayRectangle.Width, this.GameplayRectangle.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
    }

    internal void Debug_ToggleShowBulletPaths()
    {
      this.debug_showBulletPaths = !this.debug_showBulletPaths;
      this.InitializeRenderTarget();
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      KeyboardState state;
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back != ButtonState.Pressed)
      {
        state = Keyboard.GetState();
        if (!state.IsKeyDown(Keys.Escape))
          goto label_3;
      }
      this.Exit();
label_3:
      state = Keyboard.GetState();
      if (state.IsKeyDown(Keys.LeftAlt))
      {
        state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Enter))
        {
          this.graphics.IsFullScreen = !this.graphics.IsFullScreen;
          this.graphics.ApplyChanges();
        }
      }
      this.Objects.RemoveAll((Predicate<GameObject>) (c => c.IsMarkedForRemoval));
      for (int index = 0; index < this.ShmupComponents.Count; ++index)
        this.ShmupComponents[index].Update(gameTime);
      for (int index = 0; index < this.Objects.Count; ++index)
        this.Objects[index].Update(gameTime);
      int val1 = 0;
      for (int index1 = 0; index1 < this.Objects.Count; ++index1)
      {
        if (this.Objects[index1] is Collideable && ((Collideable) this.Objects[index1]).IsFriendly)
        {
          Collideable c1 = (Collideable) this.Objects[index1];
          for (int index2 = 0; index2 < this.Objects.Count; ++index2)
          {
            if (this.Objects[index2] is Collideable && !((Collideable) this.Objects[index2]).IsFriendly)
            {
              Collideable c2 = (Collideable) this.Objects[index2];
              ++val1;
              if ((double) Vector2.Distance(new Vector2((float) c1.X, (float) c1.Y), new Vector2((float) c2.X, (float) c2.Y)) < c1.CollisionRadius + c2.CollisionRadius)
              {
                c1.CollideWith(c2);
                c2.CollideWith(c1);
              }
            }
          }
        }
      }
      this.debug_collChecksPeak = Math.Max(val1, this.debug_collChecksPeak);
      if (!this.isStageEnding && this.Stages[this.StageIndex].IsEnded)
        this.EndStage();
      this.nextStageDelayCooldown.Update(gameTime.ElapsedMs());
      if (this.isStageEnding && this.nextStageDelayCooldown.IsElapsed)
        this.NextStage();
      this.stageTransitionEffectCooldown.Update(gameTime.ElapsedMs());
      base.Update(gameTime);
    }

    private void EndStage()
    {
      EventBroker.IsEnabled = false;
      this.ShmupComponents.Remove((IComponent) this.Stages[this.StageIndex]);
      this.ClearHostileObjects<Collideable>();
      this.isStageEnding = true;
      this.nextStageDelayCooldown.Reset(2000.0);
    }

    private void NextStage()
    {
      if (++this.StageIndex >= this.Stages.Count)
        return;
      EventBroker.IsEnabled = true;
      this.ShmupComponents.Add((IComponent) this.Stages[this.StageIndex]);
      this.LoadStageTextures();
      this.stageTransitionEffectCooldown.Reset(5000.0);
      this.isStageEnding = false;
    }

    protected override void Draw(GameTime gameTime)
    {
      this.DrawGameToTexture(gameTime);
      this.spriteBatch.Begin();
      this.spriteBatch.Draw((Texture2D) this.renderTarget, this.DrawingRectangle, Color.White);
      this.spriteBatch.End();
      base.Draw(gameTime);
    }

    private void DrawGameToTexture(GameTime gameTime)
    {
      this.GraphicsDevice.SetRenderTarget(this.renderTarget);
      this.spriteBatch.Begin();
      if (!this.debug_showBulletPaths)
      {
        for (int index = 0; index < this.bgTextures.Count; ++index)
        {
          double x = this.bgX[index];
          this.DrawBackground(gameTime, this.bgTextures[index], (double) (-600 - index * 150), 1601, ref x);
          this.bgX[index] = x;
        }
      }
      for (int index = 0; index < this.Objects.Count; ++index)
        this.Objects[index].Draw(gameTime, this.spriteBatch);
      this.spriteBatch.DrawString(this.font, string.Format("Health: {0} | Multiplier: {1}x | Score: {2}", (object) this.Player.Health.Amount, (object) GameSpeedManager.DifficultyMultiplier.ToString("N2", (IFormatProvider) CultureInfo.InvariantCulture), (object) ScoreKeeper.Score.ToString("N0", (IFormatProvider) CultureInfo.InvariantCulture)), new Vector2(650f, 6f), Color.White);
      if (!this.stageTransitionEffectCooldown.IsElapsed)
        this.spriteBatch.Draw(this.stageTransitionEffect, this.GameplayRectangle, Color.FromNonPremultiplied((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) ((double) byte.MaxValue * (1.0 - (double) this.stageTransitionEffectCooldown.Progress))));
      this.spriteBatch.End();
      this.GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
    }

    private void DrawBackground(
      GameTime gameTime,
      Texture2D bgTexture,
      double v,
      int textureWidth,
      ref double x,
      int y = 0)
    {
      x += GameSpeedManager.ApplySpeed(v, gameTime.ElapsedGameTime.TotalMilliseconds);
      if (x < (double) -textureWidth)
        x = 0.0;
      Color color = this.Player.IsPaybackActive ? Color.FromNonPremultiplied((int) byte.MaxValue, 50, (int) byte.MaxValue, (int) byte.MaxValue) : (this.Player.IsManaActive ? Color.FromNonPremultiplied(50, 50, (int) byte.MaxValue, (int) byte.MaxValue) : Color.White);
      this.spriteBatch.Draw(bgTexture, new Rectangle((int) x, y, textureWidth, 768), color);
      this.spriteBatch.Draw(bgTexture, new Rectangle(x < 0.0 ? (int) (x + (double) textureWidth) : (int) (x - (double) textureWidth), y, textureWidth, 768), color);
    }

    internal void Debug_SkipStage()
    {
      if ((DateTime.Now - this.debug_lastSkipTime).TotalSeconds <= 0.5)
        return;
      this.debug_lastSkipTime = DateTime.Now;
      this.EndStage();
    }

    public void ClearBullets() => this.ClearHostileObjects<Bullet>();

    internal void ClearHostileObjects<T>() where T : Collideable
    {
      foreach (T obj in this.Objects.Where<GameObject>((Func<GameObject, bool>) (o => o is T && !(o as T).IsFriendly)).ToList<GameObject>())
        obj.RemoveSelf();
    }
  }
}
