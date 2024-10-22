// Decompiled with JetBrains decompiler
// Type: DoB.Xaml.CustomDictionary`2
// Assembly: DoB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BB15B03-F383-4BF7-8B15-796548A1FAD2
// Assembly location: C:\Users\Admin\Desktop\Portable\DoB\DoB.exe

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace DoB.Xaml
{
  public class CustomDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
  {
    private Dictionary<TKey, TValue> store = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
      get => this.store[key];
      set
      {
        this.store[key] = value;
        this.OnKeyAssigned(key);
      }
    }

    protected virtual void OnKeyAssigned(TKey key)
    {
    }

    protected virtual void OnKeyRemoved(TKey key)
    {
    }

    protected virtual void OnCleared()
    {
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
    {
      this.store.Add(key, value);
      this.OnKeyAssigned(key);
    }

    bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.store.ContainsKey(key);

    ICollection<TKey> IDictionary<TKey, TValue>.Keys => (ICollection<TKey>) this.store.Keys;

    bool IDictionary<TKey, TValue>.Remove(TKey key)
    {
      int num = this.store.Remove(key) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.OnKeyRemoved(key);
      return num != 0;
    }

    bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
    {
      return this.store.TryGetValue(key, out value);
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values => (ICollection<TValue>) this.store.Values;

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      ((ICollection<KeyValuePair<TKey, TValue>>) this.store).Add(item);
      this.OnKeyAssigned(item.Key);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear()
    {
      this.store.Clear();
      this.OnCleared();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      return this.store.Contains<KeyValuePair<TKey, TValue>>(item);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      ((ICollection<KeyValuePair<TKey, TValue>>) this.store).CopyTo(array, arrayIndex);
    }

    int ICollection<KeyValuePair<TKey, TValue>>.Count => this.store.Count;

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
    {
      get => ((ICollection<KeyValuePair<TKey, TValue>>) this.store).IsReadOnly;
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      int num = ((ICollection<KeyValuePair<TKey, TValue>>) this.store).Remove(item) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.OnKeyRemoved(item.Key);
      return num != 0;
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) this.store.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.store.GetEnumerator();
  }
}
