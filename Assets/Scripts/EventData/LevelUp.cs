namespace AFKHero.EventData
{
    public class LevelUp
	{
		public double level;

		public double xpForNextLevel;

		public double xpRemaining;

		public LevelUp(double level, double xpForNext, double xp){
			this.level = level;
            xpForNextLevel = xpForNext;
            xpRemaining = xp;
		}
	}
}
