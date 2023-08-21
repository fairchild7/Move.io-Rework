using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Boomerang = 0,
    Blade = 1,
    Knife = 2,
    Hammer = 3,
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] Weapon[] weapons;

    public Weapon GetWeapon(WeaponType weapType)
    {
        return weapons[(int)weapType];
    }
}