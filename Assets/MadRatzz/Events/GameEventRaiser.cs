using UnityEngine;

public class GameEventRaiser : MonoBehaviour
{
	[SerializeField] private GameEvent GameEvent;

	public void InvokeEvent()
	{
		GameEvent.Invoke();
	}
}