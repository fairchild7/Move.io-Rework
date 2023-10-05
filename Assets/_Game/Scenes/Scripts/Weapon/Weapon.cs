using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon: MonoBehaviour
{
    public string weaponName;

    [SerializeField] int cost;
    [SerializeField] GameObject image;
    [SerializeField] WeaponType weapType;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject prefab;

    public int GetCost() => cost;
    public GameObject GetImage() => image;
    public WeaponType GetWeaponType() => weapType;
    public Bullet GetBullet() => bullet;
    public GameObject GetPrefab() => prefab;
}
