using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected int id;

    [SerializeField] WeaponData weaponData;
    [SerializeField] LayerMask characterLayer;
    [SerializeField] Animator animator;
    [SerializeField] Transform throwPoint;
    [SerializeField] protected Transform weaponPos;
    [SerializeField] WeaponType currentWeapon;
    [SerializeField] Collider attackCollider;

    private string currentAnim;

    protected bool isDead = false;
    protected float waitToThrow = 0.4f;
    protected float attackTime = 1f;
    protected int level = 0;
    protected int bulletCount = 1;

    public int GetId() => id;
    public void SetId(int id) { this.id = id; }
    public List<Character> enemyInRange = new List<Character>();

    protected virtual void Update()
    {
        
    }

    public virtual void OnInit()
    {
        level = 0;
        bulletCount = 1;
        isDead = false;
        tf.localScale = new Vector3(1f, 1f, 1f);
        enemyInRange.Clear();
        ChangeAnim(Constants.ANIM_IDLE);

        if (Random.Range(1, 3) == 1)
        {
            EquipWeapon(WeaponType.Knife);
        }
        else
        {
            EquipWeapon(WeaponType.Boomerang);
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null)
            {
                animator.ResetTrigger(currentAnim);
            }
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public void EquipWeapon(WeaponType weapType)
    {
        Weapon weap = weaponData.GetWeapon(weapType);
        Instantiate(weap.GetPrefab(), weaponPos);
        currentWeapon = weapType;
    }

    public virtual void Attack()
    {
        Character target = SetTarget();
        if (target != null)
        {
            ChangeAnim(Constants.ANIM_ATTACK);

            Vector3 dir = target.tf.position - tf.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            tf.rotation = rot;

            bulletCount--;
            StartCoroutine(IEThrow());
        }
    }

    public IEnumerator IEThrow()
    {
        yield return new WaitForSeconds(waitToThrow);

        weaponPos.gameObject.SetActive(false);

        Weapon weap = weaponData.GetWeapon(currentWeapon);
        Bullet bullet = (Bullet)SimplePool.Spawn(weap.GetBullet().poolType, throwPoint.position, Quaternion.LookRotation(transform.forward));
        bullet.owner = this;
        bullet.OnInit();
        bullet.tf.localScale *= (1 + 0.1f * level);

        yield return new WaitForSeconds(attackTime * 0.5f);

        weaponPos.gameObject.SetActive(true);
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public Character SetTarget()
    {
        float minDist = 10f;
        Character target = null;
        for (int i = 0; i < enemyInRange.Count; i++)
        {
            if (enemyInRange[i].isDead)
            {
                continue;
            }
            float dist = Vector3.Distance(enemyInRange[i].tf.position, tf.position);
            if (dist < minDist)
            {
                minDist = dist;
                target = enemyInRange[i];
            }
        }
        return target;
    }

    public virtual void OnDeath()
    {
        StopAllCoroutines();
        isDead = true;
        ChangeAnim(Constants.ANIM_DEAD);
    }

    public void LevelUp()
    {
        level++;
        tf.localScale = new Vector3(1f, 1f, 1f) * (1 + 0.1f * level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BULLET))
        {
            Bullet bullet = Cache.GetBullet(other);
            if (!(bullet.owner == this))
            {
                if (!Physics.GetIgnoreCollision(other, attackCollider))
                {
                    Physics.IgnoreCollision(other, attackCollider);
                    return;
                }
                OnDeath();
                bullet.owner.LevelUp();
                bullet.OnDespawn();
            }
        }
    }
}
