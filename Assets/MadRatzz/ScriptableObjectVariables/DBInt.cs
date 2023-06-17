using System;
using System.Globalization;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "v_", menuName = "Variables/Int Persistent")]
public class DBInt : Int, IDBVariable
{
	[SerializeField] public bool CloudSyncEnabled = true;
	[SerializeField] protected string Key;

	public bool SyncEnabled => CloudSyncEnabled;


	//TODO @Raza you can replace this with your own implementation -> Need to Create LiveOpsDBInt
	public string PlayerPrefsKey
	{
		get { return Key; }
	}

	public void Refresh()
	{
		Load();
	}

	private void OnEnable()
	{
		Load();
	}

	public void SetKey(string key)
	{
		Key = key;
	}

	public string GetKey()
	{
		return Key;
	}

	public override void SetValue(int value)
	{
		base.SetValue(value);
		Save();
	}

	public override void SetValue(Int value)
	{
		base.SetValue(value);
		Save();
	}

	public override void ApplyChange(int amount)
	{
		base.ApplyChange(amount);
		Save();
	}

	public override void ApplyChange(Int amount)
	{
		base.ApplyChange(amount);
		Save();
	}

	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	protected void Save()
	{
		DBManager.SetInt(this, Key, Value);
	}


	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	protected virtual void Load()
	{
		if (!string.IsNullOrEmpty(Key) && DBManager.HasKey(this, Key))
		{
			Value = DBManager.GetInt(this, Key);
		}
		else
		{
			if (ResetToDefaultOnPlay)
			{
				Value = DefaultValue;
			}
			else
			{
				Value = 0;
			}
		}
	}


	void IDBVariable.Update(object value)
	{
		int integer = Value;

		if (value is int)
		{
			integer = Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}
		else if (value is long)
		{
			integer = Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		SetValue(integer);
	}

	object IDBVariable.GetValue()
	{
		return GetValue();
	}
}