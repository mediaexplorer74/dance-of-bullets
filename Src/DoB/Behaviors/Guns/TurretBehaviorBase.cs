// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.TurretBehaviorBase
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public abstract class TurretBehaviorBase : GunBehaviorBase
  {
    protected Cooldown gunCooldown;
    protected Cooldown reloadCooldown;
    protected int remainingBullets;

    public double ReloadDurationMs { get; set; }

    public int Capacity { get; set; }

    public double GunCooldownMs { get; set; }

    public bool NeedsReloadFirst { get; set; }

    public bool NeedsCooldownAfterReload { get; set; }

    public override void ResetTimers()
    {
      base.ResetTimers();
      this.gunCooldown = (Cooldown) null;
      this.reloadCooldown = (Cooldown) null;
    }

    public override IPrototype Clone()
    {
      TurretBehaviorBase turretBehaviorBase = (TurretBehaviorBase) base.Clone();
      turretBehaviorBase.gunCooldown = (Cooldown) null;
      turretBehaviorBase.reloadCooldown = (Cooldown) null;
      return (IPrototype) turretBehaviorBase;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      this.gunCooldown = new Cooldown(this.NeedsCooldownAfterReload ? this.GunCooldownMs : 0.0);
      this.reloadCooldown = new Cooldown(this.ReloadDurationMs);
      if (this.NeedsReloadFirst)
        return;
      this.remainingBullets = this.Capacity;
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      bool flag = this.Capacity == 0 || this.remainingBullets > 0;
      if (!flag)
      {
        this.reloadCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        if (!this.reloadCooldown.IsElapsed)
          return;
        this.gunCooldown.Reset(this.NeedsCooldownAfterReload ? this.GunCooldownMs : 0.0);
        this.remainingBullets = this.Capacity;
      }
      else
      {
        this.gunCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        if (!flag || !this.gunCooldown.IsElapsed)
          return;
        this.Fire(gameObject);
        --this.remainingBullets;
        this.gunCooldown.Reset(this.GunCooldownMs);
        if (this.remainingBullets >= 1)
          return;
        this.reloadCooldown.Reset(this.ReloadDurationMs);
      }
    }

    protected virtual Bullet Fire(GameObject gameObject)
    {
      Bullet bullet = (Bullet) this.BulletPrototype.Clone();
      GameObject.Game.Objects.Add((GameObject) bullet);
      bullet.X = gameObject.X;
      bullet.Y = gameObject.Y;
      if (this.BulletTintOverride.HasValue)
        bullet.Tint = this.BulletTintOverride.Value;
      if (this.BulletTextureOverride != null)
        bullet.BaseTexture = this.BulletTextureOverride;
      return bullet;
    }
  }
}
