using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] Character owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_ENEMY))
        {
            Character target = Cache.GetCharacter(other);
            owner.enemyInRange.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_ENEMY))
        {
            Character target = Cache.GetCharacter(other);
            owner.enemyInRange.Remove(target);
        }
    }
}
