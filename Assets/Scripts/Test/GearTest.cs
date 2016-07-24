using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Core;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public uint weaponId;

    void Start()
    {
        Wearable mockWeapon = ResourceLoader.LoadWearableDatabase().GetItem(weaponId);

        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
