using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Core;
using AFKHero.Core.Database;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public int weaponId;

    void Start()
    {
        Wearable mockWeapon = ItemDatabaseConnector.Instance.GetWearable(weaponId);
        mockWeapon.Roll();
        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
