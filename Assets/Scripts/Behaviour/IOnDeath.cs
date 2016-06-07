using UnityEngine;

namespace AFKHero.Behaviour
{
	/// <summary>
	/// Interface pour se brancher sur la mort du gameobject.
	/// </summary>
	public interface IOnDeath
	{
		void OnDeath();
	}
}
