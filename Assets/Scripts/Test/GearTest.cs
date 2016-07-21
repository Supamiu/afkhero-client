using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Model.Affix;
using System;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public Sprite weaponSprite;

    void Start()
    {
        ItemAffix<DamageBonus> damageAffix = new ItemAffix<DamageBonus>(50, 100);
        damageAffix.Roll();
        ItemAffix<CritChancesBonus> critAffix = new ItemAffix<CritChancesBonus>(90, 100);
        critAffix.Roll();
        Wearable mockWeapon = new Wearable();
        mockWeapon.itemName = "Epée de test";
        mockWeapon.rarity = Rarity.COMMON;
        mockWeapon.sprite = weaponSprite;
        mockWeapon.type = GearType.WEAPON;
        mockWeapon.affixes.Add(damageAffix);
        mockWeapon.affixes.Add(critAffix);

        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
