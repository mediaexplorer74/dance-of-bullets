// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.Stages
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Components;
using System.Collections.Generic;

#nullable disable
namespace DoB.Xaml
{
  public class Stages : List<Stage>
  {
    public static Stages Current { get; set; }

    public List<Prototypes> PrototypePacks { get; set; }

    public Stages()
    {
      Stages.Current = this;
      this.PrototypePacks = new List<Prototypes>();
    }
  }
}
