using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Bullet
{
    private bool isReturn = false;

    protected override void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (!isReturn)
            {
                tf.position = Vector3.MoveTowards(tf.position, target, Time.deltaTime * bulletSpeed);
            }
            else
            {
                if (Vector3.Distance(tf.position, owner.tf.position) > 0.1f)
                {
                    tf.position = Vector3.MoveTowards(tf.position, owner.tf.position, Time.deltaTime * bulletSpeed);
                }
                else
                {
                    OnDespawn();
                }
            }
        }
    }

    public override void OnInit()
    {
        isReturn = false;
        target = tf.position + tf.forward * 99f;
        rb.angularVelocity = Vector3.up * 5f;
        StartCoroutine(GoBack());
    }

    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(1f);
        isReturn = true;
        yield return new WaitForSeconds(1f);
        OnDespawn();
    }
}
