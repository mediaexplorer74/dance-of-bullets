// Decompiled with JetBrains decompiler
// Type: DoB.Program
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;

#nullable disable
namespace DoB
{
  public static class Program
  {
    [STAThread]
    private static void Main()
    {
      using (DoBGame doBgame = new DoBGame())
        doBgame.Run();
    }
  }
}
