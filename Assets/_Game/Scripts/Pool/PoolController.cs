using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] PoolAmount[] prePools;

    void Start()
    {
        for (int i = 0; i < prePools.Length; i++)
        {
            SimplePool.Preload(prePools[i].gameUnit, prePools[i].parent, prePools[i].amount);
        }  
    }
}

[System.Serializable]
public class PoolAmount
{
    public GameUnit gameUnit;
    public Transform parent;
    public int amount;
}
