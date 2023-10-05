using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform weaponPos;
    [SerializeField] protected Transform hairPos;
    [SerializeField] protected SkinnedMeshRenderer pantsMesh;
    [SerializeField] protected Transform vfxPos;
    [SerializeField] Animator animator;
    [SerializeField] Transform throwPoint;
    [SerializeField] Collider attackCollider;
    [SerializeField] protected NameText playerUI;
    [SerializeField] SkinnedMeshRenderer skinRenderer;

    protected string characterName;
    protected ColorData color;
    private string currentAnim;

    protected Weapon currentWeapData;
    protected GameObject currentWeapon;
    protected GameObject currentHair;
    protected bool isDead = false;
    protected float waitToThrow = 0.3f; //or 0.4?
    protected float attackTime = 1f;
    protected int level = 0;
    protected int bulletCount = 1;

    public bool IsDead() => isDead;
    public int GetLevel() => level;
    public WeaponType GetWeaponType() => currentWeapData.GetWeaponType();
    public float levelScale => (1 + 0.05f * level);
    public List<Character> enemyInRange = new List<Character>();
    public string killedBy;
    public NameText PlayerUI => playerUI;
    public Transform WeaponPos => weaponPos;

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

        InitPlayerUI();
        ChangeAnim(Constants.ANIM_IDLE);
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
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        Weapon weap = DataManager.Ins.weaponData.GetWeapon(weapType);
        currentWeapon = Instantiate(weap.GetPrefab(), weaponPos);
        currentWeapData = weap;
    }

    public void EquipHair(HairType hairType)
    {
        if (currentHair != null)
        {
            Destroy(currentHair.gameObject);
        }
        HairData hair = DataManager.Ins.hairData.GetHairData(hairType);
        currentHair = Instantiate(hair.GetPrefab(), hairPos);
    }

    public void EquipPants(PantsType pantType)
    {
        PantsData pants = DataManager.Ins.pantsData.GetPantsData(pantType);
        pantsMesh.material = pants.GetMaterial();
    }

    public void ChangeColor(ColorData colorData)
    {
        skinRenderer.material = colorData.material;
        playerUI.UpdateColor(colorData.color);
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

        Bullet bullet = (Bullet)SimplePool.Spawn(currentWeapData.GetBullet().poolType, throwPoint.position, Quaternion.LookRotation(transform.forward));
        bullet.owner = this;
        bullet.OnInit();
        bullet.tf.localScale *= levelScale;

        yield return new WaitForSeconds(attackTime * 0.5f);

        weaponPos.gameObject.SetActive(true);
        ChangeAnim(Constants.ANIM_IDLE);
    }

    public Character SetTarget()
    {
        float minDist = 10f * levelScale;
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
        LevelManager.Ins.currentAlive--;
        LevelManager.Ins.UpdateAliveText();

        StopAllCoroutines();
        isDead = true;
        ChangeAnim(Constants.ANIM_DEAD);
    }

    public void LevelUp()
    {
        level++;
        tf.localScale = new Vector3(1f, 1f, 1f) * levelScale;
        playerUI.UpdateLevelText(level);
    }

    private void InitPlayerUI()
    {
        playerUI.UpdateNameText(characterName);
        playerUI.UpdateLevelText(level);
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
                ParticlePool.Play(ParticleType.Hit, vfxPos.position, Quaternion.identity);
                killedBy = bullet.owner.characterName;
                bullet.owner.LevelUp();
                OnDeath();
                bullet.OnDespawn();
            }
        }
    }
}
