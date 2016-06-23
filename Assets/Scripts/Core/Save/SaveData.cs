using UnityEngine;
using System.Collections.Generic;

namespace AFKHero.Core.Save
{
	[System.Serializable]
	public class SaveData
	{
		public Dictionary<string, object[]> data;
	}
}
