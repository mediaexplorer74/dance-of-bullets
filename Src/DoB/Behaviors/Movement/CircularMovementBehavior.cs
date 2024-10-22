// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.CircularMovementBehavior
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
  public class CircularMovementBehavior : Behavior
  {
    public double Vdeg { get; set; }

    public double? Vper { get; set; }

    public double Adeg { get; set; }

    public double Aper { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject g)
    {
      base.UpdateOverride(gameTime, g);
      double totalMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
      double? vper;
      if (this.Vper.HasValue)
      {
        vper = this.Vper;
        double num = GameSpeedManager.ApplySpeed(this.Aper, totalMilliseconds);
        this.Vper = vper.HasValue ? new double?(vper.GetValueOrDefault() + num) : new double?();
      }
      else
        this.Vdeg += GameSpeedManager.ApplySpeed(this.Adeg, totalMilliseconds);
      Vector2 vector2_1 = new Vector2((float) g.X, (float) g.Y);
      Vector2 vector2_2 = new Vector2((float) g.Ox, (float) g.Oy);
      double num1 = (double) Vector2.Distance(vector2_1, vector2_2);
      if (num1 == 0.0)
        return;
      vper = this.Vper;
      double num2;
      if (!vper.HasValue)
      {
        num2 = GameSpeedManager.ApplySpeed(this.Vdeg, totalMilliseconds);
      }
      else
      {
        vper = this.Vper;
        num2 = GameSpeedManager.ApplySpeed(vper.Value, totalMilliseconds) / num1;
      }
      double num3 = num2;
      Vector2 vector2_3 = Vector2.Subtract(vector2_1, vector2_2);
      Vector2 vector2_4 = Vector2.Add(new Vector2((float) ((double) vector2_3.X * Math.Cos(num3) - (double) vector2_3.Y * Math.Sin(num3)), (float) ((double) vector2_3.X * Math.Sin(num3) + (double) vector2_3.Y * Math.Cos(num3))), vector2_2);
      g.MoveX += (double) vector2_4.X - g.X;
      g.MoveY += (double) vector2_4.Y - g.Y;
    }
  }
}
