// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.Player
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;
using DoB.Drawers;
using DoB.Extensions;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.GameObjects
{
  public class Player : Collideable, IHealth
  {
    internal bool debug_IsInvincible;
    private PlayerControlBehavior controlBehavior = new PlayerControlBehavior();
    private Cooldown damageCooldown;
    private Cooldown increaseDifficultyCooldown;
    private double increaseDifficultyDelay = 20000.0;
    private RangeDrawer manaDrawer = new RangeDrawer()
    {
      X = 10,
      Y = 10,
      Width = 300,
      Height = 10,
      Color = Color.FromNonPremultiplied(0, 19, (int) sbyte.MaxValue, (int) byte.MaxValue),
      BorderColor = Color.Black
    };
    private double manaDrainSpeed = 10.0;
    private double manaRegainSpeed = 1.0;
    private RangeDrawer paybackDrawer = new RangeDrawer()
    {
      X = 340,
      Y = 10,
      Width = 300,
      Height = 10,
      Color = Color.FromNonPremultiplied((int) sbyte.MaxValue, 19, 19, (int) byte.MaxValue),
      BorderColor = Color.Black
    };
    private double paybackDrainSpeed = 250.0;
    private double paybackRegainSpeed = 10.0;
    private const double textureScale = 0.6;
    private const double textureWidth = 128.4;
    private const double textureHeight = 39.6;
    private Random rnd = new Random();

    public Health Health { get; set; }

    public Consumable Mana { get; set; }

    public Consumable Payback { get; set; }

    public bool IsManaActive { get; set; }

    public bool IsPaybackActive { get; set; }

    public Player()
    {
      this.BaseTexture = "player_new";
      this.R = 2.4;
      this.Behaviors.Add((Behavior) this.controlBehavior);
      this.Behaviors.Add((Behavior) new FramedMovementBehavior());
      this.Drawers.Add((Drawer) this.manaDrawer);
      this.Drawers.Add((Drawer) this.paybackDrawer);
      this.Health = new Health((GameObject) this, 9.0);
      this.Mana = new Consumable(20.0);
      this.Payback = new Consumable(1000.0);
      this.IsFriendly = true;
      this.Mana.Amount = 0.0;
      this.Payback.Amount = 0.0;
    }

    public override Rectangle GetDrawingRectangle()
    {
      return new Rectangle((int) (this.X - 64.2), (int) (this.Y - 19.8) + 4, 128, 39);
    }

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      if (this.damageCooldown == null)
        this.damageCooldown = new Cooldown();
      if (this.increaseDifficultyCooldown != null)
        return;
      this.increaseDifficultyCooldown = new Cooldown();
      this.increaseDifficultyCooldown.Reset(this.increaseDifficultyDelay);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this.damageCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if (this.damageCooldown.IsElapsed)
        this.Tint = Color.White;
      this.increaseDifficultyCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if (this.increaseDifficultyCooldown.IsElapsed)
      {
        GameSpeedManager.IncreaseDifficulty();
        this.increaseDifficultyCooldown.Reset(this.increaseDifficultyDelay);
      }
      this.UpdateMana(gameTime);
      this.UpdatePayback(gameTime);
    }

    private void UpdateMana(GameTime gameTime)
    {
      if (this.IsManaActive)
        this.Mana.Amount -= GameSpeedManager.ApplySpeed(this.manaDrainSpeed, gameTime.ElapsedMs());
      else
        this.Mana.Amount = (double) MathHelper.Clamp((float) (this.Mana.Amount + GameSpeedManager.ApplySpeed(this.manaRegainSpeed, gameTime.ElapsedMs())), 0.0f, (float) this.Mana.OriginalAmount);
      this.manaDrawer.Rate = this.Mana.Amount / this.Mana.OriginalAmount;
      if (!this.Mana.HasRunOut)
        return;
      this.StopMana();
    }

    private void UpdatePayback(GameTime gameTime)
    {
      if (this.IsPaybackActive)
      {
        this.Payback.Amount -= GameSpeedManager.ApplySpeed(this.paybackDrainSpeed, gameTime.ElapsedMs());
      }
      else
      {
        this.Payback.Amount = (double) MathHelper.Clamp((float) (this.Payback.Amount + GameSpeedManager.ApplySpeed(this.paybackRegainSpeed, gameTime.ElapsedMs())), 0.0f, (float) this.Payback.OriginalAmount);
        if (this.Payback.Amount == this.Payback.OriginalAmount)
          this.paybackDrawer.Color = Color.DarkGreen;
      }
      this.paybackDrawer.Rate = this.Payback.Amount / this.Payback.OriginalAmount;
      if (!this.Payback.HasRunOut)
        return;
      this.IsPaybackActive = false;
    }

    public void StartMana()
    {
      this.IsManaActive = true;
      GameSpeedManager.GlobalModifier = 0.5;
      HitboxDrawer.AreVisible = true;
    }

    public void StopMana()
    {
      this.IsManaActive = false;
      GameSpeedManager.GlobalModifier = 1.0;
      HitboxDrawer.AreVisible = false;
    }

    public void ActivatePayback()
    {
      if (this.IsPaybackActive || this.Payback.Amount != this.Payback.OriginalAmount)
        return;
      this.IsPaybackActive = true;
      this.paybackDrawer.Color = Color.FromNonPremultiplied((int) sbyte.MaxValue, 19, 19, (int) byte.MaxValue);
      GameObject.Game.ClearBullets();
    }

    public override IPrototype Clone() => throw new NotSupportedException();

    public override void CollideWith(Collideable c) => c.Collide(this);

    public override void Collide(Bullet b)
    {
      if (b.IsFriendly || !this.damageCooldown.IsElapsed || this.debug_IsInvincible)
        return;
      this.Health.Hit();
      this.damageCooldown.Reset(3000.0);
      GameSpeedManager.ReduceDifficulty();
      this.increaseDifficultyCooldown.Reset(this.increaseDifficultyDelay);
      this.Tint = Color.FromNonPremultiplied((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 125);
    }

    public override void Collide(Player p)
    {
    }

    public override void Collide(Enemy e)
    {
    }

    public void Shoot()
    {
      PlayerBullet playerBullet1 = new PlayerBullet(this.IsPaybackActive ? 1400.0 : 700.0);
      playerBullet1.X = this.X + 54.0;
      playerBullet1.Y = this.Y + 100.0 * (this.IsPaybackActive ? this.rnd.NextDouble() - 0.5 : 0.0);
      playerBullet1.R = 10.0;
      playerBullet1.Tint = this.IsPaybackActive ? Color.White : Color.White;
      PlayerBullet playerBullet2 = playerBullet1;
      GameObject.Game.Objects.Add((GameObject) playerBullet2);
      PlayerBullet playerBullet3 = new PlayerBullet(this.IsPaybackActive ? 1400.0 : 700.0);
      playerBullet3.X = this.X + 12.0;
      playerBullet3.Y = this.Y + 16.8 + 100.0 * (this.IsPaybackActive ? this.rnd.NextDouble() - 0.5 : 0.0);
      playerBullet3.R = 10.0;
      playerBullet3.Tint = this.IsPaybackActive ? Color.White : Color.White;
      PlayerBullet playerBullet4 = playerBullet3;
      GameObject.Game.Objects.Add((GameObject) playerBullet4);
    }
  }
}
