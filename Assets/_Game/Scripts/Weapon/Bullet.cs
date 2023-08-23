using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Character owner;

    [SerializeField] protected float bulletSpeed = 5f;
    [SerializeField] float lifeTime = 2f;

    [SerializeField] protected Rigidbody rb;

    public virtual void OnInit()
    {
        rb.velocity = tf.forward * bulletSpeed;
        StartCoroutine(IEDespawnOnLifeTime());
    }

    public virtual void OnDespawn()
    {
        StopAllCoroutines();
        tf.localScale = new Vector3(1f, 1f, 1f);
        SimplePool.Despawn(this);
    }

    public virtual IEnumerator IEDespawnOnLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        SimplePool.Despawn(this);
    }
}
