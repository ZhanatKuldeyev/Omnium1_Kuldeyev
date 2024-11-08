
public class CharacterDamageComponent : IDamageComponent
{
    public float Damage => 10;

    public void MakeDamage(Character characterTarget)
    {
        if (characterTarget.HealthComponent != null) 
        characterTarget.HealthComponent.SetDamage(Damage);
    }
}
