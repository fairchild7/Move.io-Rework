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
        RefreshNavmesh();
        characterName = DataManager.Ins.nameData[Random.Range(0, 7)];
        ChangeColor(DataManager.Ins.colorData.GetColorData((ColorType)Random.Range(0, 7)));
        base.OnInit();
        ChangeState(new MoveState());
        Indicator.Ins.SetIndicatorParent(this);

        EquipWeapon((WeaponType)Random.Range(0, 4));
        EquipHair((HairType)Random.Range(0, 4));
        EquipPants((PantsType)Random.Range(0, 4));
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
        arrowPrefab.gameObject.SetActive(false);
        base.OnDeath();
        agent.isStopped = true; 
        LevelManager.Ins.currentEnemies.Remove(this);
        Invoke(nameof(OnDespawn), 1f);
        LevelManager.Ins.SpawnEnemy();
        if (LevelManager.Ins.winCondition)
        {
            LevelManager.Ins.OnWin();
        }
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void RefreshNavmesh()
    {
        agent.enabled = false;
        agent.enabled = true;
    }
}
