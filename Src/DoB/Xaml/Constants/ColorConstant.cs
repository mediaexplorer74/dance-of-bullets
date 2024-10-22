// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.ColorConstant
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Xaml
{
  public class ColorConstant : Constant<Color>
  {
    public string StringValue
    {
      get
      {
        return string.Format("{0},{1},{2},{3}", (object) this.Value.R, (object) this.Value.G, (object) this.Value.B, (object) this.Value.A);
      }
      set
      {
        byte[] array = ((IEnumerable<string>) value.Split(',')).Select<string, byte>((Func<string, byte>) (i => byte.Parse(i))).ToArray<byte>();
        this.Value = new Color(array[0], array[1], array[2], array[3]);
      }
    }
  }
}
