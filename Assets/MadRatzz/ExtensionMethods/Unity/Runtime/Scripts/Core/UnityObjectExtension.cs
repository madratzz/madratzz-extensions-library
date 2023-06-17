using UnityEngine;

namespace MadRatz.Extensions
{
	public static class UnityObjectExtension
	{
		public static void SafeDestroy(this Object obj)
		{
			#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				Object.DestroyImmediate(obj);
				return;
			}
			#endif
			Object.Destroy(obj);
		}
	}
}