using UnityEngine;

[CreateAssetMenu(fileName = "v_", menuName = "Variables/Bool Persistent With Event")]
public class DBBoolWithEvent : DBBool
{
	[SerializeField] protected GameEvent ValueChanged;

	public override void SetValue(bool value)
	{
		base.SetValue(value);
		ValueChanged.Invoke();
	}

	public override void SetValue(Bool value)
	{
		base.SetValue(value);
		ValueChanged.Invoke();
	}

	public void AddListener(GameEventHandler callback)
	{
		ValueChanged.Handler += callback;
	}

	public void RemoveListener(GameEventHandler callback)
	{
		ValueChanged.Handler -= callback;
	}
}