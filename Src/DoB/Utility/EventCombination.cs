// Decompiled with JetBrains decompiler
// Type: DoB.Utility.EventCombination
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Utility
{
  public class EventCombination
  {
    public List<string> Events { get; private set; }

    public string Name { get; set; }

    public EventCombination(IEnumerable<string> events)
    {
      this.Events = events.ToList<string>();
      for (int index = 0; index < this.Events.Count; ++index)
        EventBroker.SubscribeOnce(this.Events[index], new Action<string>(this.HandleEvent));
    }

    private void HandleEvent(string eventName)
    {
      this.Events.Remove(eventName);
      if (this.Events.Count != 0)
        return;
      EventBroker.FireEvent(this.Name);
    }
  }
}
