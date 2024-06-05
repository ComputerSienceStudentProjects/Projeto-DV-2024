using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    [SerializeField] 
    private string characterName = "John";
    [SerializeField]
    private float hp = 100;
    [SerializeField]
    private float def = 20;
    [SerializeField]
    private float atk = 20;
    
    public float GetHp => hp;
    public float GetDef => def;
    public float GetAtk => atk;
    public string GetName => characterName;

    public void SetHp(float newHp)
    {
        this.hp = newHp;
    }

    public void AddHp(float valueToAdd)
    {
        this.hp += valueToAdd;
    }
    
    public void SubtractHp(float valueToSubtract)
    {
        this.hp -= valueToSubtract;
    }
}