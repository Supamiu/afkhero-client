using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Core;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public Sprite weaponSprite;

    public Sprite weaponIcon;

    void Start()
    {
        Wearable mockWeapon = ResourceLoader.LoadWearableDatabase().GetItem(1804102268);

        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
