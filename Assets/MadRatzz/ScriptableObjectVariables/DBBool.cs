using System;
using System.Globalization;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "v_", menuName = "Variables/Bool Persistent")]
public class DBBool : Bool, IDBVariable
{
	[SerializeField] public bool CloudSyncEnabled = true;
	[SerializeField] protected string Key;

	public bool SyncEnabled => CloudSyncEnabled;

	private void OnEnable()
	{
		Load();
	}

	[Button]
	public override void SetValue(bool value)
	{
		base.SetValue(value);
		Save();
	}

	public override void SetValue(Bool value)
	{
		base.SetValue(value);
		Save();
	}

	protected void Save()
	{
		DBManager.SetBool(this, Key, Value);
	}

	protected void Load()
	{
		if (!string.IsNullOrEmpty(Key) && DBManager.HasKey(this, Key))
		{
			Value = DBManager.GetBool(this, Key);
		}
		else
		{
			if (ResetToDefaultOnPlay)
			{
				Value = DefaultValue;
			}
			else
			{
				Value = false;
			}
		}
	}

	void IDBVariable.Update(object value)
	{
		bool boolean = Value;

		if (value is int)
		{
			boolean = Convert.ToInt32(value, CultureInfo.InvariantCulture) == 1;
		}
		else if (value is long)
		{
			boolean = Convert.ToInt32(value, CultureInfo.InvariantCulture) == 1;
		}
		else if (value is bool)
		{
			boolean = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		SetValue(boolean);
	}

	object IDBVariable.GetValue()
	{
		return GetValue() ? 1 : 0;
	}
}