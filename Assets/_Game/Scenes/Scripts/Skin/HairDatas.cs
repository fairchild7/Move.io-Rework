using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HairType
{
    None = 0,
    Cowboy = 1,
    Crown = 2,
    Hat = 3,
    Horn = 4,
}

[CreateAssetMenu(fileName = "Hairs", menuName = "ScriptableObjects/HairDatas", order = 1)]
public class HairDatas : ScriptableObject
{
    [SerializeField] HairData[] hairDatas;

    public HairData GetHairData(HairType hairType)
    {
        return hairDatas[(int)hairType];
    }
}

[System.Serializable]
public class HairData
{
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] HairType type;
    [SerializeField] GameObject hairPrefab;

    public int GetCost() => cost;
    public Sprite GetIcon() => icon;
    public HairType GetPantsType() => type;
    public GameObject GetPrefab() => hairPrefab;
}
