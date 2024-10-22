// Decompiled with JetBrains decompiler
// Type: DoB.Utility.Health
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;

#nullable disable
namespace DoB.Utility
{
  public class Health : Consumable
  {
    private GameObject gameObject;
    private double ScoreValue;

    public Health(GameObject g, double lives)
      : base(lives)
    {
      this.gameObject = g;
      if (g is Player)
        return;
      this.ScoreValue = lives * 100.0;
    }

    public bool Hit()
    {
      if (--this.Amount != 0.0)
        return false;
      this.gameObject.RemoveSelf();
      ScoreKeeper.AddScore(this.ScoreValue);
      return true;
    }
  }
}
