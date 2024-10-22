// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.GunBehaviorReference
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Behaviors
{
  public class GunBehaviorReference : BehaviorReference, IGunConfigurator
  {
    public Color? BulletTintOverride { get; set; }

    public string BulletTextureOverride { get; set; }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      ((IGunConfigurator) this.referencedBehavior).BulletTintOverride = this.BulletTintOverride;
      ((IGunConfigurator) this.referencedBehavior).BulletTextureOverride = this.BulletTextureOverride;
    }
  }
}
