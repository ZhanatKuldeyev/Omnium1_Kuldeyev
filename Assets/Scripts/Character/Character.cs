using UnityEngine;


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

        public IControlComponent ControlComponent { get; protected set; }


        public abstract Character Target { get; }



    public virtual void Initialize()
        {
            MovableComponent = new CharacterMovementComponent();
            MovableComponent.Initialize(this);

		    HealthComponent = new CharacterHealthComponent();
		    HealthComponent.Initialize(this);

		    AnimationComponent = new CharacterAnimationComponent();
		    AnimationComponent.Initialize(this);

        }


        public abstract void Update();
    }
