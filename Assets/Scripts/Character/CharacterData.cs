
using UnityEngine;


public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private int ScoreManager;
    [SerializeField] private Animator animator;


    public int baseHealth;
    public float baseDamage;

    public float DefaultSpeed => speed;

    public Animator Animator => animator;

    public float TimeBetweenAttacks => timeBetweenAttacks;

    public Transform CharacterTransform => characterTransform;

    public CharacterController CharacterController
    {
        get
        {
            return characterController;
        }
    }

    public int ScoreCost => ScoreManager;
}
