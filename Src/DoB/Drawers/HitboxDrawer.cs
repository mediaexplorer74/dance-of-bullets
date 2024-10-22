// Decompiled with JetBrains decompiler
// Type: DoB.Drawers.HitboxDrawer
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace DoB.Drawers
{
  public class HitboxDrawer : TextureDrawer
  {
    public static bool AreVisible { get; set; }

    static HitboxDrawer() => HitboxDrawer.AreVisible = false;

    public HitboxDrawer() => this.Texture = "hitbox";

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
    {
      if (!HitboxDrawer.AreVisible)
        return;
      spriteBatch.Draw(this.textureObj, ((Collideable) gameObject).GetCollisionRectangle(), Color.White);
    }
  }
}
