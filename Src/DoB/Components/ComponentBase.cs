// Decompiled with JetBrains decompiler
// Type: DoB.Components.ComponentBase
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Utility;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.Components
{
  public abstract class ComponentBase : IComponent
  {
    private Cooldown delay = new Cooldown();
    private double _DelayMs;
    private bool isWaiting;
    private string _WaitForEvent;
    private bool wasWaiting;

    public double DelayMs
    {
      get => this._DelayMs;
      set
      {
        this._DelayMs = value;
        this.delay.Reset(value);
      }
    }

    public string WaitForEvent
    {
      get => this._WaitForEvent;
      set
      {
        this._WaitForEvent = value;
        this.isWaiting = true;
        EventBroker.SubscribeOnce(value, (Action<string>) (s => this.isWaiting = false));
      }
    }

    public void Update(GameTime gameTime)
    {
      if (this.isWaiting)
      {
        this.wasWaiting = true;
      }
      else
      {
        if (this.wasWaiting)
        {
          this.wasWaiting = false;
          this.delay.Reset(this.DelayMs);
        }
        if (!this.delay.IsElapsed)
        {
          this.delay.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
          if (!this.delay.IsElapsed)
            return;
        }
        this.UpdateOverride(gameTime);
      }
    }

    protected abstract void UpdateOverride(GameTime gameTime);
  }
}
