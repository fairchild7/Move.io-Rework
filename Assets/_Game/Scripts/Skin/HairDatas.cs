using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HairType
{
    Cowboy = 0,
    Crown = 1,
    Hat = 2,
    Horn = 3,
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
    [SerializeField] Image icon;
    [SerializeField] HairType type;
    [SerializeField] GameObject hairPrefab;

    public int GetCost() => cost;
    public Image GetIcon() => icon;
    public HairType GetPantsType() => type;
    public GameObject GetPrefab() => hairPrefab;
}
