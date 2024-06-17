using System;

[Serializable]
public class AiData
{
    private float attackRange = 5f;
    private float movementDistance = 5f;
    private float attackDamage = 1f;


    public float getAttackRange => attackRange;

    public float getMovementDistance => movementDistance;

    public float getAttackDamage => attackDamage;

    public void setAttackRange(float range)
    {
        attackRange = range;
    }
}