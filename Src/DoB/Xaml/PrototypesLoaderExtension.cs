// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.PrototypesLoaderExtension
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Collections.Generic;
using System.Windows.Markup;

#nullable disable
namespace DoB.Xaml
{
  public class PrototypesLoaderExtension : MarkupExtension
  {
    public string FileNames { get; set; }

    public PrototypesLoaderExtension()
    {
    }

    public PrototypesLoaderExtension(string fileNames) => this.FileNames = fileNames;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      List<Prototypes> prototypesList = new List<Prototypes>();
      string fileNames = this.FileNames;
      char[] chArray = new char[1]{ ';' };
      foreach (string str in fileNames.Split(chArray))
        prototypesList.Add(Prototypes.LoadFrom("StageData\\" + str + ".xaml"));
      return (object) prototypesList;
    }
  }
}
