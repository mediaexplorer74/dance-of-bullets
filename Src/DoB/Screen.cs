// Screen

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace DoB
{
    public class Screen
    {
        public Color ClearColor = Color.Black;
        public SamplerState SamplerState = SamplerState.PointClamp;
        public Vector2 Offset = Vector2.Zero;
        public Vector2 OffsetAdd = Vector2.Zero;
        private float scale = 0.8f;//0.8 - for L640; 1.75f; - for L950
        private float OrigScale = 1f;
        public Rectangle DrawRect;
        public Effect Effect;
        public float PadOffset;
        public Matrix Matrix;
        private Viewport viewport;
        private Rectangle screenRect;
        private int width;
        private int height;
        private Game1 mainGame;

        public GraphicsDeviceManager Graphics
        {
            get
            {
                return this.mainGame?.graphics;
            }
        }

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.mainGame?.GraphicsDevice;
            }
        }

        public RenderTarget2D RenderTarget { get; private set; }

        public Screen(Game1 game, int width, int height, float scale)
        {
            this.mainGame = game;
            this.screenRect = this.DrawRect = new Rectangle(0, 0, width, height);
            this.viewport = new Viewport();
            this.width = width;
            this.height = height;
            this.scale = this.OrigScale = scale;
            this.DrawRect.Width = this.viewport.Width =
                      (int)((double)this.screenRect.Width * (double)scale);
            this.DrawRect.Height = this.viewport.Height =
                      (int)((double)this.screenRect.Height * (double)scale * 2);
            this.SetWindowSize(this.DrawRect.Width, this.DrawRect.Height);
        }

        public void Initialize()
        {
            this.Dispose();
            this.RenderTarget = new RenderTarget2D(this.GraphicsDevice, 
                this.screenRect.Width, this.screenRect.Height);
        }

        public void Dispose()
        {
            if (this.RenderTarget == null)
                return;
            ((GraphicsResource)this.RenderTarget).Dispose();
        }

        public void Resize(int width, int height, float scale)
        {
            this.screenRect = this.DrawRect = new Rectangle(0, 0, width, height);
            this.viewport = new Viewport();
            this.width = width;
            this.height = height;
            this.scale = scale;
            this.DrawRect.Width = this.viewport.Width =
                      (int)((double)this.screenRect.Width * (double)scale);
            this.DrawRect.Height = this.viewport.Height =
                      (int)((double)this.screenRect.Height * (double)scale * 2);
            if (this.IsFullscreen)
            {
                this.Scale = Math.Min((float)this.GraphicsDevice.DisplayMode.Width
                    / (float)this.screenRect.Width,
                    (float)this.GraphicsDevice.DisplayMode.Height / (float)this.screenRect.Height);
                this.HandleFullscreenViewport();
            }
            else
                this.SetWindowSize(this.DrawRect.Width, this.DrawRect.Height);
            this.Initialize();
        }

        public void Render()
        {
            this.GraphicsDevice.Viewport = this.viewport;
            this.mainGame.spriteBatch.Begin((SpriteSortMode)2,
                BlendState.Opaque, this.SamplerState, DepthStencilState.None,
                RasterizerState.CullNone, this.Effect, new Matrix?());
            if (this.Effect != null)
                this.Effect.CurrentTechnique.Passes[0].Apply();
            Vector2 vector2 = Vector2.Add(this.Offset, this.OffsetAdd);
            if (Vector2.Equals(vector2, Vector2.Zero))
            {
                this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget, this.DrawRect,
                    new Rectangle?(this.screenRect), Color.White);
            }
            else
            {
                vector2.X = (float)(((double)vector2.X + 320.0) % 320.0);
                vector2.Y = (float)(((double)vector2.Y + 240.0) % 240.0);
                if ((double)vector2.X == 0.0)
                {
                    int num = (int)Math.Round((double)vector2.Y * (double)this.Scale,
                        MidpointRounding.AwayFromZero);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X, this.DrawRect.Y + num,
                        this.DrawRect.Width, this.DrawRect.Height), 
                        new Rectangle?(this.screenRect), Color.White);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X, this.DrawRect.Y + (num >= 0
                        ? num - (int)((double)this.Height * (double)this.Scale)
                        : num + (int)((double)this.Height * (double)this.Scale)),
                        this.DrawRect.Width, this.DrawRect.Height), 
                        new Rectangle?(this.screenRect), Color.White);
                }
                else if ((double)vector2.Y == 0.0)
                {
                    int num = (int)Math.Round((double)vector2.X * (double)this.Scale,
                        MidpointRounding.AwayFromZero);

                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + num, this.DrawRect.Y,
                        this.DrawRect.Width, this.DrawRect.Height), 
                        new Rectangle?(this.screenRect), Color.White);

                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + (num >= 0
                        ? num - (int)((double)this.Width * (double)this.Scale)
                        : num + (int)((double)this.Width * (double)this.Scale)),
                        this.DrawRect.Y, this.DrawRect.Width, this.DrawRect.Height),
                        new Rectangle?(this.screenRect), Color.White);
                }
                else
                {
                    int num1 = (int)Math.Round((double)vector2.X * (double)this.Scale,
                        MidpointRounding.AwayFromZero);
                    int num2 = (int)Math.Round((double)vector2.Y * (double)this.Scale,
                        MidpointRounding.AwayFromZero);
                    int num3 = num1 < 0 ? num1 + (int)((double)this.Width * (double)this.Scale)
                                  : num1 - (int)((double)this.Width * (double)this.Scale);
                    int num4 = num2 < 0 ? num2 + (int)((double)this.Height * (double)this.Scale)
                                  : num2 - (int)((double)this.Height * (double)this.Scale);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + num1, this.DrawRect.Y + num2,
                        this.DrawRect.Width, this.DrawRect.Height), new Rectangle?(this.screenRect), 
                        Color.White);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + num1, this.DrawRect.Y + num4,
                        this.DrawRect.Width, this.DrawRect.Height), new Rectangle?(this.screenRect),
                        Color.White);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + num3, this.DrawRect.Y + num2,
                        this.DrawRect.Width, this.DrawRect.Height), new Rectangle?(this.screenRect),
                        Color.White);
                    this.mainGame.spriteBatch.Draw((Texture2D)this.RenderTarget,
                        new Rectangle(this.DrawRect.X + num3, this.DrawRect.Y + num4,
                        this.DrawRect.Width, this.DrawRect.Height), new Rectangle?(this.screenRect), 
                        Color.White);
                }
            }
            this.mainGame.spriteBatch.End();
        }

        public float Scale
        {
            get => this.scale;
            set
            {
                if ((double)this.scale == (double)value)
                    return;
                this.scale = value;

                this.DrawRect.Width = this.viewport.Width =
                            (int)((double)this.screenRect.Width * (double)this.scale);

                this.DrawRect.Height = this.viewport.Height =
                            (int)((double)this.screenRect.Height * (double)this.scale * 2);

                if (this.IsFullscreen)
                    this.HandleFullscreenViewport();
                else
                    this.SetWindowSize(this.ScaledWidth, this.ScaledHeight);
            }
        }

        private void SetWindowSize(int width, int height)
        {
            this.Graphics.IsFullScreen = false;
            this.Graphics.PreferredBackBufferWidth = width;
            this.Graphics.PreferredBackBufferHeight = height;
            //this.Graphics.ApplyChanges();
            this.viewport.Width = width;
            this.viewport.Height = this.ScaledHeight;
            this.viewport.X = 0;
            this.viewport.Y = (height - this.ScaledHeight) / 2;
            this.DrawRect.X = width / 2 - this.ScaledWidth / 2;

            this.Matrix = Matrix.Multiply(Matrix.CreateScale(this.scale),
                Matrix.CreateTranslation((float)this.DrawRect.X, 0.0f, 0.0f));
        }

        public void HandleFullscreenViewport()
        {
            this.viewport.Width = this.GraphicsDevice.DisplayMode.Width;
            this.viewport.Height = this.ScaledHeight;
            this.viewport.X = 0;
            this.viewport.Y = (this.GraphicsDevice.DisplayMode.Height - this.ScaledHeight) / 2;
            this.DrawRect.X = this.viewport.Width / 2 - this.ScaledWidth / 2;
            this.Matrix = Matrix.Multiply(Matrix.CreateScale(this.scale),
                Matrix.CreateTranslation((float)this.DrawRect.X, 0.0f, 0.0f));
        }

        public void EnableFullscreen(Screen.FullscreenMode mode = Screen.FullscreenMode.LargestScale)
        {
            this.Graphics.IsFullScreen = true;
            this.Graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
            this.Graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
            this.Graphics.ApplyChanges();
            switch (mode)
            {
                case Screen.FullscreenMode.LargestScale:
                    this.Scale = Math.Min((float)this.GraphicsDevice.DisplayMode.Width
                        / (float)this.screenRect.Width, (float)this.GraphicsDevice.DisplayMode.Height
                        / (float)this.screenRect.Height);
                    break;
                case Screen.FullscreenMode.LargestIntegerScale:
                    this.Scale = (float)Math.Floor((double)Math.Min(
                        (float)this.GraphicsDevice.DisplayMode.Width / (float)this.screenRect.Width,
                        (float)this.GraphicsDevice.DisplayMode.Height / (float)this.screenRect.Height));
                    break;
            }
            this.HandleFullscreenViewport();
        }

        public void DisableFullscreen(float newScale)
        {
            this.Graphics.IsFullScreen = false;
            this.Graphics.ApplyChanges();
            this.scale = newScale;
            this.DrawRect.Width = this.viewport.Width = (int)((double)this.screenRect.Width
                      * (double)this.scale);
            this.DrawRect.Height = this.viewport.Height = (int)((double)this.screenRect.Height
                      * (double)this.scale * 2);
            this.SetWindowSize(this.ScaledWidth, this.ScaledHeight);
        }

        public void DisableFullscreen()
        {
            this.DisableFullscreen(this.OrigScale);
        }

        public int ScaledWidth
        {
            get
            {
                return (int)((double)this.width * (double)this.scale);
            }
        }

        public int ScaledHeight
        {
            get
            {
                return (int)((double)this.height * (double)this.scale * 2);
            }
        }

        public bool IsFullscreen
        {
            get
            {
                return this.Graphics.IsFullScreen;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2((float)this.Width, (float)this.Height);
            }
        }

        public int Width
        {
            get
            {
                return ((Texture2D)this.RenderTarget).Width;
            }
        }

        public int Height
        {
            get
            {
                return ((Texture2D)this.RenderTarget).Height;
            }
        }

        public Vector2 Center
        {
            get
            {
                return Vector2.Multiply(this.Size, 0.5f);
            }
        }

        public enum FullscreenMode
        {
            KeepScale,
            LargestScale,
            LargestIntegerScale,
        }
    }
}
