using UnityEngine;

[CreateAssetMenu(fileName = "v_", menuName = "Variables/String")]
public class String : ScriptableObject
{
	[TextArea(1, 20)] [SerializeField] protected string Value;
	[TextArea(1, 20)] [SerializeField] protected string DefaultValue;
	[SerializeField] protected bool ResetToDefaultOnPlay = true;

	private void OnEnable()
	{
		if (ResetToDefaultOnPlay)
		{
			Value = DefaultValue;
		}
	}

	public string GetValue()
	{
		return Value;
	}

	public virtual void SetValue(string value)
	{
		Value = value;
	}

	public virtual void SetValue(String value)
	{
		Value = value.Value;
	}

	public virtual void ResetToDefault()
	{
		Value = DefaultValue;
	}

	public static implicit operator string(String str)
	{
		return str.Value;
	}
}