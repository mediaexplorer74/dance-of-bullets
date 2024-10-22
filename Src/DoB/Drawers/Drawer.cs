// Decompiled with JetBrains decompiler
// Type: DoB.Drawers.Drawer
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace DoB.Drawers
{
  public abstract class Drawer : IPrototype
  {
    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject);

    public virtual IPrototype Clone() => (IPrototype) this.MemberwiseClone();
  }
}
