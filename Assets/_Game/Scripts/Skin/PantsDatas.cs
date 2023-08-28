using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PantsType
{
    Batman = 0,
    Pokemon = 1,
    USFlag = 2,
    Skull = 3,
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
    [SerializeField] Image icon;
    [SerializeField] PantsType type;
    [SerializeField] Material mat;

    public int GetCost() => cost;
    public Image GetIcon() => icon;
    public PantsType GetPantsType() => type;
    public Material GetMaterial() => mat;
}
