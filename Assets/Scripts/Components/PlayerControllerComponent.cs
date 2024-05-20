using NativeClasses;
using UnityEngine;

[AddComponentMenu(menuName:"Game Components/Player Controller")]
public class PlayerControllerComponent : MonoBehaviour
{
    private GameStateService _gameStateService;
    private void Awake()
    {
        _gameStateService = GameStateService.GetInstance();
    }

    public void OnPlayerTurnStart()
    {
        Debug.Log("Event Player TurnStart called");
    }
}
