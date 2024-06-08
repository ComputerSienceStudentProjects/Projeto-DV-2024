using System;
using System.Collections.Generic;
using UnityEngine;

public class AiControllable : MonoBehaviour, IControllable, IAIControllable
{

    private GameObject currentTarget;
    private float leastDistance;

    public void PerformAttack(IAttack attack, IControllable target)
    {
        throw new System.NotImplementedException();
    }

    public void PerformMove(Vector3 destination)
    {
        throw new System.NotImplementedException();
    }

    public void ApplyAttackResults(AttackResult attackResult)
    {
        throw new NotImplementedException("Someone called Apply attack results, not yet implemented");
    }

    public ScriptableObject[] GetAttacks()
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        throw new NotImplementedException();
    }

    public void FindLocation()
    {
        throw new System.NotImplementedException();
    }

    public int FindClosestTarget(List<GameObject> playerParty)
    {
        int closestIndex = 0;
        for (int i = 0; i < playerParty.Count; i++)
        {
            float distance = Vector3.Distance(gameObject.transform.position, playerParty[i].transform.position);
            if (distance < leastDistance) { leastDistance = distance; currentTarget = playerParty[i]; closestIndex = i; }
        }

        return closestIndex;
    }
}
