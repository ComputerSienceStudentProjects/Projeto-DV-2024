using System.Collections.Generic;
using UnityEngine;

public class AISystems : MonoBehaviour
{
    private List<GameObject> aiParty;
    private List<GameObject> playerParty;
    public void OnAITurnStart()
    {
        Debug.Log("Event AI TurnStart called");
        aiParty = new List<GameObject>(GameObject.FindGameObjectsWithTag("AI"));
        playerParty = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));


        //each ai enemy finds the closest player to designate as a target; probably change later when abilities work;
        foreach (GameObject obj in aiParty)
        {
            int index = obj.GetComponent<AiControllable>().FindClosestTarget(playerParty);
            if (playerParty.Count > 1)
                playerParty.RemoveAt(index);
        }
    }
}