using System.Collections;
using System.Collections.Generic;
using NativeClasses;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private GameStateService _gameStateService;
    
    private void Awake()
    {
        _gameStateService = GameStateService.GetInstance();
    }

    public void OnAITurnStart()
    {
        Debug.Log("Event AI TurnStart called");
    }

    
}
