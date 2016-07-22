using AFKHero.Core.Gear;
using AFKHero.Model;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

    public Image icon;

    public Text weaponName;

    public Text weaponDescription;

    public GearSystem gear;

    private Wearable weapon;

    void Awake()
    {
        gear.GearChangeEvent += () =>
        {
            weapon = gear.GetWearableAtSlot(GearSlot.WEAPON);
            Debug.Log(weapon.icon.name);
            icon.sprite = weapon.icon;
            weaponName.text = weapon.itemName;
        };
    }
}
