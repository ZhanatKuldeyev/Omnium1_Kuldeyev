using UnityEngine;

namespace ZombieIo.Input
{
    public class NewInputService : IInputService
    {
        private PlayerInput playerInput;


        public Vector2 Direction =>
            playerInput.Game_map.Movement.ReadValue<Vector2>();

        public bool Attack =>
            playerInput.Game_map.Attack.ReadValue<bool>();

        public bool Skill =>
            playerInput.Game_map.Skill.ReadValue<bool>();


        public NewInputService()
        {
            playerInput = new PlayerInput();
            playerInput.Game_map.Enable();
        }
    }
}
