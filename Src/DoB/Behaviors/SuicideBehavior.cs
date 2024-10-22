// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.SuicideBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Extensions;
using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.Behaviors
{
  public class SuicideBehavior : Behavior
  {
    private Cooldown timeoutCooldown;
    private bool readyToCommit;

    public string OnEvent { get; set; }

    public double? TimeoutMs { get; set; }

    public override void ResetTimers()
    {
      base.ResetTimers();
      this.timeoutCooldown = (Cooldown) null;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      if (!string.IsNullOrEmpty(this.OnEvent))
        EventBroker.SubscribeOnce(this.OnEvent, new Action<string>(this.Commit));
      double? timeoutMs = this.TimeoutMs;
      if (!timeoutMs.HasValue)
        return;
      timeoutMs = this.TimeoutMs;
      this.timeoutCooldown = new Cooldown(timeoutMs.Value);
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      if (this.TimeoutMs.HasValue)
        this.timeoutCooldown.Update(gameTime.ElapsedMs());
      if (!this.readyToCommit && (this.timeoutCooldown == null || !this.timeoutCooldown.IsElapsed))
        return;
      gameObject.RemoveSelf();
    }

    public void Commit(string eventName) => this.readyToCommit = true;
  }
}
