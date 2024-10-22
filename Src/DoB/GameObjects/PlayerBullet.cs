// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.PlayerBullet
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.GameObjects
{
  public class PlayerBullet : Bullet
  {
    public PlayerBullet(double vx)
    {
      this.IsFriendly = true;
      this.Behaviors.Add((Behavior) new XYMovementBehavior()
      {
        Vx = vx
      });
      this.BaseTexture = "playerbullet";
    }

    public override Rectangle GetDrawingRectangle()
    {
      return new Rectangle()
      {
        X = (int) this.X,
        Y = (int) this.Y - 4,
        Width = 56,
        Height = 8
      };
    }
  }
}
