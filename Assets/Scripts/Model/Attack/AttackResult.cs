public class AttackResult
{
    private float _resultingDamage;
    private float _resultingHealing;

    public AttackResult(float resultingDamage,float resultingHealing)
    {
        this._resultingHealing = resultingHealing;
        this._resultingDamage = resultingDamage;
    }

    public float ResultingDamage => this._resultingDamage;
    public float ResultingHealing => this._resultingHealing;
}