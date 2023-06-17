using System.Collections.Generic;
using UnityEditor;

public static class EditorUtilities
{
	#region AssetSearch
	#if UNITY_EDITOR
	public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));

		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

			if (asset != null)
			{
				assets.Add(asset);
			}
		}

		return assets;
	}

	public static List<T> FindAssetsByType<T>(string[] foldersToSearch) where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)), foldersToSearch);

		for (int i = 0; i < guids.Length; i++)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
			T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

			if (asset != null)
			{
				assets.Add(asset);
			}
		}

		return assets;
	}
	#endif
	#endregion
}