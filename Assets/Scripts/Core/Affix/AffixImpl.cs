using AFKHero.Model.Affix;
using UnityEngine;

namespace AFKHero.Core.Affix
{
    public abstract class AffixImpl
    {
        public AffixModel model;

        public void Attach(GameObject go, AffixModel affix)
        {
            if (affix.type != GetAffixType())
            {
                throw new System.Exception("Trying to attach affix with wrong impl !");
            }
            else
            {
                model = affix;
                DoAttach(go);
            }
        }

        public abstract void DoAttach(GameObject go);

        public abstract void Detach();

        public abstract AffixType GetAffixType();

        
    }
}
