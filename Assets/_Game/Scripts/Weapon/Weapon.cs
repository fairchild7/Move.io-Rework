using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType
{
    Ranged = 0,
    Melee = 1
}

public class Weapon: MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] Image image;
    [SerializeField] WeaponType weapType;
    [SerializeField] AttackType attackType;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject prefab;

    public int GetCost() => cost;
    public Image GetImage() => image;
    public WeaponType GetWeaponType() => weapType;
    public AttackType GetAttackType() => attackType;
    public Bullet GetBullet() => bullet;
    public GameObject GetPrefab() => prefab;
}
