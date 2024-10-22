// Decompiled with JetBrains decompiler
// Type: DoB.Drawers.TextureDrawer
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Drawers
{
  public class TextureDrawer : Drawer
  {
    protected static readonly string[] rotatableTextures = new string[1]
    {
      "arrow"
    };
    protected bool rotate;
    protected Texture2D textureObj;
    private string _Texture = "";

    public string Texture
    {
      get => this._Texture;
      set
      {
        if (!(this._Texture != value))
          return;
        this._Texture = value;
        this.rotate = ((IEnumerable<string>) TextureDrawer.rotatableTextures).Contains<string>(value);
        if (GameObject.Game == null)
          return;
        this.textureObj = GameObject.Game.Content.Load<Texture2D>(this._Texture);
      }
    }

    public override IPrototype Clone()
    {
      TextureDrawer textureDrawer = (TextureDrawer) base.Clone();
      if (!string.IsNullOrEmpty(this.Texture))
        textureDrawer.textureObj = GameObject.Game.Content.Load<Texture2D>(this.Texture);
      return (IPrototype) textureDrawer;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
    {
      if (this.textureObj == null)
        return;
      if (!this.rotate)
      {
        spriteBatch.Draw(this.textureObj, gameObject.GetDrawingRectangle(), gameObject.Tint);
      }
      else
      {
        Rectangle drawingRectangle = gameObject.GetDrawingRectangle();
        spriteBatch.Draw(this.textureObj, new Vector2()
        {
          X = (float) drawingRectangle.X + (float) drawingRectangle.Width / 2f,
          Y = (float) drawingRectangle.Y + (float) drawingRectangle.Height / 2f
        }, new Rectangle?(), gameObject.Tint, (float) gameObject.GeneralDirection, new Vector2()
        {
          X = (float) this.textureObj.Width / 2f,
          Y = (float) this.textureObj.Height / 2f
        }, new Vector2()
        {
          X = (float) drawingRectangle.Width / (float) this.textureObj.Width,
          Y = (float) drawingRectangle.Height / (float) this.textureObj.Height
        }, SpriteEffects.None, 0.0f);
      }
    }
  }
}
