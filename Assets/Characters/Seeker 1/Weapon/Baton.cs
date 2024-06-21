using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baton : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Character>().ApplyAttackResults(new AttackResult(damage, 0));
        }
    }
}
