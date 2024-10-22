// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.TurningTurretBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class TurningTurretBehavior : TurretBehaviorBase
  {
    private bool wasShooting;

    public double TurnAfterReload { get; set; }

    public double TurnAfterShoot { get; set; }

    public double Alpha { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      bool flag = this.Capacity == 0 || this.remainingBullets > 0;
      if (flag && !this.wasShooting)
        this.Alpha += this.TurnAfterReload;
      this.wasShooting = flag;
      base.UpdateOverride(gameTime, gameObject);
    }

    protected override Bullet Fire(GameObject gameObject)
    {
      Bullet bullet = base.Fire(gameObject);
      bullet.GeneralDirection = this.Alpha;
      this.Alpha += this.TurnAfterShoot;
      return bullet;
    }
  }
}
