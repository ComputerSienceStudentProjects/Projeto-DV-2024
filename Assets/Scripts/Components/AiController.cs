using System.Collections;
using System.Collections.Generic;
using NativeClasses;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private GameStateAccessor _gameStateAccessor;
    
    private void Awake()
    {
        _gameStateAccessor = GameStateAccessor.GetInstance();
    }

    public void OnAITurnStart()
    {
        Debug.Log("Event AI TurnStart called");
    }

    
}
