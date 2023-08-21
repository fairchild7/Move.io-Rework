using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public int id;

    [SerializeField] float bulletSpeed = 5f;

    private Rigidbody rb;

    public virtual void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character target = other.GetComponent<Character>();
        if (target != null)
        {
            if (target.GetId() != id)
            {
                Debug.Log("hit");
                OnDespawn();
            }
        }   
    }
}
