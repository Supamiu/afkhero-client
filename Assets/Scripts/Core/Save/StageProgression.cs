using AFKHero.Model;

namespace AFKHero.Core.Save
{
    [System.Serializable]
    public class StageProgression
    {
        public Stage stage;

        public float distance;

        public StageProgression(Stage stage, float distance)
        {
            this.stage = stage;
            this.distance = distance;
        }
    }
}
