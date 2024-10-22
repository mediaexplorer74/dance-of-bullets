// Decompiled with JetBrains decompiler
// Type: DoB.Utility.GameSpeedManager
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

#nullable disable
namespace DoB.Utility
{
  public static class GameSpeedManager
  {
    public static double GlobalModifier { get; set; } = 1.0;

    public static double DifficultyMultiplier { get; set; } = 1.0;

    public static bool IsPaused { get; set; }

    public static double ApplySpeed(double v, double ms)
    {
      return !GameSpeedManager.IsPaused ? v * (ms / 1000.0) * GameSpeedManager.GlobalModifier * GameSpeedManager.DifficultyMultiplier : 0.0;
    }

    public static void TogglePause() => GameSpeedManager.IsPaused = !GameSpeedManager.IsPaused;

    public static void ReduceDifficulty()
    {
      if (GameSpeedManager.DifficultyMultiplier <= 0.8)
        return;
      GameSpeedManager.DifficultyMultiplier -= GameSpeedManager.DifficultyMultiplier > 1.0 ? 0.4 : 0.2;
    }

    public static void IncreaseDifficulty()
    {
      if (GameSpeedManager.DifficultyMultiplier >= 1.5)
        return;
      GameSpeedManager.DifficultyMultiplier += 0.2;
    }
  }
}
