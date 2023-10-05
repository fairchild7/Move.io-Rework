using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Bullet
{
    public override void OnInit()
    {
        target = tf.position + tf.forward * 99f;
        rb.angularVelocity = Vector3.up * 5f;
        StartCoroutine(IEDespawnOnLifeTime());
    }
}
