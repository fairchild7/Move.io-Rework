using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float idleDuration;

    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        idleDuration = Random.Range(0.5f, 2f);
        enemy.StopMoving();
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > idleDuration)
        {
            enemy.ChangeState(new MoveState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
