// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.BehaviorReference
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class BehaviorReference : Behavior
  {
    protected Behavior referencedBehavior;
    private string _ReferenceName;

    public string ReferenceName
    {
      get => this._ReferenceName;
      set
      {
        if (this._ReferenceName == value)
          return;
        this._ReferenceName = value;
        this.referencedBehavior = (Behavior) null;
      }
    }

    public override IPrototype Clone()
    {
      BehaviorReference behaviorReference = (BehaviorReference) base.Clone();
      behaviorReference.referencedBehavior = (Behavior) null;
      return (IPrototype) behaviorReference;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      if (this.referencedBehavior != null)
        return;
      this.referencedBehavior = (Behavior) Prototypes.All[this.ReferenceName].Clone();
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      this.referencedBehavior.Update(gameTime, gameObject);
    }
  }
}
