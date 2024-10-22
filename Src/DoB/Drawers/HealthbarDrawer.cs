// Decompiled with JetBrains decompiler
// Type: DoB.Drawers.HealthbarDrawer
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Extensions;
using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace DoB.Drawers
{
  public class HealthbarDrawer : RangeDrawer
  {
    private Cooldown delay = new Cooldown();
    private double _DelayMs;

    public double DelayMs
    {
      get => this._DelayMs;
      set
      {
        this._DelayMs = value;
        this.delay.Reset(value);
      }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
    {
      this.delay.Update(gameTime.ElapsedMs());
      if (!this.delay.IsElapsed)
        return;
      double rate = ((IHealth) gameObject).Health.Amount / ((IHealth) gameObject).Health.OriginalAmount;
      this.DrawInner(spriteBatch, rate);
    }
  }
}
