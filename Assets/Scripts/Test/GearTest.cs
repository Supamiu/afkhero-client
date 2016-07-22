using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Model.Affix;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public Sprite weaponSprite;

    public Sprite weaponIcon;

    void Start()
    {
        ItemAffix<DamageBonus> damageAffix = new ItemAffix<DamageBonus>("Damage", 50, 100);
        damageAffix.Roll();
        ItemAffix<CritChancesBonus> critChancesAffix = new ItemAffix<CritChancesBonus>("Crit chances", 90, 100);
        critChancesAffix.Roll();
        ItemAffix<CritDamageBonus> critDamageAffix = new ItemAffix<CritDamageBonus>("Crit damage", 150, 200);
        critDamageAffix.Roll();
        ItemAffix<HPBonus> hpAffix = new ItemAffix<HPBonus>("HP Bonus", 50, 100);
        hpAffix.Roll();
        Wearable mockWeapon = new Wearable();
        mockWeapon.itemName = "Epée de test";
        mockWeapon.description = "L'épée pour tester ta résistance à ma main dans ta gueule";
        mockWeapon.rarity = Rarity.COMMON;
        mockWeapon.sprite = weaponSprite;
        mockWeapon.type = GearType.WEAPON;
        mockWeapon.affixes.Add(damageAffix);
        mockWeapon.affixes.Add(critChancesAffix);
        mockWeapon.affixes.Add(critDamageAffix);
        mockWeapon.affixes.Add(hpAffix);

        mockWeapon.icon = weaponIcon;

        if (gear.IsSlotFree(GearSlot.WEAPON))
        {
            gear.Equip(mockWeapon);
        }
    }
}
