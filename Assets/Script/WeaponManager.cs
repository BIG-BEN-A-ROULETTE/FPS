using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeapon primaryWeapon;


    private PlayerWeapon currentWeapon;

    [SerializeField]
    private string weaponLayerName = "Weapon";


    void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    void EquipWeapon(PlayerWeapon _weapon)  //rend dynamique la variable current weapon.
    {
        currentWeapon = _weapon;
    }


}
