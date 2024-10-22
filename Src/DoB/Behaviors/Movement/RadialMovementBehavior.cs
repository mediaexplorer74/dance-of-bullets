// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.RadialMovementBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.Behaviors
{
  public class RadialMovementBehavior : Behavior
  {
    public double Vr { get; set; }

    public double? DirectionOverride { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject g)
    {
      base.UpdateOverride(gameTime, g);
      double num1 = GameSpeedManager.ApplySpeed(this.Vr, gameTime.ElapsedGameTime.TotalMilliseconds);
      double num2 = this.DirectionOverride ?? g.GeneralDirection;
      g.MoveX += Math.Cos(num2) * num1;
      g.MoveY += Math.Sin(num2) * num1;
    }
  }
}
