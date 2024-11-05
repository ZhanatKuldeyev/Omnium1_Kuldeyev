using Elasticsearch.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private float currentHealth;

    public float MaxHealth => 50; 
    

    public float Health
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            currentHealth = value;
            if (currentHealth <= 0)

            {
                currentHealth = 0;
                SetDeath();
            }
        }
    }

    public CharacterLiveComponent()
    {
        Health = MaxHealth;
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Get damage = " + damage);
    }
}


    private void SetDeath()
    {
        Debug.Log("Character is death");
    }
}
