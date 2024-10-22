// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.Constant`1
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System.Windows.Markup;

#nullable disable
namespace DoB.Xaml
{
  [ContentProperty("Value")]
  public class Constant<T> : ConstantBase
  {
    public T Value { get; set; }

    public override object GetValue() => (object) this.Value;
  }
}
