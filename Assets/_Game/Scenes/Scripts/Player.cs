using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Character
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float moveSpeed = 5f;
    
    private void Start()
    {
        playerUI.gameObject.SetActive(false);
        SetName("Fairchild");
        DataManager.Ins.LoadPlayerData();
    }

    public override void OnInit()
    {
        playerUI.gameObject.SetActive(true);
        joystick = UIManager.Ins.GetUI<UIGamePlay>().joystick;
        base.OnInit();
    }

    protected override void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            base.Update();
            if (!isDead)
            {
                rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    StopAllCoroutines();
                    if (rb.velocity != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(rb.velocity);
                    }
                    ChangeAnim(Constants.ANIM_RUN);
                    bulletCount = 1;
                }
                else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
                {
                    if (enemyInRange.Count > 0 && SetTarget() != null)
                    {
                        if (bulletCount > 0)
                        {
                            Attack();
                        }
                    }
                    else
                    {
                        weaponPos.gameObject.SetActive(true);
                        ChangeAnim(Constants.ANIM_IDLE);
                    }
                }
            }
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        if (GameManager.IsState(GameState.GamePlay))
        {
            LevelManager.Ins.OnLose();
        }
    }
    
    public void SetName(string name)
    {
        characterName = name;
    }
}
