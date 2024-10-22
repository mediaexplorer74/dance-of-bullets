// Decompiled with JetBrains decompiler
// Type: DoB.Utility.Cooldown
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Utility
{
  public class Cooldown
  {
    private double cooldown;
    private double totalCooldown;

    public Cooldown()
    {
    }

    public Cooldown(double ms)
    {
      this.totalCooldown = ms;
      this.cooldown = ms;
    }

    public void Reset(double ms)
    {
      this.totalCooldown = ms;
      this.cooldown = ms;
    }

    public void Update(double elapsedTimeMs)
    {
      if (this.IsElapsed)
        return;
      this.cooldown -= GameSpeedManager.ApplySpeed(1000.0, elapsedTimeMs);
    }

    public bool IsElapsed => this.cooldown <= 0.0;

    public float Progress
    {
      get => MathHelper.Clamp(1f - (float) (this.cooldown / this.totalCooldown), 0.0f, 1f);
    }
  }
}
