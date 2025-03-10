
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;


    
    public int baseHealth;
    public float baseDamage;

    public float DefaultSpeed => speed;

    public float TimeBetweenAttacks => timeBetweenAttacks;

    public Transform CharacterTransform => characterTransform;

    public CharacterController CharacterController
    {
        get
        {
            return characterController;
        }
    }
}
