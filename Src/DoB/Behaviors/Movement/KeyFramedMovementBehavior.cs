// Decompiled with JetBrains decompiler
// Type: DoB.Behaviors.KeyFramedMovementBehavior
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

#nullable disable
namespace DoB.Behaviors
{
  [ContentProperty("KeyFrames")]
  public class KeyFramedMovementBehavior : Behavior
  {
    private int activeIndex;
    private float keyFrameStartX;
    private float keyFrameStartY;
    private float keyFrameStartR;
    private Cooldown keyFrameDuration;
    private Cooldown keyFrameHold;
    private bool isHoldingFrame;

    public List<KeyFrame> KeyFrames { get; set; }

    public int EasingPow { get; set; }

    public KeyFramedMovementBehavior()
    {
      this.KeyFrames = new List<KeyFrame>();
      this.keyFrameDuration = new Cooldown();
      this.keyFrameHold = new Cooldown();
      this.EasingPow = 1;
    }

    public override void ResetTimers() => throw new NotSupportedException();

    public override IPrototype Clone()
    {
      KeyFramedMovementBehavior movementBehavior = (KeyFramedMovementBehavior) base.Clone();
      movementBehavior.KeyFrames = this.KeyFrames.ToList<KeyFrame>();
      for (int index = 0; index < this.KeyFrames.Count; ++index)
        movementBehavior.KeyFrames[index] = this.KeyFrames[index];
      movementBehavior.keyFrameDuration = new Cooldown();
      this.keyFrameHold = new Cooldown();
      movementBehavior.activeIndex = 0;
      movementBehavior.isHoldingFrame = false;
      return (IPrototype) movementBehavior;
    }

    public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
    {
      base.OnFirstUpdate(gameTime, gameObject);
      this.KeyFrameStarted(this.KeyFrames[0], gameTime, gameObject);
    }

    public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
    {
      base.UpdateOverride(gameTime, gameObject);
      if (this.activeIndex >= this.KeyFrames.Count)
        return;
      KeyFrame keyFrame = this.KeyFrames[this.activeIndex];
      this.keyFrameDuration.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      this.keyFrameHold.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
      if (this.keyFrameDuration.IsElapsed)
      {
        if (!this.isHoldingFrame)
        {
          this.isHoldingFrame = true;
          this.keyFrameHold.Reset(keyFrame.HoldMs);
        }
        else
        {
          if (!this.keyFrameHold.IsElapsed)
            return;
          ++this.activeIndex;
          if (this.activeIndex >= this.KeyFrames.Count)
            return;
          this.KeyFrameStarted(this.KeyFrames[this.activeIndex], gameTime, gameObject);
        }
      }
      else
      {
        float progress = this.keyFrameDuration.Progress;
        double keyFrameStartX = (double) this.keyFrameStartX;
        double? nullable = keyFrame.ToX;
        double num1 = nullable ?? gameObject.Ox + keyFrame.TranslateX;
        double amount1 = this.Ease((double) progress);
        float num2 = MathHelper.Lerp((float) keyFrameStartX, (float) num1, (float) amount1);
        double keyFrameStartY = (double) this.keyFrameStartY;
        nullable = keyFrame.ToY;
        double num3 = nullable ?? gameObject.Oy + keyFrame.TranslateY;
        double amount2 = this.Ease((double) progress);
        float num4 = MathHelper.Lerp((float) keyFrameStartY, (float) num3, (float) amount2);
        nullable = keyFrame.ToR;
        if (!nullable.HasValue)
        {
          nullable = keyFrame.FromR;
          if (!nullable.HasValue)
            goto label_12;
        }
        GameObject gameObject1 = gameObject;
        nullable = keyFrame.FromR;
        double num5 = nullable ?? (double) this.keyFrameStartR;
        nullable = keyFrame.ToR;
        double num6 = nullable ?? (double) this.keyFrameStartR;
        double amount3 = this.Ease((double) progress);
        double num7 = (double) MathHelper.Lerp((float) num5, (float) num6, (float) amount3);
        gameObject1.R = num7;
label_12:
        gameObject.MoveX += (double) num2 - gameObject.X;
        gameObject.MoveY += (double) num4 - gameObject.Y;
      }
    }

    private void KeyFrameStarted(KeyFrame akf, GameTime gameTime, GameObject gameObject)
    {
      this.isHoldingFrame = false;
      this.keyFrameDuration.Reset(akf.DurationMs);
      this.keyFrameStartX = (float) gameObject.X;
      this.keyFrameStartY = (float) gameObject.Y;
      this.keyFrameStartR = (float) gameObject.R;
    }

    private double Ease(double amount)
    {
      return amount > 0.5 ? (1.0 - this.EaseIn((1.0 - amount) * 2.0)) / 2.0 + 0.5 : this.EaseIn(amount * 2.0) / 2.0;
    }

    private double EaseIn(double amount) => Math.Pow(amount, (double) this.EasingPow);
  }
}
