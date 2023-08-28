using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : GameUnit
{
    public Character owner;

    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] float lifeTime = 2f;

    [SerializeField] protected Rigidbody rb;
    protected Vector3 target;

    protected virtual void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            tf.position = Vector3.MoveTowards(tf.position, target, Time.deltaTime * bulletSpeed);
        }
    }

    public virtual void OnInit()
    {
        target = tf.position + tf.forward * 99f;
        //rb.velocity = tf.forward * bulletSpeed;
        //rb.DOMove(rb.position + tf.forward * bulletSpeed * Time.deltaTime * lifeTime, lifeTime);
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
