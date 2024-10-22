// Decompiled with JetBrains decompiler
// Type: DoB.Utility.EventCombinator
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace DoB.Utility
{
  public static class EventCombinator
  {
    private static int nextId = 0;
    private static Dictionary<string, EventCombination> combinators = new Dictionary<string, EventCombination>();

    public static string Combine(string[] eventList)
    {
      string str = "eventCombination_" + (object) EventCombinator.nextId++;
      EventCombinator.combinators[str] = new EventCombination((IEnumerable<string>) eventList)
      {
        Name = str
      };
      EventBroker.SubscribeOnce(str, (Action<string>) (n => EventCombinator.combinators.Remove(n)));
      return str;
    }
  }
}
