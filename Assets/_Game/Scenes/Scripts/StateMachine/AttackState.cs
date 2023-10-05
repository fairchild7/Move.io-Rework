using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float attackDuration;

    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        enemy.StopMoving();
        attackDuration = Random.Range(1f, 2f); //check this
        enemy.Attack();
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > attackDuration)
        {
            enemy.ChangeState(new MoveState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
