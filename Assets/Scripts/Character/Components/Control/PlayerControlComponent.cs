using UnityEngine;
using ZombieIo.Input;


public class PlayerControlComponent : IControlComponent
{
    private Character character;
    private IInputService inputService;


    private IMovable MovableComponent =>
        character.MovableComponent;

    private IDamageComponent DamageComponent =>
        character.DamageComponent;


    public void Initialize(Character character)
    {
        this.character = character;
        inputService = GameManager.Instance.InputService;
    }

    public void OnUpdate()
    {
        float x = inputService.Direction.x;
        float z = inputService.Direction.y;

        Vector3 moveDirection = new Vector3(x, 0, z).normalized;
        MovableComponent.Move(moveDirection);

        if (character.Target == null || !character.Target.HealthComponent.IsAlive)
        {
            MovableComponent.Rotation(moveDirection);
        }
        else
        {
            Vector3 directionToTarget = character.Target.transform.position
                - character.CharacterData.CharacterTransform.position;
            directionToTarget.Normalize();
            MovableComponent.Rotation(directionToTarget);

            DamageComponent.OnUpdate();
            DamageComponent.MakeAttack();
        }
    }
}
