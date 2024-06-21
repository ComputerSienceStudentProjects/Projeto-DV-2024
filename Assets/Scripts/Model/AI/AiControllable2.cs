using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiControllable2 : MonoBehaviour, IAIControllable, IControllable
{

    private GameObject currentTarget;
    private float leastDistance = 100f;

    private NavMeshAgent agent;
    private Animator _animator;

    private bool canAttack = false;

    [SerializeField] private float attackRange = 10f;

    [SerializeField] private float movementRange = 15f;

    [SerializeField] private float initialHealth = 100.0f;
    [SerializeField] private float runtimeHealth;

    private string IsMoving = "is_moving";
    private string attack = "attack";
    private string IsAttacking = "is_threat";
    private string death = "death";

    [SerializeField] string attackAnimationName;
    [SerializeField] string storeWeaponAnimationName;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        runtimeHealth = initialHealth;
    }

    private void Threat()
    {
        _animator.SetBool(IsAttacking, true);
        canAttack = true;
    }

    public void ApplyAttackResults(AttackResult attackResult)
    {
        runtimeHealth -= attackResult.ResultingDamage;
        if (runtimeHealth <= 0) _animator.SetTrigger(death);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Attack()
    {
        _animator.SetBool(IsAttacking, false);
        _animator.SetTrigger(attack);
        canAttack = false;

        // Wait until the Animator is in the "attack" state
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }

        // Wait until the "slash" animation is done
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            yield return null;
        }

        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Store"))
        {
            yield return null;
        }

        // Wait until the "slash" animation is done
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Store"))
        {
            yield return null;
        }
    }

    private IEnumerator ThreatTarget()
    {
        int layermask = 1 << 8;

        layermask = ~layermask;
        Vector3 destination;
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

        RaycastHit hit;
        //check if we can hit player before moving
        if (Physics.Raycast(transform.position, direction, out hit, attackRange, layermask))
        {
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow, Mathf.Infinity);
            Debug.Log("Did Hit: " + hit.collider.name);
            Threat();
        }
        else
        //if we cant hit the player then we move in the direction of the player character, very linear needs balancing later
        {
            Debug.DrawRay(transform.position, direction * 10, Color.white, Mathf.Infinity);
            Debug.Log("Did not Hit");

            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            Debug.Log(distanceToTarget);

            if (distanceToTarget < movementRange)
            {
                destination = transform.position + direction * (distanceToTarget - attackRange);
            }
            else
                destination = transform.position + direction * movementRange;

            yield return StartCoroutine(WaitUntilReachedTarget(destination, direction));
        }
    }


    public IEnumerator PerformTurn()
    {
        if (canAttack) yield return StartCoroutine(Attack());
        Debug.Log("Finished Attack");
        yield return StartCoroutine(ThreatTarget());
    }

    IEnumerator WaitUntilReachedTarget(Vector3 destination, Vector3 direction)
    {
        agent.destination = destination;
        // Wait until the agent reaches its destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            _animator.SetBool(IsMoving, true);
            yield return null;
        }

        int layermask = 1 << 8;
        layermask = ~layermask;

        _animator.SetBool(IsMoving, false);

        RaycastHit hit;
        // Code to execute after the agent has finished moving
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, Mathf.Infinity);
        if (Physics.Raycast(transform.position, direction, out hit, attackRange, layermask))
            Threat();
        else
            Debug.Log("not close to attack heal or something");
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

    public void PerformAttack(IAttack attack, IControllable target)
    {

    }

    public void PerformMove(Vector3 destination)
    {

    }

    public ScriptableObject[] GetAttacks()
    {
        return null;
    }

    public string GetName()
    {
        return null;
    }

    public void ResetAttackFlag()
    {

    }
}
