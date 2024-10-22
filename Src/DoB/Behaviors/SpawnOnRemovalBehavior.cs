// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.SpawnOnRemovalBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class SpawnOnRemovalBehavior : Behavior
  {
    private double x;
    private double y;
    private double r;

    public GameObject Prototype { get; set; }

    public bool InheritSize { get; set; }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      this.x = gameObject.X;
      this.y = gameObject.Y;
      this.r = gameObject.R;
    }

    public override void OnRemoval(GameObject gameObject)
    {
      if (!GameObject.Game.GameplayRectangle.Contains((int) this.x, (int) this.y))
        return;
      GameObject gameObject1 = (GameObject) this.Prototype.Clone();
      GameObject.Game.Objects.Add(gameObject1);
      gameObject1.X = this.x;
      gameObject1.Y = this.y;
      if (!this.InheritSize)
        return;
      gameObject1.R = this.r;
    }
  }
}
