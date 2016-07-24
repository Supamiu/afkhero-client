
namespace AFKHero.Model
{
    [System.Serializable]
    public class WearableDrop : Drop<Wearable>
    {
        public WearableDrop(Wearable item) : base(item)
        {
        }
    }
}
