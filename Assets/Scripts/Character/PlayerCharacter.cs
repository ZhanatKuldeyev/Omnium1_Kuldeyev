using UnityEngine;

    public class PlayerCharacter : Character
    {
        public override Character CharacterTarget
        {
            get
            {
                Character target = null;
                float nearest = float.MaxValue;
                var activePool = GameManager.Instance.CharacterFactory.ActivePool;
                foreach (var activeCharacter in activePool)
                {
                    if (activeCharacter.CharacterType == CharacterType.DefaultPlayer)
                        continue;

                    if (!activeCharacter.HealthComponent.IsAlive)
                        continue;

                    float distance = Vector3.Distance(activeCharacter.transform.position, transform.position);
                    if (distance < nearest)
                    {
                        nearest = distance;
                        target = activeCharacter;
                    }
                }

            return target;

        }
    }

        private void Awake()
        {
            MovableComponent = gameObject.AddComponent<CharacterMovementComponent>();

            if (characterData == null)
            {
                Debug.LogError("characterData is NULL in PlayerCharacter!");
                return;
            }

            MovableComponent.Initialize(characterData);

            if (MovableComponent == null)
            {
            Debug.LogError("MovableComponent не был добавлен!");
            }
    }

        public override void Initialize()
        {
            base.Initialize();

            HealthComponent = new CharacterHealthComponent();

        //    AttackComponent = new WeaponAttackComponent();
        //    AttackComponent.Initialize(this);
    }

        public override void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

            if (CharacterTarget == null)
            {
                MovableComponent.Rotation(movementVector);
            }
            else
            {

                Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
                MovableComponent.Rotation(rotationDirection);

                if (Input.GetButtonDown("Jump"))
                    DamageComponent.MakeDamage(CharacterTarget);
            }

        }
    }
