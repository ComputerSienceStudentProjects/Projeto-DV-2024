using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystems : MonoBehaviour
{
    private List<GameObject> aiParty;
    private List<GameObject> playerParty;

    [SerializeField] GameEvent EndAITurnEvent;
    [SerializeField] GameEvent PlayerTurnStart;
    public void OnAITurnStart()
    {
        Debug.Log("Event AI TurnStart called");
        aiParty = new List<GameObject>(GameObject.FindGameObjectsWithTag("AI"));
        playerParty = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));


        //each ai enemy finds the closest player to designate as a target; probably change later when abilities work;
        foreach (GameObject obj in aiParty)
        {
            int index = obj.GetComponent<AiControllable2>().FindClosestTarget(playerParty);
            if (playerParty.Count > 1)
                playerParty.RemoveAt(index);
        }

        StartCoroutine(HandleAITurns());
    }

    IEnumerator HandleAITurns()
    {
        List<Coroutine> aiTurnCoroutines = new List<Coroutine>();

        // Start all AI turns and add the coroutines to the list.
        foreach (GameObject obj in aiParty)
        {
            aiTurnCoroutines.Add(StartCoroutine(WaitToFinishTurn(obj)));
        }

        // Wait until all AI turn coroutines have completed.
        foreach (Coroutine aiTurn in aiTurnCoroutines)
        {
            yield return aiTurn;
        }

        // Raise the end turn events.
        EndAITurnEvent.Raise();
        PlayerTurnStart.Raise();
    }

    IEnumerator WaitToFinishTurn(GameObject ai)
    {
        yield return ai.GetComponent<AiControllable2>().PerformTurn();
    }
}