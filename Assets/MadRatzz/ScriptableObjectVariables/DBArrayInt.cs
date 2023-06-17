using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "DBArrayInt", menuName = "Variables/DBList of int")]
public class DBArrayInt : DBString
{
	public CustomArrayInt List;

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

	public virtual bool Add(int number)
	{
		if (List.List.Contains(number))
		{
			return false;
		}

		List.List.Add(number);
		SaveArray();
		return true;
	}

	public virtual bool Contains(int number)
	{
		if (List.List.Contains(number))
			return true;
		return false;
	}

	public void InsertAtIndex(int index, int number)
	{
		List.List.Insert(index, number);
		SaveArray();
	}

	public bool Remove(int number)
	{
		bool result = List.List.Remove(number);
		SaveArray();
		return result;
	}

	public int this[int index] => List.List[index];

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
		if (JsonUtility.FromJson<CustomArrayInt>(Value) != null) List = JsonUtility.FromJson<CustomArrayInt>(Value);
	}
}


[Serializable]
public class CustomArrayInt
{
	[Searchable] public List<int> List = new();
}