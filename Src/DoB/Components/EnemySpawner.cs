// Decompiled with JetBrains decompiler
// Type: DoB.Components.EnemySpawner
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;

#nullable disable
namespace DoB.Components
{
  public class EnemySpawner : ComponentBase
  {
    private Cooldown cooldown = new Cooldown();
    private double totalRIncrement;

    public double CooldownMs { get; set; }

    public int Count { get; set; }

    public string ReferenceName { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public double XIncrement { get; set; }

    public double YIncrement { get; set; }

    public double RIncrement { get; set; }

    protected override void UpdateOverride(GameTime gameTime)
    {
      if (this.Count < 1)
        return;
      this.cooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if (!this.cooldown.IsElapsed)
        return;
      GameObject gameObject = (GameObject) Prototypes.All[this.ReferenceName].Clone();
      GameObject.Game.Objects.Add(gameObject);
      gameObject.X = this.X;
      gameObject.Y = this.Y;
      gameObject.R += this.totalRIncrement;
      this.X += this.XIncrement;
      this.Y += this.YIncrement;
      this.totalRIncrement += this.RIncrement;
      --this.Count;
      this.cooldown.Reset(this.CooldownMs);
    }
  }
}
