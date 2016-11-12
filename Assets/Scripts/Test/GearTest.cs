using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Core.Database;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public int weaponId;

    private void Start()
    {
        var mockWeapon = ItemDatabaseConnector.Instance.GetWearable(weaponId);
        mockWeapon.Roll();
        mockWeapon.mainStat = 10;
        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
