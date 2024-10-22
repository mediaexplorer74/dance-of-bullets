// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.EventsExtension
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using DoB.Utility;
using System;
using System.Windows.Markup;

#nullable disable
namespace DoB.Xaml
{
  public class EventsExtension : MarkupExtension
  {
    public string EventsCSV { get; set; }

    public EventsExtension(string eventsCSV) => this.EventsCSV = eventsCSV;

    public EventsExtension()
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) EventCombinator.Combine(this.EventsCSV.Split(';'));
    }
  }
}
