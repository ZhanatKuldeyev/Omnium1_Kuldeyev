using System;
using UnityEngine;

public class ImmortalHealthComponent : IHealthComponent
{
    float IHealthComponent._MaxHealth { get => 1; set => { } };
    float IHealthComponent._Health { get => 1; set => { } };

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal");
    }
    
}
