using UnityEngine;

public class GameEventRaiserOnEnable : MonoBehaviour
{
	[SerializeField] private GameEvent GameEvent;

	private void OnEnable()
	{
		GameEvent.Invoke();
	}
}