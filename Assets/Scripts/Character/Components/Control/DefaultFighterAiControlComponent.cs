using UnityEngine;

public class DefaultFighterAiControlComponent : IControlComponent
{
    private const float TARGET_DISTANCE = 0.5f;


    private Character character;
    private AiState aiState;


    private IMovable MovableComponent =>
        character.MovableComponent;

    private IDamageComponent DamageComponent =>
        character.DamageComponent;


    public void Initialize(Character character)
    {
        this.character = character;
        aiState = AiState.MoveToTarget;
    }

    public void OnUpdate()
    {
        if (character.Target == null || !character.Target.gameObject.activeSelf)
            return;

        Vector3 direction = character.Target.transform.position
            - character.CharacterData.CharacterTransform.position;

        switch (aiState)
        {
            case AiState.Idle:

                return;

            case AiState.MoveToTarget:
                direction = direction.normalized;
                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);
                if (Vector3.Distance(character.Target.transform.position,
                        character.CharacterData.CharacterTransform.position)
                    <= TARGET_DISTANCE)
                {
                    aiState = AiState.Damage;
                }

                return;

            case AiState.Damage:
                MovableComponent.Move(Vector3.zero);

                direction = direction.normalized;
                MovableComponent.Rotation(direction);

                DamageComponent.MakeDamage();

                if (Vector3.Distance(character.Target.transform.position,
                        character.CharacterData.CharacterTransform.position)
                    > TARGET_DISTANCE)
                    aiState = AiState.MoveToTarget;
                return;
        }
    }
}

