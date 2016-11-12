using AFKHero.Core.Tools;
using AFKHero.Model;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace AFKHero.Core.Save
{
    [Serializable]
    public class Progression : Singleton<Progression>, Saveable
    {
        public List<StageProgression> stageProgression;

        protected Progression() { }

        private void Awake()
        {
            //Lazy loading accéléré.
            Instance.IsStageDone(new Stage(), 0);
        }

        public bool IsStageDone(Stage stage, float distance)
        {
            return stageProgression != null && stageProgression.Any(s => Mathf.RoundToInt(distance) / 100 == Mathf.RoundToInt(s.distance) / 100 && stage.boss.name == s.stage.boss.name);
        }
        
        public void StageDone(Stage stage, float distance)
        {
            if(stageProgression == null)
            {
                stageProgression = new List<StageProgression>();
            }
            stageProgression.Add(new StageProgression(stage, distance));
        }

        public void Load(SaveData data)
        {
            stageProgression = data.progressionStages;
        }

        public SaveData Save(SaveData save)
        {
            save.progressionStages = stageProgression;
            return save;
        }
    }
}
