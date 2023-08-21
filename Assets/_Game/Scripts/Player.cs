using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] float moveSpeed = 5f;

    public override void OnInit()
    {
        base.OnInit();
        id = 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
            ChangeAnim(Constants.ANIM_RUN);
            //canAttack = false;
            //numBullet = 1;
        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
            //canAttack = true;
        }
    }
}
