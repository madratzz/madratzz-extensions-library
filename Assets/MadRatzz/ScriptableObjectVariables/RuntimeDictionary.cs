using UnityEngine;
using System.Collections.Generic;


public abstract class RuntimeDictionary<TKey, TValue> : ScriptableObject
{
	public Dictionary<TKey, TValue> keyValuePairs;

	public void Clear()
	{
		keyValuePairs.Clear();
	}

	public bool Add(TKey key, TValue value)
	{
		if (keyValuePairs.ContainsKey(key))
		{
			return false;
		}
		else
		{
			keyValuePairs.Add(key, value);
			return true;
		}
	}

	public void AddOrUpdate(TKey key, TValue value)
	{
		if (keyValuePairs.ContainsKey(key))
		{
			keyValuePairs[key] = value;
		}
		else
		{
			Add(key, value);
		}
	}

	public bool Remove(TKey key)
	{
		if (keyValuePairs.ContainsKey(key))
		{
			keyValuePairs.Remove(key);
			return true;
		}
		else
		{
			return false;
		}
	}

	public TValue this[TKey key]
	{
		get { return keyValuePairs[key]; }
	}

	private void OnEnable()
	{
		keyValuePairs = new Dictionary<TKey, TValue>();
	}
}