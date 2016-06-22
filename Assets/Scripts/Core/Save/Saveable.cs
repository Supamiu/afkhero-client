using UnityEngine;
using System.Collections;

namespace AFKHero.Core.Save
{
	public interface Saveable
	{
		string GetIdentifier();

		object[] Save();

		void Load(object[] data);
	}
}
