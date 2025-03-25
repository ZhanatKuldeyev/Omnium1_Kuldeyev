using System;

namespace ZombieIo
{
    public interface IHealthComponent : ICharacterComponent
    {
        public event Action<Character> OnCharacterHealthChange;
        public event Action<Character> OnCharacterDeath;


        public float Health { get; set; }

        public int HealthMax { get; }

        public bool IsAlive { get; }
        float _MaxHealth { get; set; }
        float _Health { get; set; }
        void SetDamage(float damage);
    }
}
