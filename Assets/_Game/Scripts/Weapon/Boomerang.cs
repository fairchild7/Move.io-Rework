using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    public override void OnInit()
    {
        rb.velocity = tf.forward * bulletSpeed;
        rb.angularVelocity = Vector3.up * 5f;
        StartCoroutine(GoBack());
    }

    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(1f);
        Vector3 dir = Vector3.Normalize(owner.tf.position - tf.position);
        rb.velocity = dir * bulletSpeed;
        yield return new WaitForSeconds(1f);
        OnDespawn();
    }
}
