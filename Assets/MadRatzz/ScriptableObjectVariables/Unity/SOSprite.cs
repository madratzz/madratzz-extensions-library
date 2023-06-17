using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "v_", menuName = "Variables/Sprite")]
public class SOSprite : ScriptableObject
{
	[PreviewField] [SerializeField] protected Sprite Value;
	[PreviewField] [SerializeField] protected Sprite DefaultValue;

	public Sprite GetValue()
	{
		if (Value == null)
		{
			return DefaultValue;
		}

		return Value;
	}

	public void SetValue(Sprite sprite)
	{
		Value = sprite;
	}
}