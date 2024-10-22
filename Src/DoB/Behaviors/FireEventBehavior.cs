// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.FireEventBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Extensions;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class FireEventBehavior : Behavior
  {
    protected Cooldown fireCooldown;

    public string EventName { get; set; }

    public double CooldownMs { get; set; }

    public override bool IsElapsed
    {
      get
      {
        Cooldown fireCooldown = this.fireCooldown;
        return fireCooldown != null && fireCooldown.IsElapsed;
      }
    }

    public override void ResetTimers()
    {
      base.ResetTimers();
      this.fireCooldown = (Cooldown) null;
    }

    public override IPrototype Clone()
    {
      FireEventBehavior fireEventBehavior = (FireEventBehavior) base.Clone();
      fireEventBehavior.fireCooldown = (Cooldown) null;
      return (IPrototype) fireEventBehavior;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      this.fireCooldown = new Cooldown(this.CooldownMs);
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      this.fireCooldown.Update(gameTime.ElapsedMs());
      if (!this.fireCooldown.IsElapsed)
        return;
      EventBroker.FireEvent(this.EventName);
    }
  }
}
