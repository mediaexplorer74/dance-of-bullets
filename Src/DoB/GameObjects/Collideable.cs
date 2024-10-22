// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.Collideable
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;
using DoB.Drawers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace DoB.GameObjects
{
  public abstract class Collideable : GameObject
  {
    public bool IsFriendly { get; set; }

    public Collideable()
    {
      this.Drawers.Add((Drawer) new HitboxDrawer());
      List<Behavior> behaviors = this.Behaviors;
      SpawnOnRemovalBehavior onRemovalBehavior = new SpawnOnRemovalBehavior();
      Decoration decoration = new Decoration();
      decoration.BaseTexture = "explosion";
      decoration.LengthMs = 600.0;
      decoration.LengthVariance = 300.0;
      onRemovalBehavior.Prototype = (GameObject) decoration;
      onRemovalBehavior.InheritSize = true;
      behaviors.Add((Behavior) onRemovalBehavior);
      this.CollisionBoxScale = 1.0;
    }

    public double CollisionBoxScale { get; set; }

    public double CollisionRadius => this.R * this.CollisionBoxScale;

    public abstract void CollideWith(Collideable c);

    public virtual void Collide(Bullet b)
    {
    }

    public virtual void Collide(Player p)
    {
    }

    public virtual void Collide(Enemy e)
    {
    }

    public Rectangle GetCollisionRectangle(bool applyPendingMovement = false)
    {
      return !applyPendingMovement ? new Rectangle((int) (this.X - this.CollisionRadius), (int) (this.Y - this.CollisionRadius), (int) (2.0 * this.CollisionRadius), (int) (2.0 * this.CollisionRadius)) : new Rectangle((int) (this.X + this.MoveX - this.CollisionRadius), (int) (this.Y + this.MoveY - this.CollisionRadius), (int) (2.0 * this.CollisionRadius), (int) (2.0 * this.CollisionRadius));
    }
  }
}
