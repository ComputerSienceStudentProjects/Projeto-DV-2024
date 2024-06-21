using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.UIElements.Cursor;

public class PlayerInputSystem : MonoBehaviour
{
    private ISelectable _previousSelectableReference;
    private IControllable _aiTarget;
    private IControllable _previousControllableReference;
    private Camera _mainCamera;


    private bool _isMovementPhase = true;
    private bool _isAttackPhase;

    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private VisualTreeAsset attackButtonComponent;
    private VisualElement _rootElement;

    [SerializeField] private GameEvent startAIEvent;

    private void Update()
    {
        if (_previousControllableReference == null) HideAttacks();
        if (_isMovementPhase) HandleMovementPhaseInput();
        if (_isAttackPhase) HandleAttackPhaseInput();
    }

    private void ShowAttacks()
    {
        VisualElement possibleAttacksContainer = _rootElement.Q<VisualElement>("PossibleAttacks");
        VisualElement attackContainer = _rootElement.Q<VisualElement>("AttackListContainer");
        possibleAttacksContainer.style.display = DisplayStyle.Flex;


        foreach (ScriptableObject attackObject in _previousControllableReference.GetAttacks())
        {
            IAttack attack = (IAttack)attackObject;
            Debug.Log("Attack: " + attack.GetName() + ",Icon: " + attack.GetIcon() + ", color: " + attack.GetColor() + ", Text color: " + attack.GetTextColor());
            TemplateContainer newAttackButton = attackButtonComponent.CloneTree();
            newAttackButton.Q<VisualElement>("AttackButtonBackground").style.backgroundColor = attack.GetColor();
            newAttackButton.Q<Label>("AttackName").style.color = attack.GetTextColor();
            newAttackButton.Q<Label>("AttackName").text = attack.GetName();
            newAttackButton.Q<VisualElement>("AttackIcon").style.backgroundImage = new StyleBackground(attack.GetIcon());
            newAttackButton.Q<VisualElement>().RegisterCallback<ClickEvent>(evt => OnAttackClick(attack));
            attackContainer.Add(newAttackButton.contentContainer);
        }
    }

    private void OnAttackClick(IAttack attack)
    {
        _previousControllableReference.PerformAttack(attack, _aiTarget);
    }

    private void HideAttacks()
    {
        VisualElement possibleAttacksContainer = _rootElement.Q<VisualElement>("PossibleAttacks");
        VisualElement attackContainer = _rootElement.Q<VisualElement>("AttackListContainer");
        possibleAttacksContainer.style.display = DisplayStyle.None;
        attackContainer.Clear();
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.Break();
        }
    }

    private void Start()
    {
        _rootElement = uiDocument.rootVisualElement;
        _rootElement.Q<Button>("FinishPhase").clicked += OnEndPhaseClicked;
    }

    public void StartPlayerTurn()
    {
        _isMovementPhase = true;
        _isAttackPhase = false;
        _rootElement.Q<VisualElement>("Buttons").style.display = DisplayStyle.Flex;
        _rootElement.Q<Label>("Turn").text = "Your Turn";
    }

    private void OnEndPhaseClicked()
    {
        if (_isMovementPhase)
        {
            _isMovementPhase = false;
            _isAttackPhase = true;
            _rootElement.Q<Button>("FinishPhase").text = "Finish Attack Phase";
        }
        else if (_isAttackPhase)
        {
            _isMovementPhase = false;
            _isAttackPhase = false;
            _rootElement.Q<VisualElement>("Buttons").style.display = DisplayStyle.None;
            _rootElement.Q<Label>("Turn").text = "AI Turn";
            startAIEvent.Raise();
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                gameObject.GetComponent<IControllable>()?.ResetAttackFlag();
                gameObject.GetComponent<ISelectable>()?.OnDeselect();
                _previousControllableReference = null;
                _aiTarget = null;
                _previousSelectableReference = null;
            }
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
                    ShowAttacks();
                    return;
                }
                ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null) HandleSelectable(selectable, hit.collider.GetComponent<IControllable>());
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
                if (selectable != null) HandleSelectable(selectable, hit.collider.GetComponent<IControllable>());
                if (selectable == null) HandleMoveInput(hit);
            }
        }
    }

    private void HandleMoveInput(RaycastHit raycastHit)
    {
        _previousControllableReference?.PerformMove(raycastHit.point);
    }

    private void HandleSelectable(ISelectable selectable, IControllable target)
    {
        HideAttacks();
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
