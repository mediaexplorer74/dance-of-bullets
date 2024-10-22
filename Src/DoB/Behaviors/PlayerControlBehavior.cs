// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.PlayerControlBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Behaviors
{
  public class PlayerControlBehavior : Behavior
  {
    private bool lockMana;
    private bool lockPause;
    private double v = 300.0;
    private Cooldown gunCooldown = new Cooldown();

    public override void ResetTimers() => throw new NotSupportedException();

    public override IPrototype Clone()
    {
      PlayerControlBehavior playerControlBehavior = (PlayerControlBehavior) base.Clone();
      playerControlBehavior.gunCooldown = new Cooldown();
      return (IPrototype) playerControlBehavior;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
      Player player = (Player) gameObject;
      this.gunCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if ((((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.C) || ((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Space) || player.IsPaybackActive) && this.gunCooldown.IsElapsed)
      {
        ((Player) gameObject).Shoot();
        this.gunCooldown.Reset(player.IsPaybackActive ? 10.0 : 120.0);
      }
      TimeSpan elapsedGameTime;
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Up))
      {
        GameObject gameObject1 = gameObject;
        double moveY = gameObject1.MoveY;
        double v = this.v;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalMilliseconds = elapsedGameTime.TotalMilliseconds;
        double num = GameSpeedManager.ApplySpeed(v, totalMilliseconds);
        gameObject1.MoveY = moveY - num;
      }
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Down))
      {
        GameObject gameObject2 = gameObject;
        double moveY = gameObject2.MoveY;
        double v = this.v;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalMilliseconds = elapsedGameTime.TotalMilliseconds;
        double num = GameSpeedManager.ApplySpeed(v, totalMilliseconds);
        gameObject2.MoveY = moveY + num;
      }
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Left))
      {
        GameObject gameObject3 = gameObject;
        double moveX = gameObject3.MoveX;
        double v = this.v;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalMilliseconds = elapsedGameTime.TotalMilliseconds;
        double num = GameSpeedManager.ApplySpeed(v, totalMilliseconds);
        gameObject3.MoveX = moveX - num;
      }
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Right))
      {
        GameObject gameObject4 = gameObject;
        double moveX = gameObject4.MoveX;
        double v = this.v;
        elapsedGameTime = gameTime.ElapsedGameTime;
        double totalMilliseconds = elapsedGameTime.TotalMilliseconds;
        double num = GameSpeedManager.ApplySpeed(v, totalMilliseconds);
        gameObject4.MoveX = moveX + num;
      }
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.X))
      {
        if (this.lockMana)
          return;
        if (player.IsManaActive)
          player.StopMana();
        else
          player.StartMana();
        this.lockMana = true;
      }
      else
        this.lockMana = false;
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Y) || ((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.Z))
        player.ActivatePayback();
      if (((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.P) || ((IEnumerable<Keys>) pressedKeys).Contains<Keys>(Keys.B))
      {
        if (this.lockPause)
          return;
        GameSpeedManager.TogglePause();
        this.lockPause = true;
      }
      else
        this.lockPause = false;
    }
  }
}
