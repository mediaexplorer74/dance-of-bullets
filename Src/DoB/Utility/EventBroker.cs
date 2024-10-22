// Decompiled with JetBrains decompiler
// Type: DoB.Utility.EventBroker
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace DoB.Utility
{
  public static class EventBroker
  {
    private static Dictionary<string, List<Action<string>>> subscriptions = new Dictionary<string, List<Action<string>>>();

    public static bool IsEnabled { get; set; }

    static EventBroker() => EventBroker.IsEnabled = true;

    public static void SubscribeOnce(string eventName, Action<string> handler)
    {
      if (!EventBroker.subscriptions.ContainsKey(eventName))
        EventBroker.subscriptions[eventName] = new List<Action<string>>();
      EventBroker.subscriptions[eventName].Add(handler);
    }

    public static void FireEvent(string eventName)
    {
      if (!EventBroker.IsEnabled || !EventBroker.subscriptions.ContainsKey(eventName))
        return;
      for (int index = 0; index < EventBroker.subscriptions[eventName].Count; ++index)
        EventBroker.subscriptions[eventName][index](eventName);
      EventBroker.subscriptions.Remove(eventName);
    }
  }
}
