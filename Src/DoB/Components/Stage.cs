// Decompiled with JetBrains decompiler
// Type: DoB.Components.Stage
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.Components
{
  [ContentProperty("Components")]
  public class Stage : IComponent
  {
    private string _BackgroundTextures;
    private string _EndsOnEvent;

    public bool IsEnded { get; set; }

    public string BackgroundTexture { get; set; }

    public string BackgroundTextures
    {
      get => this._BackgroundTextures;
      set
      {
        if (this._BackgroundTextures == value)
          return;
        this._BackgroundTextures = value;
        this.BackgroundTextureArray = this._BackgroundTextures.Split(';');
      }
    }

    public string[] BackgroundTextureArray { get; set; }

    public string EndsOnEvent
    {
      get => this._EndsOnEvent;
      set
      {
        this._EndsOnEvent = value;
        EventBroker.SubscribeOnce(value, (Action<string>) (s => this.IsEnded = true));
      }
    }

    public List<IComponent> Components { get; set; }

    public Stage() => this.Components = new List<IComponent>();

    public void Update(GameTime gameTime)
    {
      for (int index = 0; index < this.Components.Count; ++index)
        this.Components[index].Update(gameTime);
    }
  }
}
