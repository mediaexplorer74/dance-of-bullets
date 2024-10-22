// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.MultiBullet
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.GameObjects
{
  public class MultiBullet : Bullet
  {
    private Cooldown duplicateCooldown = new Cooldown();
    private MultiBullet originalClone;
    private bool hadOneFullUpdate;

    public double DuplicateCooldownMs { get; set; }

    public int Count { get; set; }

    public double DegIncrement { get; set; }

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      this.duplicateCooldown.Reset(this.DuplicateCooldownMs);
      if (this.Count <= 1)
        return;
      this.originalClone = (MultiBullet) this.Clone();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (!this.hadOneFullUpdate)
      {
        this.hadOneFullUpdate = true;
      }
      else
      {
        this.duplicateCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        if (!this.duplicateCooldown.IsElapsed || this.Count <= 1)
          return;
        MultiBullet originalClone = this.originalClone;
        this.originalClone = (MultiBullet) null;
        this.Count = 0;
        --originalClone.Count;
        GameObject.Game.Objects.Add((GameObject) originalClone);
        originalClone.GeneralDirection += this.DegIncrement;
      }
    }

    public override IPrototype Clone()
    {
      MultiBullet multiBullet = (MultiBullet) base.Clone();
      multiBullet.duplicateCooldown = new Cooldown();
      multiBullet.hadOneFullUpdate = false;
      return (IPrototype) multiBullet;
    }
  }
}
