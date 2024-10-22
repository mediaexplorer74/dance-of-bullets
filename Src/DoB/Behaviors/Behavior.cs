// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.Behavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Extensions;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.Behaviors
{
  public abstract class Behavior : IPrototype
  {
    private bool isWaitingForEvent;
    private bool isFirstUpdate = true;
    private Cooldown delay;
    private Cooldown length;

    public double DelayMs { get; set; }

    public double LengthMs { get; set; }

    public string WaitForEvent { get; set; }

    public Behavior() => this.LengthMs = double.PositiveInfinity;

    public virtual bool IsElapsed => this.length != null && this.length.IsElapsed;

    public virtual IPrototype Clone()
    {
      Behavior behavior = (Behavior) this.MemberwiseClone();
      behavior.isFirstUpdate = true;
      behavior.delay = (Cooldown) null;
      behavior.length = (Cooldown) null;
      return (IPrototype) behavior;
    }

    public virtual void ResetTimers()
    {
      this.delay = (Cooldown) null;
      this.length = (Cooldown) null;
      this.isFirstUpdate = true;
    }

    public void Update(GameTime gameTime, GameObject gameObject)
    {
      if (!this.isWaitingForEvent && this.WaitForEvent != null)
      {
        this.isWaitingForEvent = true;
        EventBroker.SubscribeOnce(this.WaitForEvent, (Action<string>) (s =>
        {
          this.isWaitingForEvent = false;
          this.WaitForEvent = (string) null;
        }));
      }
      if (this.isWaitingForEvent)
        return;
      if (this.delay == null)
        this.delay = new Cooldown(this.DelayMs);
      if (this.length == null)
        this.length = new Cooldown(this.DelayMs + this.LengthMs);
      this.length.Update(gameTime.ElapsedMs());
      if (this.length.IsElapsed)
        return;
      if (!this.delay.IsElapsed)
      {
        this.delay.Update(gameTime.ElapsedMs());
        if (!this.delay.IsElapsed)
          return;
      }
      if (this.isFirstUpdate)
      {
        this.OnFirstUpdate(gameTime, gameObject);
        this.isFirstUpdate = false;
      }
      this.UpdateOverride(gameTime, gameObject);
    }

    public virtual void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
    }

    public virtual void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
    }

    public virtual void OnRemoval(GameObject gameObject)
    {
    }
  }
}
