// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.Prototypes
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System.Collections.Generic;
using System.IO;
using System.Xaml;

#nullable disable
namespace DoB.Xaml
{
  public class Prototypes : CustomDictionary<string, IPrototype>
  {
    public static Prototypes All { get; set; }

    static Prototypes() => Prototypes.All = new Prototypes();

    public static Prototypes LoadFrom(string path)
    {
      return (Prototypes) XamlServices.Parse(File.ReadAllText(path));
    }

    protected override void OnKeyAssigned(string key)
    {
      base.OnKeyAssigned(key);
      if (this == Prototypes.All)
        return;
      Prototypes.All.AsDictionary().Add(key, this[key]);
    }

    private IDictionary<string, IPrototype> AsDictionary()
    {
      return (IDictionary<string, IPrototype>) this;
    }
  }
}
