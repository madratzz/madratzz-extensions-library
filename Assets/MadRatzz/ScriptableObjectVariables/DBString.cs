using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "v_", menuName = "Variables/String Persistent")]
[InlineEditor()]
public class DBString : String, IDBVariable
{
	[SerializeField] public bool CloudSyncEnabled = true;
	[SerializeField] protected string Key;

	public bool SyncEnabled => CloudSyncEnabled;

	private void OnEnable()
	{
		Load();
	}

	public void Refresh()
	{
		Load();
	}

	public override void SetValue(string value)
	{
		base.SetValue(value);
		Save();
	}

	public override void SetValue(String value)
	{
		base.SetValue(value);
		Save();
	}

	[Button]
	protected virtual void Save()
	{
		DBManager.SetString(this, Key, Value);
	}

	public string GetKey()
	{
		return Key;
	}

	[Button]
	protected virtual void Load()
	{
		if (!string.IsNullOrEmpty(Key) && DBManager.HasKey(this, Key))
		{
			Value = DBManager.GetString(this, Key);
		}
		else
		{
			if (ResetToDefaultOnPlay)
			{
				Value = DefaultValue;
			}
			else
			{
				Value = string.Empty;
			}
		}
	}

	void IDBVariable.Update(object value)
	{
		if (value is string)
		{
			SetValue((string)value);
		}
	}

	object IDBVariable.GetValue()
	{
		return GetValue();
	}
}