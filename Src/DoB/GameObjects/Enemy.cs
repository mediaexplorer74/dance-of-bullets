// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.Enemy
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace DoB.GameObjects
{
  public class Enemy : Collideable, IHealth
  {
    private Cooldown damageCooldown = new Cooldown();
    private Color originalTint;

    public string EventOnDeath { get; set; }

    public double Lives
    {
      get => this.Health.Amount;
      set => this.Health.Amount = value;
    }

    public Health Health { get; set; }

    public Enemy() => this.Health = new Health((GameObject) this, 0.0);

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      this.originalTint = this.Tint;
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this.damageCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if (!this.damageCooldown.IsElapsed)
        return;
      this.Tint = this.originalTint;
    }

    public override IPrototype Clone()
    {
      Enemy g = (Enemy) base.Clone();
      g.Health = new Health((GameObject) g, this.Lives);
      return (IPrototype) g;
    }

    public override void CollideWith(Collideable c) => c.Collide(this);

    public override void Collide(Bullet b)
    {
      if (!b.IsFriendly || !this.damageCooldown.IsElapsed)
        return;
      this.Health.Hit();
      this.damageCooldown.Reset(100.0);
      this.originalTint = this.Tint;
      this.Tint = Color.FromNonPremultiplied((int) byte.MaxValue, 50, 50, (int) byte.MaxValue);
    }

    public override void Collide(Player p)
    {
    }

    public override void Collide(Enemy e)
    {
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      base.Draw(gameTime, spriteBatch);
    }

    public override void RemoveSelf()
    {
      base.RemoveSelf();
      if (string.IsNullOrEmpty(this.EventOnDeath))
        return;
      EventBroker.FireEvent(this.EventOnDeath);
    }
  }
}
