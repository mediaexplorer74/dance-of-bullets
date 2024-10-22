// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.FramedMovementBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class FramedMovementBehavior : Behavior
  {
    public Rectangle Screen { get; set; }

    public FramedMovementBehavior()
    {
      if (GameObject.Game == null)
        return;
      this.Screen = GameObject.Game.GameplayRectangle;
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      Rectangle rectangle = gameObject.GetRectangle(true);
      if (rectangle.X < this.Screen.X)
        gameObject.MoveX = (double) this.Screen.X + gameObject.R - gameObject.X;
      if (rectangle.X + rectangle.Width > this.Screen.X + this.Screen.Width)
        gameObject.MoveX = (double) (this.Screen.X + this.Screen.Width) - gameObject.R - gameObject.X;
      if (rectangle.Y < this.Screen.Y)
        gameObject.MoveY = (double) this.Screen.Y + gameObject.R - gameObject.Y;
      if (rectangle.Y + rectangle.Height <= this.Screen.Y + this.Screen.Height)
        return;
      gameObject.MoveY = (double) (this.Screen.Y + this.Screen.Height) - gameObject.R - gameObject.Y;
    }
  }
}
