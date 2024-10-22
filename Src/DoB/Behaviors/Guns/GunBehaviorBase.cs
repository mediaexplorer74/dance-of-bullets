// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.GunBehaviorBase
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System.Windows.Markup;

#nullable disable
namespace DoB.Behaviors
{
  [ContentProperty("BulletPrototype")]
  public abstract class GunBehaviorBase : Behavior, IGunConfigurator
  {
    private Bullet _BulletPrototype;
    private Color originalBulletTint;
    private Color? _BulletTintOverride;
    private string originalBulletTexture;
    private string _BulletTextureOverride;

    public virtual Bullet BulletPrototype
    {
      get => this._BulletPrototype;
      set
      {
        this._BulletPrototype = value;
        this.originalBulletTint = this._BulletPrototype.Tint;
        this.originalBulletTexture = this._BulletPrototype.BaseTexture;
        if (!this.BulletTintOverride.HasValue)
          return;
        this._BulletPrototype.Tint = this.BulletTintOverride.Value;
      }
    }

    public override IPrototype Clone()
    {
      GunBehaviorBase gunBehaviorBase = (GunBehaviorBase) base.Clone();
      gunBehaviorBase.BulletPrototype = (Bullet) this.BulletPrototype.Clone();
      return (IPrototype) gunBehaviorBase;
    }

    public Color? BulletTintOverride
    {
      get => this._BulletTintOverride;
      set
      {
        this._BulletTintOverride = value;
        if (this.BulletPrototype == null)
          return;
        if (value.HasValue)
          this.BulletPrototype.Tint = value.Value;
        else
          this.BulletPrototype.Tint = this.originalBulletTint;
      }
    }

    public string BulletTextureOverride
    {
      get => this._BulletTextureOverride;
      set
      {
        this._BulletTextureOverride = value;
        if (this.BulletPrototype == null)
          return;
        if (value != null)
          this.BulletPrototype.BaseTexture = value;
        else
          this.BulletPrototype.BaseTexture = this.originalBulletTexture;
      }
    }
  }
}
