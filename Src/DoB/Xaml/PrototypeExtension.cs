// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.PrototypeExtension
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Windows.Markup;

#nullable disable
namespace DoB.Xaml
{
  public class PrototypeExtension : MarkupExtension
  {
    public string Name { get; set; }

    public PrototypeExtension()
    {
    }

    public PrototypeExtension(string name) => this.Name = name;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return Prototypes.All != null ? (object) Prototypes.All[this.Name].Clone() : (object) null;
    }
  }
}
