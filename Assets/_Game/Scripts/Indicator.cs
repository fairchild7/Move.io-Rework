using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : Singleton<Indicator>
{
    [SerializeField] Canvas indicatorCanvas;

    public void SetIndicatorParent(Enemy enemy)
    {
        enemy.arrowPrefab.SetParent(indicatorCanvas.transform);
    }

    public void CheckNavigation(Enemy enemy)
    {
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(enemy.tf.position);

        if (viewPosition.x >= 0 && viewPosition.x <= 1 && viewPosition.y >= 0 && viewPosition.y <= 1)
        {
            enemy.arrowPrefab.gameObject.SetActive(false);
        }
        else
        {
            enemy.arrowPrefab.gameObject.SetActive(true);
            DrawNavigation(enemy);
        }
    }

    public void DrawNavigation(Enemy enemy)
    {
        Vector3 botPos;
        if (Camera.main.transform.position.z - enemy.tf.position.z > 5f)
        {
            botPos = new Vector3(enemy.tf.position.x, enemy.tf.position.y, Camera.main.transform.position.z - 5f);
            botPos = Camera.main.WorldToScreenPoint(botPos);
        }
        else
        {
            botPos = Camera.main.WorldToScreenPoint(enemy.tf.position);
        }
        Vector3 playerPos = Camera.main.WorldToScreenPoint(LevelManager.Ins.player.tf.position);
        Vector3 dir = botPos - playerPos;

        Vector3 arrowPos = new Vector3(Mathf.Clamp(dir.x, -Screen.width/2 * 0.95f, Screen.width/2 * 0.95f), 
            Mathf.Clamp(dir.y, -Screen.height/2 * 0.95f, Screen.height/2 * 0.95f), dir.z);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Player" + playerPos);
            Debug.Log("Bot" + botPos);
            Debug.Log("Arrow" + arrowPos);
        }
        enemy.arrowPrefab.transform.localPosition = arrowPos;
        enemy.arrowPrefab.eulerAngles = new Vector3(0f, 0f, GetAngle(enemy.tf) + 270f);
    }


    private float GetAngle(Transform target)
    {
        Vector3 dir = target.position - LevelManager.Ins.player.tf.position;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        return angle;
    }
}
