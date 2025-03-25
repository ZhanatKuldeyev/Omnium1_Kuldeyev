using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyharacter : Character
{
    [SerializeField] private AiState currentState;


    private float timeBetweenAttackCounter = 0;

    public override Character CharacterTarget => GameManager.Instance?.CharacterFactory?.PlayerCharacter;

    void Start()
    {
        StartCoroutine(WaitForPlayer());
    }

    IEnumerator WaitForPlayer()
    {
        while (GameManager.Instance == null ||
               GameManager.Instance.CharacterFactory == null ||
               GameManager.Instance.CharacterFactory.PlayerCharacter == null)
        {
            yield return null; // Ждать следующего кадра
        }

        // Теперь PlayerCharacter точно существует
    }


    [SerializeField]
    private AiState aiState;

    public override void Initialize()
    {
        base.Initialize();

        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        if (CharacterTarget == null) return;

        switch (currentState) 
        { 
            case AiState.None:

                break;

            case AiState.MoveToTarget:
                Vector3 direction = CharacterTarget.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3
                    && timeBetweenAttackCounter <= 0)
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweenAttackCounter = characterData.TimeBetweenAttacks;
                }

                if (timeBetweenAttackCounter > 0)
                    timeBetweenAttackCounter -= Time.deltaTime;

                break;
        }

    }
}
