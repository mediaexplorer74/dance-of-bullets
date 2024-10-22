// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.XYMovementBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class XYMovementBehavior : Behavior
  {
    public double Vx { get; set; }

    public double Vy { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      double totalMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
      gameObject.MoveX += GameSpeedManager.ApplySpeed(this.Vx, totalMilliseconds);
      gameObject.MoveY += GameSpeedManager.ApplySpeed(this.Vy, totalMilliseconds);
    }
  }
}
