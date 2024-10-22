// Decompiled with JetBrains decompiler
// Type: DoB.Components.DebugControls
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Components
{
  public class DebugControls : IComponent
  {
    private bool waitForRelease;
    private Dictionary<Keys, Action<Keys[]>> keyBindings = new Dictionary<Keys, Action<Keys[]>>()
    {
      {
        Keys.F,
        (Action<Keys[]>) (keys => GameSpeedManager.GlobalModifier += ((IEnumerable<Keys>) keys).Contains<Keys>(Keys.LeftShift) ? 0.5 : 0.1)
      },
      {
        Keys.S,
        (Action<Keys[]>) (keys => GameSpeedManager.GlobalModifier -= ((IEnumerable<Keys>) keys).Contains<Keys>(Keys.LeftShift) ? 0.5 : 0.1)
      },
      {
        Keys.D,
        (Action<Keys[]>) (keys => GameSpeedManager.GlobalModifier = 1.0)
      },
      {
        Keys.H,
        (Action<Keys[]>) (keys =>
        {
          GameObject.Game.Player.Health.Refill();
          GameObject.Game.Player.Mana.Refill();
          GameObject.Game.Player.Payback.Refill();
        })
      },
      {
        Keys.A,
        (Action<Keys[]>) (keys => GameObject.Game.Debug_ToggleShowBulletPaths())
      },
      {
        Keys.N,
        (Action<Keys[]>) (keys => GameObject.Game.Debug_SkipStage())
      },
      {
        Keys.I,
        (Action<Keys[]>) (keys => GameObject.Game.Player.debug_IsInvincible = !GameObject.Game.Player.debug_IsInvincible)
      },
      {
        Keys.K,
        (Action<Keys[]>) (keys => GameObject.Game.ClearHostileObjects<Collideable>())
      }
    };

    public void Update(GameTime gameTime)
    {
      Keys[] keys = Keyboard.GetState().GetPressedKeys();
      Keys key = this.keyBindings.Keys.FirstOrDefault<Keys>((Func<Keys, bool>) (k => ((IEnumerable<Keys>) keys).Contains<Keys>(k)));
      if (key != Keys.None)
      {
        if (this.waitForRelease)
          return;
        this.keyBindings[key](keys);
        this.waitForRelease = true;
      }
      else
        this.waitForRelease = false;
    }
  }
}
