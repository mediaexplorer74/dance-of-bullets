// Decompiled with JetBrains decompiler
// Type: DoB.Utility.Consumable
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

#nullable disable
namespace DoB.Utility
{
  public class Consumable
  {
    public double Amount { get; set; }

    public double OriginalAmount { get; private set; }

    public Consumable(double lives)
    {
      this.Amount = lives;
      this.OriginalAmount = lives;
    }

    public void Refill() => this.Amount = this.OriginalAmount;

    public bool HasRunOut => this.Amount <= 0.0;
  }
}
