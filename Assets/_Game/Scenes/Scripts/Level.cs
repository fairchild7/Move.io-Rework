using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    [SerializeField] NavMeshData navmeshData;
    [SerializeField] int maxEnemy;
    [SerializeField] int maxCurrentEnemy;

    public int GetMaxEnemy() => maxEnemy;
    public int GetMaxCurrentEnemy() => maxCurrentEnemy;
    public NavMeshData GetNavMeshData() => navmeshData;
}
