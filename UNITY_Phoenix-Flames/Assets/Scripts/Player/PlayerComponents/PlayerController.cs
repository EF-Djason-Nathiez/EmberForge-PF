using System;
using UnityEngine;

namespace Player.PlayerComponents
{
    [Serializable] public class PlayerController : PlayerComponent
    {
        private Transform player;
        private PlayerInput _input;

        private Vector3 moveDir;
        
        public override void Initialize(PlayerManager pm)
        {
            base.Initialize(pm);
            
            player = manager.player;
            
            _input = new PlayerInput();
            _input.Enable();

            _input.Base.Primary.started += _ => manager.ability.primary.InputStarted?.Invoke();
            _input.Base.Primary.canceled += _ => manager.ability.primary.OnAbilityEnable?.Invoke();
            
            PhysicManager.instance.OnFixedUpdate += () =>
            {
                ReadInputs();
                Move();
            };
        }

        private void ReadInputs()
        {
            moveDir = new Vector3(_input.Base.Move.ReadValue<Vector2>().x, 0, _input.Base.Move.ReadValue<Vector2>().y);
        }
        
        private void Move()
        {
            if (moveDir.magnitude != 0)
            {
                player.position += moveDir * (manager.CurrentSpeed() * Time.deltaTime);
                
                Quaternion rot = Quaternion.LookRotation(moveDir);
                player.rotation = Quaternion.Slerp(player.rotation, rot, 8 * Time.deltaTime);
            }
        }
    }
}
