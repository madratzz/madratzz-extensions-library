using System.Collections.Generic;

namespace MadRatz.Extensions
{
	public static class HashSetExtension
	{
		public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
		{
			foreach (var item in range)
			{
				hashSet.Add(item);
			}
		}
	}
}