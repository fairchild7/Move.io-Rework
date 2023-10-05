using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PantsType
{
    None = 0,
    Batman = 1,
    Pokemon = 2,
    USFlag = 3,
    Skull = 4,
}

[CreateAssetMenu(fileName = "Pants", menuName = "ScriptableObjects/PantsDatas", order = 1)]
public class PantsDatas : ScriptableObject
{
    [SerializeField] PantsData[] pantDatas;

    public PantsData GetPantsData(PantsType pantsType)
    {
        return pantDatas[(int)pantsType];
    }
}

[System.Serializable]
public class PantsData
{
    [SerializeField] int cost;
    [SerializeField] Sprite icon;
    [SerializeField] PantsType type;
    [SerializeField] Material mat;

    public int GetCost() => cost;
    public Sprite GetIcon() => icon;
    public PantsType GetPantsType() => type;
    public Material GetMaterial() => mat;
}
