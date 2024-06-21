using System.Collections.Generic;
using UnityEngine;
public interface IAIControllable
{

    public int FindClosestTarget(List<GameObject> playerParty);

}