using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Transform tf;

    public Transform target;
    public Vector3 playOffset;
    public Transform menuTf;
    public Transform shopTf;
    public float speed = 20;

    void Start()
    {
        target = player.tf;
    }

    void FixedUpdate()
    {
        if (GameManager.IsState(GameState.GamePlay) || GameManager.IsState(GameState.Setting) || GameManager.IsState(GameState.Revive))
        {
            tf.rotation = Quaternion.Euler(50, 0, 0);
            Vector3 currentOffset = playOffset * (1 + 0.05f * player.GetLevel());
            tf.position = Vector3.Lerp(transform.position, target.position + currentOffset, Time.deltaTime * speed);
        }
        else if (GameManager.IsState(GameState.Shop))
        {
            tf.rotation = shopTf.rotation;
            tf.position = shopTf.position;
        }
        else
        {
            tf.rotation = menuTf.rotation;
            tf.position = menuTf.position;
        }
    }
}
