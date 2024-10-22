// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.Bullet
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;

#nullable disable
namespace DoB.GameObjects
{
  public class Bullet : Collideable
  {
    private DieOffScreenBehavior dieOffScreen;

    public Bullet()
    {
      this.R = 7.0;
      this.dieOffScreen = new DieOffScreenBehavior();
      this.Behaviors.Add((Behavior) this.dieOffScreen);
    }

    public bool DieOffScreen
    {
      get => this.Behaviors.Contains((Behavior) this.dieOffScreen);
      set
      {
        if (value)
          this.Behaviors.Add((Behavior) this.dieOffScreen);
        else
          this.Behaviors.Remove((Behavior) this.dieOffScreen);
      }
    }

    public override void CollideWith(Collideable c) => c.Collide(this);

    public override void Collide(Bullet b)
    {
    }

    public override void Collide(Player p)
    {
      if (this.IsFriendly)
        return;
      this.RemoveSelf();
    }

    public override void Collide(Enemy e)
    {
      if (!this.IsFriendly)
        return;
      this.RemoveSelf();
    }
  }
}
