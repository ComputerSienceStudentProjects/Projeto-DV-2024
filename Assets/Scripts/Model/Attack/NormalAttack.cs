using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Attacks/ Normal Attack",fileName = "New Normal Attack")]
public class NormalAttack : ScriptableObject, IAttack
{
    private readonly string _guid = Guid.NewGuid().ToString();
    
    [SerializeField] private string attackName = "Normal Attack";
    [SerializeField] private float damageValue;
    [SerializeField] private float healingValue;
    [SerializeField] private GameEvent attackAnimationObject;
    [SerializeField] private Sprite attackIcon;
    [SerializeField] private Color attackColor;
    [SerializeField] private Color textColor;
    
    
    public void Execute(IControllable target)
    {
        if (target == null) return;
        target.ApplyAttackResults(new AttackResult(damageValue,healingValue));
    }

    public Sprite GetIcon()
    {
        return attackIcon;
    }

    public Color GetColor()
    {
        return attackColor;
    }

    public Color GetTextColor()
    {
        return textColor;
    }

    public string GetName()
    {
        return attackName;
    }
}