using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "DBArrayIntInt", menuName = "Variables/DBList of CustomVector2Int")]
public class DBArrayIntInt : DBString
{
	public CustomArrayIntInt List;

	private void OnEnable()
	{
		if (ResetToDefaultOnPlay) Clear();

		Load();
	}

	[Button]
	public new void Refresh()
	{
		Load();
	}

	public int Count => List.List.Count;

	[Button]
	public void Clear()
	{
		if (List != null) List.List.Clear();
	}

	public new void ResetToDefault()
	{
		Clear();
		SaveArray();
	}

	public virtual bool Add(CustomVector2Int value)
	{
		if (List.List.Contains(value))
		{
			return false;
		}

		List.List.Add(value);
		SaveArray();
		return true;
	}

	public virtual bool Contains(CustomVector2Int value)
	{
		if (List.List.Contains(value))
			return true;
		return false;
	}

	public void InsertAtIndex(int index, CustomVector2Int value)
	{
		List.List.Insert(index, value);
		SaveArray();
	}

	public bool Remove(CustomVector2Int value)
	{
		bool result = List.List.Remove(value);
		SaveArray();
		return result;
	}

	public CustomVector2Int this[int index] => List.List[index];

	[Button]
	protected void SaveArray()
	{
		string saveString = JsonUtility.ToJson(List);
		SetValue(saveString);
	}

	[Button]
	protected override void Load()
	{
		base.Load();
		if (JsonUtility.FromJson<CustomArrayIntInt>(Value) != null)
			List = JsonUtility.FromJson<CustomArrayIntInt>(Value);
	}
}


[Serializable]
public class CustomArrayIntInt
{
	[Searchable] public List<CustomVector2Int> List = new();
}

[Serializable]
public struct CustomVector2Int
{
	public int x, y;
}