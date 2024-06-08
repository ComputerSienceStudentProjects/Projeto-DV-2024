using System.Collections.Generic;
using UnityEngine;
public interface IAIControllable
{
    public void FindLocation();

    public int FindClosestTarget(List<GameObject> playerParty);

}