// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.SpinningTurretBehavior
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
  [Obsolete("Use TurningTurretBehavior instead.")]
  public class SpinningTurretBehavior : TurretBehaviorBase
  {
    public double VdegReloading { get; set; }

    public double VdegShooting { get; set; }

    public double Vdeg
    {
      set
      {
        this.VdegShooting = value;
        this.VdegReloading = value;
      }
    }

    public double Alpha { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      this.Alpha += GameSpeedManager.ApplySpeed((this.Capacity == 0 ? 1 : (this.remainingBullets > 0 ? 1 : 0)) != 0 ? this.VdegShooting : this.VdegReloading, gameTime.ElapsedGameTime.TotalMilliseconds);
      base.UpdateOverride(gameTime, gameObject);
    }

    protected override Bullet Fire(GameObject gameObject)
    {
      Bullet bullet = base.Fire(gameObject);
      bullet.GeneralDirection = this.Alpha;
      return bullet;
    }
  }
}
