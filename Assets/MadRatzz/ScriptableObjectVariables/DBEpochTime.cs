using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "v_", menuName = "Variables/EPOCH Time Persistent")]
public class DBEpochTime : DBInt
{
	const int OneDayEpochValue = 86400;
	private int _lastSyncTime = 0;

	public override void SetValue(int value)
	{
		base.SetValue(value);
		ResetLastSyncTime();
	}

	public override void SetValue(Int value)
	{
		base.SetValue(value);
		ResetLastSyncTime();
	}

	private void OnEnable()
	{
		Load();
	}

	[Button]
	private void AddDay()
	{
		ApplyChange(OneDayEpochValue);
	}

	[Button]
	private void SubtractDay()
	{
		ApplyChange(-OneDayEpochValue);
	}

	[Button]
	private void AddDays(int days)
	{
		for (int i = 0; i < days; i++)
		{
			AddDay();
		}
	}

	[Button]
	private void SubtractDays(int days)
	{
		for (int i = 0; i < days; i++)
		{
			SubtractDay();
		}
	}

	public void ResetLastSyncTime()
	{
		_lastSyncTime = (int)Time.realtimeSinceStartup;
	}

	public int GetTimeSinceLastUpdate()
	{
		return Value + ((int)Time.realtimeSinceStartup - _lastSyncTime);
	}
}