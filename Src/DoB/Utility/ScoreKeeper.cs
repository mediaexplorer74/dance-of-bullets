// Decompiled with JetBrains decompiler
// Type: DoB.Utility.ScoreKeeper
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

#nullable disable
namespace DoB.Utility
{
  internal static class ScoreKeeper
  {
    public static double Score;

    public static void AddScore(double scoreValue)
    {
      ScoreKeeper.Score += scoreValue * GameSpeedManager.DifficultyMultiplier;
    }
  }
}
