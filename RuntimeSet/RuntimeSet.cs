using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified for LD42 Jam
// Author: Nerzal
// ----------------------------------------------------------------------------

namespace Sets {
  public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T> {
    public List<T> items = new List<T>();
    public int index = -1;

    public Action itemsChanged;

    public T this[int i] {
      get { return this.items[i]; }
      set { this.items[i] = value; }
    }

    public void Initialize() {
      this.items = new List<T>();
      this.index = -1;
      this.Clear();
      this.itemsChanged?.Invoke();
    }

    public virtual void Add(T thing) {
      if (this.items.Contains(thing)) {
        return;
      }

      this.items.Add(thing);
      this.index++;
      this.itemsChanged?.Invoke();
    }

    public virtual void AddRange(IEnumerable<T> things) {
      this.items.AddRange(things);
    }

    public virtual void Remove(T thing) {
      if (!this.items.Contains(thing)) {
        return;
      }

      this.items.Remove(thing);
      this.index--;
      this.itemsChanged?.Invoke();
    }

    public virtual void Clear() {
      this.items.Clear();
      this.itemsChanged?.Invoke();
    }

    public bool Contains(T item) {
      foreach (T item1 in this.items) {
        if (item1.Equals(item)) {
          return true;
        }
      }

      return false;
    }

    public int Count() {
      return this.items.Count;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() {
      return this.items.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }
}