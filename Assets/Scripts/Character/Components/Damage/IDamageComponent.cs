using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent 
{
    float Damage { get; }
    public float DamageRange { get; }

    void MakeDamage(Character characterTarget);
    public void OnUpdate();
}
