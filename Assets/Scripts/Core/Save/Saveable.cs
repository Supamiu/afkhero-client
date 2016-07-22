namespace AFKHero.Core.Save
{
    public interface Saveable
	{
		SaveData Save(SaveData save);

		void Load(SaveData data);
	}
}
