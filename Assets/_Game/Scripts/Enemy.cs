using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 destination;

    public RectTransform arrowPrefab;
    public IState currentState;
    public bool IsDestination => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new MoveState());
        arrowPrefab = Instantiate()
        Indicator.Ins.SetIndicatorParent(this);
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public void RandomMove()
    {
        ChangeAnim(Constants.ANIM_RUN);
        int randomTarget = Random.Range(0, LevelManager.Ins.currentEnemies.Count + 1);
        if (randomTarget == LevelManager.Ins.currentEnemies.Count)
        {
            SetDestination(LevelManager.Ins.player.tf.position);
        }
        else
        {
            SetDestination(LevelManager.Ins.currentEnemies[randomTarget].tf.position);
        }
    }

    public void StopMoving()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        SetDestination(tf.position);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        agent.isStopped = true;
        LevelManager.Ins.currentEnemies.Remove(this);
        Invoke(nameof(OnDespawn), 1f);
        
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
