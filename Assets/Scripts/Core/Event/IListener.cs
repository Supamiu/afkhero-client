using System;

namespace AFKHero.Core.Event
{
    /// <summary>
    /// Interface listener.
    /// </summary>
    public interface IListener {

        string GetId();

		Type getType ();

		void Call (ref object e);

		int getPriority();
	}
}
