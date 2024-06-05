using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IControllable, ISelectable
{
    private readonly string _guid = Guid.NewGuid().ToString();
    
    [Header("Character Information")] 
    [SerializeField] private CharacterData characterData;
    [SerializeField] private ScriptableObject[] attacks;

    private NavMeshAgent _agent;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material walkingMaterial;
    private bool _shouldCheckIfReaced = false;
    private bool _isSelected;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        selectedMaterial = meshRenderer.materials[1];
        walkingMaterial = meshRenderer.materials[2];
        selectedMaterial.SetFloat("_OutlineSize",0f);
    }

    public void PerformAttack(IAttack attack, IControllable target)
    {
        if (target == null) return;
        attack?.Execute(target);
    }

    private bool ReachedDestinationOrGaveUp()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    
    private void Update()
    {
        if (!_shouldCheckIfReaced) return;
        if (!ReachedDestinationOrGaveUp()) return;
        walkingMaterial.SetFloat("_OutlineSize",0f);
        if (_isSelected) selectedMaterial.SetFloat("_OutlineSize",1.1f);
        _shouldCheckIfReaced = false;
    }

    public void PerformMove(Vector3 destination)
    {
        _agent.SetDestination(destination);
        _shouldCheckIfReaced = true;
        selectedMaterial.SetFloat("_OutlineSize",0f);
        walkingMaterial.SetFloat("_OutlineSize",1.1f);
    }
    
    public void ApplyAttackResults(AttackResult attackResult)
    {
        characterData.SubtractHp(attackResult.ResultingDamage);
    }

    public void OnSelect()
    {
        Debug.Log("Here!");
        _isSelected = true;
        selectedMaterial.SetFloat("_OutlineSize",1.1f);
    }

    public void OnDeselect()
    {
        _isSelected = false;
        selectedMaterial.SetFloat("_OutlineSize",0f);
    }
}
