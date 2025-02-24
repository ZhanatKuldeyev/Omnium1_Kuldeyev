using System;
using UnityEngine;

public class ImmortalHealthComponent : IHealthComponent
{
    float IHealthComponent._MaxHealth
    {
        get => 1;
        set { }
    }


    public void Initialize(Character character)
    {
        throw new NotImplementedException();
    }

    public event Action<Character> OnCharacterHealthChange;
    public event Action<Character> OnCharacterDeath;
    public float Health { get; set; }
    public int HealthMax { get; }
    public bool IsAlive { get; }

    float IHealthComponent._Health
    {
        get => 1;
        set { }
    }

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal");
    }
    
}
