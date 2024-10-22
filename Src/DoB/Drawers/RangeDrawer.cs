// Decompiled with JetBrains decompiler
// Type: DoB.Drawers.RangeDrawer
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace DoB.Drawers
{
  public class RangeDrawer : Drawer
  {
    protected Texture2D textureObj;

    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public Color Color { get; set; }

    public Color BorderColor { get; set; }

    public double Rate { get; set; }

    public Rectangle Position
    {
      get => new Rectangle(this.X, this.Y, this.Width, this.Height);
      set
      {
        this.X = value.X;
        this.Y = value.Y;
        this.Width = value.Width;
        this.Height = value.Height;
      }
    }

    public RangeDrawer()
    {
      if (GameObject.Game == null)
        return;
      this.textureObj = GameObject.Game.Content.Load<Texture2D>("white");
    }

    public override IPrototype Clone()
    {
      HealthbarDrawer healthbarDrawer = (HealthbarDrawer) base.Clone();
      healthbarDrawer.textureObj = GameObject.Game.Content.Load<Texture2D>("white");
      return (IPrototype) healthbarDrawer;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
    {
      this.DrawInner(spriteBatch, this.Rate);
    }

    protected void DrawInner(SpriteBatch spriteBatch, double rate)
    {
      if (this.textureObj == null)
        return;
      Rectangle position1 = this.Position with
      {
        Width = (int) (rate * (double) this.Position.Width)
      };
      Rectangle position2 = this.Position;
      position2.X -= 2;
      position2.Width += 4;
      position2.Y -= 2;
      position2.Height += 4;
      spriteBatch.Draw(this.textureObj, position2, this.BorderColor);
      spriteBatch.Draw(this.textureObj, position1, this.Color);
    }
  }
}
