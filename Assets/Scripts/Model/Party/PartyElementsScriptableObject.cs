using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Party Scriptable Object")]
public class PartyElementsScriptableObject : ScriptableObject
{
    [Header("Party parameters")] 
    [SerializeField] private List<PartyElement> partyElements;
    [SerializeField] private int selectedPartyMember = 0;

    public void AddCharacterToParty(GameObject character,int position = -1)
    {
        if (FindPartyMember(character) != null) return;
        int nextFreePosition = FindNextOpenPartySlot();
        PartyElement newPartyElement = new PartyElement(character, nextFreePosition);
        partyElements.Add(newPartyElement);
    }

    public void RemoveCharacterFromParty(GameObject character)
    {
        if (!FindPartyMember(character)) return;
        partyElements.Remove(FindPartyElement(character));
    }

    public void SelectPartyMember(GameObject partyMember)
    {
        selectedPartyMember = FindPartyElement(partyMember).GetCharacterPartyPosition();
    }

    private PartyElement FindPartyElement(GameObject character)
    {
        return partyElements.Find(partyElement => partyElement.GetCharacterObject() == character);
    }

    public int FindNextOpenPartySlot()
    {
        partyElements.Sort((partyElement1, partyElement2) => partyElement1.GetCharacterPartyPosition().CompareTo(partyElement2.GetCharacterPartyPosition()));
        for (int i = 0; i < partyElements.Count; i++)
        {
            if (partyElements[i].GetCharacterPartyPosition() != i)
            {
                return i;
            }
        }
        return partyElements.Count;
    }
    
    public void ChangePartyPosition(GameObject character, int position)
    {
        PartyElement partyMember = partyElements.Find(partyElement => partyElement.GetCharacterObject() == character);
        if (partyMember != null)
        {
            partyMember.SetPartyPosition(position);
        }
    }


    public GameObject GetPartyElement(int partySlot)
    {
        return partyElements.Find(partyElement => partyElement.GetCharacterPartyPosition() == partySlot)?.GetCharacterObject();
    }

    public GameObject FindPartyMember(GameObject character)
    {
        return FindPartyElement(character)?.GetCharacterObject();
    }
}


[Serializable]
public class PartyElement
{
    [SerializeField] private GameObject characterGameObject;
    [SerializeField] private int partyPosition;

    public PartyElement(GameObject characterGameObject, int partyPosition)
    {
        this.characterGameObject = characterGameObject;
        this.partyPosition = partyPosition;
    }
    
    public GameObject GetCharacterObject()
    {
        return characterGameObject;
    }

    public int GetCharacterPartyPosition()
    {
        return partyPosition;
    }

    public void SetPartyPosition(int position)
    {
        partyPosition = position;
    }
}