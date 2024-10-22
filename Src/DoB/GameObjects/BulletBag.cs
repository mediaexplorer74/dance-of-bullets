// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.BulletBag
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Xaml;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.GameObjects
{
  [ContentProperty("Bullets")]
  public class BulletBag : Bullet
  {
    public List<Bullet> Bullets { get; set; }

    public BulletBag() => this.Bullets = new List<Bullet>();

    public override IPrototype Clone()
    {
      BulletBag bulletBag = (BulletBag) base.Clone();
      bulletBag.Bullets = new List<Bullet>();
      for (int index = 0; index < this.Bullets.Count; ++index)
        bulletBag.Bullets.Add((Bullet) this.Bullets[index].Clone());
      return (IPrototype) bulletBag;
    }

    public override void RemoveSelf()
    {
      base.RemoveSelf();
      for (int index = 0; index < this.Bullets.Count; ++index)
        this.Bullets[index].RemoveSelf();
    }

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      for (int index = 0; index < this.Bullets.Count; ++index)
      {
        Bullet bullet = this.Bullets[index];
        bullet.X += this.X;
        bullet.Y += this.Y;
        bullet.R += this.R;
        bullet.Tint = this.Tint;
        bullet.GeneralDirection += this.GeneralDirection;
        GameObject.Game.Objects.Add((GameObject) bullet);
      }
      base.RemoveSelf();
    }
  }
}
