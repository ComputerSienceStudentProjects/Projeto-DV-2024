using System;
using System.Collections;
using System.Collections.Generic;
using Model.OpenWorld;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldInputSystem : MonoBehaviour
{
    private ISelectable _previousSelectableReference;
    private IControllable _previousControllableReference;
    private Camera _mainCamera;

    private void Update()
    {
        HandleMovementPhaseInput();
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.Break();
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
               CombatIteractable combat = hit.collider.GetComponent<CombatIteractable>();
               if (combat != null) HandleCombatStart(combat);
               if (selectable == null) HandleMoveInput(hit);
            }
        }
    }

    private void HandleCombatStart(CombatIteractable combat)
    {

        if (Physics.CheckSphere(combat.GetPosition(), 2.0f, LayerMask.GetMask("Player"),
                QueryTriggerInteraction.Collide))
        {
            SceneManager.LoadScene("BattleScene");
        }
        else
        {
            Debug.Log("Player is too far away from combat character, not initializing combat");
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
            _previousControllableReference = null;  
            return;
        }
        selectable.OnSelect();
        _previousSelectableReference?.OnDeselect();
        _previousSelectableReference = selectable;
        _previousControllableReference = target;
    }
}

