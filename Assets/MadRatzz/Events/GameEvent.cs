using Sirenix.OdinInspector;
using UnityEngine;

public delegate void GameEventHandler();

[CreateAssetMenu(fileName = "e_", menuName = "Events/Game Event - Basic")]
[InlineEditor(Expanded = false)]
public class GameEvent : ScriptableObject
{
	public event GameEventHandler Handler;

	[Button(ButtonSizes.Medium)]
	public void Invoke()
	{
		Handler?.Invoke();
	}
}