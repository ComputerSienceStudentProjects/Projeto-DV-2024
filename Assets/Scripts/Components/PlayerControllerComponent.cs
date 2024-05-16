using NativeClasses;
using UnityEngine;

[AddComponentMenu(menuName:"Game Components/Player Controller")]
public class PlayerControllerComponent : MonoBehaviour
{
    private GameStateAccessor _gameStateAccessor;
    private void Awake()
    {
        _gameStateAccessor = GameStateAccessor.GetInstance();
    }

    public void OnPlayerTurnStart()
    {
        Debug.Log("Event Player TurnStart called");
    }
}
