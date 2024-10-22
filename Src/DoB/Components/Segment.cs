// Decompiled with JetBrains decompiler
// Type: DoB.Components.Segment
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.Components
{
  [ContentProperty("Components")]
  public class Segment : ComponentBase
  {
    public List<IComponent> Components { get; set; }

    public Segment() => this.Components = new List<IComponent>();

    protected override void UpdateOverride(GameTime gameTime)
    {
      for (int index = 0; index < this.Components.Count; ++index)
        this.Components[index].Update(gameTime);
    }
  }
}
