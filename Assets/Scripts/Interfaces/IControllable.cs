using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void PerformAttack(IAttack attack,IControllable target);
    void PerformMove(Vector3 destination);
    void ApplyAttackResults(AttackResult attackResult);
}
