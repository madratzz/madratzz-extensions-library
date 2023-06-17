using UnityEngine.UI;

namespace MadRatz.Extensions
{
	public static class InputFieldExtension
	{
		public static void Focus(this InputField inputField)
		{
			inputField.Select();
			inputField.ActivateInputField();
		}
	}
}