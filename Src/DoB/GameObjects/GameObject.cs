// Decompiled with JetBrains decompiler
// Type: DoB.GameObjects.GameObject
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Behaviors;
using DoB.Drawers;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.GameObjects
{
  [ContentProperty("Behaviors")]
  public class GameObject : IPrototype
  {
    private TextureDrawer baseTextureDrawer;
    public double MoveX;
    public double MoveY;
    public static DoBGame Game;
    private bool isFirstUpdate = true;
    public bool IsMarkedForRemoval;

    public virtual void OnIsEnabledChanged()
    {
    }

    public double X { get; set; }

    public double Y { get; set; }

    public double R { get; set; }

    public string BaseTexture
    {
      get => this.baseTextureDrawer.Texture;
      set => this.baseTextureDrawer.Texture = value;
    }

    public Color Tint { get; set; }

    public bool AutoUpdateGeneralDirection { get; set; }

    public double Ox { get; private set; }

    public double Oy { get; private set; }

    public double GeneralDirection { get; set; }

    public List<Drawer> Drawers { get; set; }

    public List<Behavior> Behaviors { get; set; }

    public GameObject()
    {
      this.AutoUpdateGeneralDirection = true;
      this.Tint = Color.White;
      this.Drawers = new List<Drawer>();
      this.baseTextureDrawer = new TextureDrawer();
      this.Drawers.Add((Drawer) this.baseTextureDrawer);
      this.Behaviors = new List<Behavior>();
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this.Drawers.Count; ++index)
        this.Drawers[index].Draw(gameTime, spriteBatch, this);
    }

    public virtual void OnFirstUpdate(GameTime gameTime)
    {
      this.Ox = this.X;
      this.Oy = this.Y;
    }

    public virtual void Update(GameTime gameTime)
    {
      if (this.isFirstUpdate)
      {
        this.OnFirstUpdate(gameTime);
        this.isFirstUpdate = false;
      }
      this.MoveX = 0.0;
      this.MoveY = 0.0;
      for (int index = 0; index < this.Behaviors.Count; ++index)
        this.Behaviors[index].Update(gameTime, this);
      this.X += this.MoveX;
      this.Y += this.MoveY;
      if (!this.AutoUpdateGeneralDirection)
        return;
      this.GeneralDirection = (this.X - this.Ox >= 0.0 ? 0.0 : Math.PI) + Math.Atan((this.Y - this.Oy) / (this.X - this.Ox));
    }

    public Rectangle GetRectangle(bool applyPendingMovement = false)
    {
      return !applyPendingMovement ? new Rectangle((int) (this.X - this.R), (int) (this.Y - this.R), (int) (2.0 * this.R), (int) (2.0 * this.R)) : new Rectangle((int) (this.X + this.MoveX - this.R), (int) (this.Y + this.MoveY - this.R), (int) (2.0 * this.R), (int) (2.0 * this.R));
    }

    public virtual Rectangle GetDrawingRectangle() => this.GetRectangle();

    public virtual void RemoveSelf()
    {
      this.IsMarkedForRemoval = true;
      for (int index = 0; index < this.Behaviors.Count; ++index)
        this.Behaviors[index].OnRemoval(this);
    }

    public virtual IPrototype Clone()
    {
      GameObject gameObject = (GameObject) this.MemberwiseClone();
      gameObject.Behaviors = new List<Behavior>();
      for (int index = 0; index < this.Behaviors.Count; ++index)
        gameObject.Behaviors.Add((Behavior) this.Behaviors[index].Clone());
      gameObject.Drawers = new List<Drawer>();
      for (int index = 0; index < this.Drawers.Count; ++index)
        gameObject.Drawers.Add((Drawer) this.Drawers[index].Clone());
      gameObject.baseTextureDrawer = (TextureDrawer) gameObject.Drawers[0];
      gameObject.isFirstUpdate = true;
      gameObject.IsMarkedForRemoval = false;
      return (IPrototype) gameObject;
    }
  }
}
