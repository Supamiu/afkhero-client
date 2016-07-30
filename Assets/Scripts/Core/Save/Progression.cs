using AFKHero.Core.Tools;
using AFKHero.Model;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace AFKHero.Core.Save
{
    [Serializable]
    public class Progression : Singleton<Progression>, Saveable
    {
        public List<StageProgression> stageProgression;

        protected Progression() { }

        void Awake()
        {
            //Lazy loading acc�l�r�.
            Instance.IsStageDone(new Stage(), 0);
        }

        public bool IsStageDone(Stage stage, float distance)
        {
            if(stageProgression == null)
            {
                return false;
            }
            foreach(StageProgression s in stageProgression)
            {
                if (Mathf.RoundToInt(distance) / 100 == Mathf.RoundToInt(s.distance) / 100 && 
                    s.stage.boss.name == s.stage.boss.name)
                {
                    return true;
                }
            }
            return false;            
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
