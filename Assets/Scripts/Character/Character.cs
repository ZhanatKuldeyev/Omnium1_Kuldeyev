using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected CharacterData characterData;

    public IMovable MovableComponent { get; protected set; }

    public IHealthComponent HealthComponent { get; protected set; }

    public IDamageComponent DamageComponent { get; protected set; }
    public CharacterData CharacterData { get => characterData; }


    public virtual void Start()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(characterData);

        HealthComponent = new CharacterHealthComponent();
        HealthComponent.Initialize(this);
    }


    public abstract void Update();
}
