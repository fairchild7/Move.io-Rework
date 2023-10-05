using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Orange = 3,
    Green = 4,
    Black = 5,
    White = 6,
    Purple = 7,
    Yellow = 8,
}

[CreateAssetMenu(fileName = "Color", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorDatas : ScriptableObject
{
    [SerializeField] ColorData[] colorDatas;

    public ColorData GetColorData(ColorType colorType)
    {
        return colorDatas[(int)colorType];
    }
}

[System.Serializable]
public class ColorData
{
    public int cost;
    public Material material;
    public Color color;
}
