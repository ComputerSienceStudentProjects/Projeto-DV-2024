using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    private ISelectable _previousSelectableReference;
    private IControllable _aiTarget;
    private IControllable _previousControllableReference;
    private Camera _mainCamera;

    private bool ShouldShowAttackList;
    public bool testMovementPhase;
    public bool testAttackPhase;
    
    
    private void Update()
    {
        if (testMovementPhase) HandleMovementPhaseInput();
        if (testAttackPhase) HandleAttackPhaseInput();
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.Break();
        }
    }

    private void HandleAttackPhaseInput()
    {
        /* How this is gonna work:
         * Everytime the player clicks on their characters, we store that, highlight the cvharacter
         * then we wait either untill the player changed their mind, or selects a target,
         * and if bnoth target, and offender r set, we can perform an attack
         */
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(mouseScreenPosition), out var hit))
            {
                
                IAIControllable aiTarget = hit.collider.GetComponent<IAIControllable>();
                if (aiTarget != null)
                {
                    if (_previousControllableReference == null)
                    {
                        Debug.Log("Clicked on AI TARGET Before selecting a prty chracter");
                        return;
                    }
                    _aiTarget = hit.collider.GetComponent<IControllable>();
                        Debug.Log("both offender and target set");
                        ShouldShowAttackList = true;
                    return;
                }
                ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null) HandleSelectable(selectable,hit.collider.GetComponent<IControllable>());
            }
        }
    }

    private void HandleMovementPhaseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(mouseScreenPosition), out var hit))
            {
               ISelectable selectable = hit.collider.GetComponent<ISelectable>();
               if (selectable != null) HandleSelectable(selectable,hit.collider.GetComponent<IControllable>());
               if (selectable == null) HandleMoveInput(hit);
            }
        }
    }

    private void HandleMoveInput(RaycastHit raycastHit)
    {
        _previousControllableReference?.PerformMove(raycastHit.point);
    }

    private void HandleSelectable(ISelectable selectable,IControllable target)
    {
        if (_previousSelectableReference == selectable)
        {
            selectable.OnDeselect();
            _previousSelectableReference = null;
            return;
        }
        selectable.OnSelect();
        _previousSelectableReference?.OnDeselect();
        _previousSelectableReference = selectable;
        _previousControllableReference = target;
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 250, 25), "Finish Movement Phase"))
        {
            testMovementPhase = false;
            testAttackPhase = true;
            _previousSelectableReference?.OnDeselect();
            _previousControllableReference = null;
            _previousSelectableReference = null;
        }
        
        if (GUI.Button(new Rect(10, 50, 250, 25), "Finish Attack Phase"))
        {
            testMovementPhase = true;
            testAttackPhase = false;
        }

        if (!ShouldShowAttackList) return;
        GUI.Box(new Rect(10, 90, 300, 300),
            "Possible Attacks for character: " + _previousControllableReference.GetName());
        int baseY = 90 + 40;
        foreach(var attackScriptable in _previousControllableReference?.GetAttacks())
        {
            if (GUI.Button(new Rect(20, baseY, 280, 25), attackScriptable.name))
            {
                _previousControllableReference?.PerformAttack((IAttack)attackScriptable,_aiTarget);
            }
            baseY += 40;
        }

        if (GUI.Button(new Rect(20, 390 - 40, 280, 25), "Cancel Attack"))
        {
            _previousSelectableReference?.OnDeselect();
            _previousControllableReference = null;
            _previousSelectableReference = null;
            _aiTarget = null;
            ShouldShowAttackList = false;
        }
    }
}
