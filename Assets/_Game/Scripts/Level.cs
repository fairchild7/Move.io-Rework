using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int maxEnemy;
    [SerializeField] int maxCurrentEnemy;

    public int GetMaxEnemy() => maxEnemy;
    public int GetMaxCurrentEnemy() => maxCurrentEnemy;
}
