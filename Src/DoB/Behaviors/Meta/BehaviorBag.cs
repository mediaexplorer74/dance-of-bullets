// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.BehaviorBag
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.Behaviors
{
  [ContentProperty("Behaviors")]
  public class BehaviorBag : Behavior, IGunConfigurator
  {
    private Color? _BulletTintOverride;
    private string _BulletTextureOverride;

    public List<Behavior> Behaviors { get; set; }

    public bool Loop { get; set; }

    public BehaviorBag() => this.Behaviors = new List<Behavior>();

    public override IPrototype Clone()
    {
      BehaviorBag behaviorBag = (BehaviorBag) base.Clone();
      behaviorBag.Behaviors = new List<Behavior>();
      for (int index = 0; index < this.Behaviors.Count; ++index)
        behaviorBag.Behaviors.Add((Behavior) this.Behaviors[index].Clone());
      return (IPrototype) behaviorBag;
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      bool flag = true;
      for (int index = 0; index < this.Behaviors.Count; ++index)
      {
        this.Behaviors[index].Update(gameTime, gameObject);
        flag &= this.Behaviors[index].IsElapsed;
      }
      if (!flag || !this.Loop)
        return;
      for (int index = 0; index < this.Behaviors.Count; ++index)
        this.Behaviors[index].ResetTimers();
    }

    public Color? BulletTintOverride
    {
      get => this._BulletTintOverride;
      set
      {
        this._BulletTintOverride = value;
        for (int index = 0; index < this.Behaviors.Count; ++index)
        {
          if (this.Behaviors[index] is IGunConfigurator)
            ((IGunConfigurator) this.Behaviors[index]).BulletTintOverride = value;
        }
      }
    }

    public string BulletTextureOverride
    {
      get => this._BulletTextureOverride;
      set
      {
        this._BulletTextureOverride = value;
        for (int index = 0; index < this.Behaviors.Count; ++index)
        {
          if (this.Behaviors[index] is IGunConfigurator)
            ((IGunConfigurator) this.Behaviors[index]).BulletTextureOverride = value;
        }
      }
    }
  }
}
