﻿namespace AFKHero.EventData
{
    public class XPGain
	{
		public double xp;

		public double xpForNextLevel;

		public XPGain(double xp,double forNext){
			this.xp = xp;
            xpForNextLevel = forNext;
		}
	}
}
