using System.Collections;
using System.Collections.Generic;
using UFramework.GameEvents;
using UnityEngine;

public class GameStateInit : MonoBehaviour
{
    [SerializeField] private GameEvent _gameStartEvent;
    
    void Start()
    {
        //_gameStartEvent.Raise();
    }
}
