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
    [SerializeField] Transform weaponPos;
    [SerializeField] WeaponType currentWeapon;

    private string currentAnim;
    private bool canAttack = true;

    public int GetId() => id;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    public virtual void OnInit()
    {
        canAttack = true;
        EquipWeapon(WeaponType.Knife);
        currentWeapon = WeaponType.Knife;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public void EquipWeapon(WeaponType weapType)
    {
        Weapon weap = weaponData.GetWeapon(weapType);
        Instantiate(weap.GetPrefab(), weaponPos);
    }

    public void Attack()
    {
        Character target = GetEnemy();
        if (target != null)
        {
            canAttack = false;
            ChangeAnim(Constants.ANIM_ATTACK);

            Vector3 dir = target.tf.position - tf.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            tf.rotation = rot;

            weaponPos.gameObject.SetActive(false);

            Weapon weap = weaponData.GetWeapon(currentWeapon);
            Bullet bullet = (Bullet)SimplePool.Spawn(weap.GetBullet().poolType, throwPoint.position, Quaternion.LookRotation(transform.forward));
            bullet.id = id;
            bullet.OnInit();

            Invoke(nameof(DeactiveAttack), 0.5f);
        }
    }

    public void DeactiveAttack()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        weaponPos.gameObject.SetActive(true);
    }

    public Character GetEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(tf.position, 5f, characterLayer);
        if (enemies.GetLength(0) > 1)
        {
            return enemies[1].gameObject.GetComponent<Character>();
        }
        return null;
    }

    public virtual void Death()
    {
        ChangeAnim(Constants.ANIM_DEAD);
    }
}
