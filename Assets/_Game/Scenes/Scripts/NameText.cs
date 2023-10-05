using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;
    [SerializeField] Transform tf;
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        tf.position = screenPos + offset;
    }

    public void UpdateNameText(string name)
    {
        nameText.text = name;
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = level.ToString();
    }

    public void UpdateColor(Color color)
    {
        nameText.color = color;
        levelText.color = color;
    }
}
