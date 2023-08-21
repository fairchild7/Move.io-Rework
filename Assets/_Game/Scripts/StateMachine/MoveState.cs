using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    float timer;
    float moveDuration;

    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        //enemy. ~ random move
        moveDuration = Random.Range(2f, 5f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > moveDuration)
        {
            enemy.ChangeState(new IdleState());
        }
        if (enemy.enemyInRange.Count > 0 && timer > 1f)
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
