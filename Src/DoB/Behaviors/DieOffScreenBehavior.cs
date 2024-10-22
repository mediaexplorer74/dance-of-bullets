// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.DieOffScreenBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class DieOffScreenBehavior : Behavior
  {
    public Rectangle Screen
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

    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public DieOffScreenBehavior()
    {
      if (GameObject.Game == null)
        return;
      this.Screen = GameObject.Game.GameplayRectangle;
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      if (this.Screen.Intersects(gameObject.GetRectangle()))
        return;
      gameObject.RemoveSelf();
    }
  }
}
