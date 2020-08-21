using System;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    //[SerializeField]
    //private PlayerWeapon2 secondaryWeapon;

    private PlayerWeapon currentWeapon;

    private WeaponGraphics currentGraphics;

	void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    //void Update()
    //{
    //    if (Input.GetButton("Fire2"))
    //    {
    //        ToggleWeapons();
    //        Debug.Log("Weapon changed");
    //    }
    //}

    //private void ToggleWeapons()
    //{
    //    GameObject weaponToDelete = GameObject.FindWithTag("Weapon");
    //    Destroy(weaponToDelete);
    //    EquipWeapon(secondaryWeapon);
    //}

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipWeapon (PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        
        GameObject _weaponIns = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null)
            Debug.LogError("No WeaponGraphics component on '" + _weaponIns.name + "'.");

        if (isLocalPlayer)
        {//sets the layers of every child of the weapon object
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }
}
