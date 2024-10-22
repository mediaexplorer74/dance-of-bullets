// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.ConstantBase
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

#nullable disable
namespace DoB.Xaml
{
  public abstract class ConstantBase : IPrototype
  {
    public abstract object GetValue();

    public IPrototype Clone() => (IPrototype) this.MemberwiseClone();
  }
}
