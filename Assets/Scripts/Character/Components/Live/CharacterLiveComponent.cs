using System;
using UnityEngine;


    public class CharacterHealthComponent : IHealthComponent
    {
        private Character selfCharacter;
        private float _health;
        private int _healthMax;


        public event Action<Character> OnCharacterHealthChange;
        public event Action<Character> OnCharacterDeath;


        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, _healthMax);
                OnCharacterHealthChange?.Invoke(selfCharacter);

                if (_health > 0)
                    return;

                _health = 0;
                OnCharacterDeath?.Invoke(selfCharacter);
                Debug.LogError("Character death");
            }
        }

        public int HealthMax =>
            _healthMax;

        public bool IsAlive =>
            _health > 0;

        public float _MaxHealth { get; set; }
        public float _Health { get; set; }

        public void SetDamage(float damage)
        {
            if (damage > _MaxHealth)
            {
                _health = 0;
            }
            else
            {
                if (_health < 0)
                {
                    _health = 0;
                }

                _health -= damage;
            }
        }


        public void Initialize(Character selfCharacter)
        {
            this.selfCharacter = selfCharacter;
            _healthMax = selfCharacter.CharacterData.baseHealth;
            _health = selfCharacter.CharacterData.baseHealth;
        }
    }
