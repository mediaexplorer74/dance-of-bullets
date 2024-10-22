// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.AimingTurretBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.Behaviors
{
  public class AimingTurretBehavior : TurretBehaviorBase
  {
    protected double idealDirection;

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      this.idealDirection = (GameObject.Game.Player.X - gameObject.X > 0.0 ? 0.0 : Math.PI) + Math.Atan((GameObject.Game.Player.Y - gameObject.Y) / (GameObject.Game.Player.X - gameObject.X));
      base.UpdateOverride(gameTime, gameObject);
    }

    protected override Bullet Fire(GameObject gameObject)
    {
      Bullet bullet = base.Fire(gameObject);
      bullet.GeneralDirection = this.idealDirection;
      return bullet;
    }
  }
}
