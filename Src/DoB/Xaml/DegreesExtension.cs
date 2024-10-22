// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.DegreesExtension
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Windows.Markup;

#nullable disable
namespace DoB.Xaml
{
  public class DegreesExtension : MarkupExtension
  {
    public double Degrees { get; set; }

    public DegreesExtension()
    {
    }

    public DegreesExtension(double degrees) => this.Degrees = degrees;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) (Math.PI / 180.0 * this.Degrees);
    }
  }
}
