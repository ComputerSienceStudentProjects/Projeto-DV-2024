using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private ISelectable _previousSelectableReference;
    private IControllable _previousControllableReference;
    private Camera _mainCamera;
    
    private void Update()
    {
        CheckMouseInputs();
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.Break();
        }
    }

    private void CheckMouseInputs()
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
}
