using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "e_", menuName = "Events/Game Event With Bool")]
public class GameEventWithBool : GameEventWithParam<bool>
{
	[Button]
	public override void Raise(bool t)
	{
		base.Raise(t);
	}
}