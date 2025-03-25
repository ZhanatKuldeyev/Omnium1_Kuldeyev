using UnityEngine;

namespace ZombieIo
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected CharacterType characterType;
        [SerializeField] protected CharacterData characterData;

        public CharacterType CharacterType => characterType;
        public CharacterData CharacterData => characterData;


        public abstract Character CharacterTarget { get; }
        public IMovable MovableComponent { get; protected set; }

        public IHealthComponent HealthComponent { get; protected set; }

        public IDamageComponent DamageComponent { get; protected set; }

        public IAnimationComponent AnimationComponent { get; protected set; }


        public virtual void Initialize()
        {
            MovableComponent = new CharacterMovementComponent();
            MovableComponent.Initialize(characterData);

            HealthComponent = new CharacterHealthComponent();
            HealthComponent.Initialize(this);
        }


        public abstract void Update();
    }
}
