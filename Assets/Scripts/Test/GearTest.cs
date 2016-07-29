using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Core.Database;
using AFKHero.Tools;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public int weaponId;

    void Start()
    {
        Wearable mockWeapon = ItemDatabaseConnector.Instance.GetWearable(weaponId);
        mockWeapon.Roll();
        mockWeapon.mainStat = 10;
        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
