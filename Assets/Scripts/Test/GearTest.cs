using UnityEngine;
using AFKHero.Core.Gear;
using AFKHero.Model;
using AFKHero.Model.Affix;

public class GearTest : MonoBehaviour
{
    public GearSystem gear;

    public Sprite weaponSprite;

    void Start()
    {
        ItemAffix<DamageBonus> affix = new ItemAffix<DamageBonus>(50, 100);
        affix.Roll();
        Wearable mockWeapon = new Wearable();
        mockWeapon.itemName = "Epée de test";
        mockWeapon.rarity = Rarity.COMMON;
        mockWeapon.sprite = weaponSprite;
        mockWeapon.type = GearType.WEAPON;
        mockWeapon.affixes.Add(affix);

        gear.Equip(mockWeapon);
    }
}
