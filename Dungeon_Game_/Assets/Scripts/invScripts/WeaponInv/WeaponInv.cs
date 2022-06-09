using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInv : MonoBehaviour
{
    public PlayerWeaponController playerWeaponController;
    public Item sword;

    void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        List<BaseValueStat> swordStats = new List<BaseValueStat>();
        swordStats.Add(new BaseValueStat(6, "Power", "Your power Level"));
        sword = new Item(swordStats, "sword");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerWeaponController.EquipWeapon(sword);
        }
    }

}
