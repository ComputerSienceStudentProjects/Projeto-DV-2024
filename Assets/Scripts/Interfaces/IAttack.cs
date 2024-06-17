using UnityEngine;

public interface IAttack
{
    void Execute(IControllable target);
    Sprite GetIcon();
    Color GetColor();
    Color GetTextColor();
    string GetName();
}