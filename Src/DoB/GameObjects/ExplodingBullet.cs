// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.ExplodingBullet
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;
using Microsoft.Xna.Framework;
using System.Windows.Markup;

#nullable disable
namespace DoB.GameObjects
{
  [ContentProperty("ClusterBulletPrototype")]
  public class ExplodingBullet : Bullet
  {
    public int ClusterCount { get; set; }

    public Bullet ClusterBulletPrototype { get; set; }

    public double Vexpl { get; set; }

    public override void OnFirstUpdate(GameTime gameTime)
    {
      base.OnFirstUpdate(gameTime);
      float num = 6.28318548f / (float) this.ClusterCount;
      for (int index = 0; index < this.ClusterCount; ++index)
      {
        Bullet bullet = (Bullet) this.ClusterBulletPrototype.Clone();
        GameObject.Game.Objects.Add((GameObject) bullet);
        bullet.X = this.X;
        bullet.Y = this.Y;
        bullet.GeneralDirection = this.GeneralDirection;
        if (this.Vexpl != 0.0)
        {
          bullet.AutoUpdateGeneralDirection = false;
          bullet.Behaviors.Add((Behavior) new RadialMovementBehavior()
          {
            DirectionOverride = new double?((double) index * (double) num),
            Vr = this.Vexpl
          });
        }
      }
      this.RemoveSelf();
    }
  }
}
