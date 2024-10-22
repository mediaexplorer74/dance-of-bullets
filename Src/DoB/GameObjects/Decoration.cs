// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.Decoration
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Extensions;
using DoB.Utility;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace DoB.GameObjects
{
  public class Decoration : GameObject
  {
    private Cooldown lengthCooldown;
    private static Random rnd = new Random();

    public double LengthMs { get; set; }

    public double LengthVariance { get; set; }

    public Decoration() => this.LengthMs = double.PositiveInfinity;

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      this.lengthCooldown = new Cooldown(this.LengthMs + this.LengthVariance * 2.0 * (Decoration.rnd.NextDouble() - 0.5));
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this.lengthCooldown.Update(gameTime.ElapsedMs());
      if (!this.lengthCooldown.IsElapsed)
        return;
      this.RemoveSelf();
    }
  }
}
