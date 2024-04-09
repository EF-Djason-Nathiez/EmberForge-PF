using Player.PlayerComponents;
using UnityEngine;

namespace Player
{
    public class PlayerManager : Entity
    {
        [SerializeField] public PlayerData data;
        public PlayerController controller;
        [SerializeField] public AbilityManager ability;
        public InventoryManager inventory;
        public PlayerInterfaceDrawer interfaceDrawer;
        
        public Transform player;
        public Transform target;
        
        
        private void InitializePlayer()
        {
            data.Initialize(this);
            controller.Initialize(this);
            ability.Initialize(this);
            inventory.Initialize(this);
            interfaceDrawer = new PlayerInterfaceDrawer();
            interfaceDrawer.Initialize(this);
        }

        public override void UpdateHealth(float amount)
        {
            base.UpdateHealth(amount);
            Debug.Log(CurrentHealth());
            interfaceDrawer.UpdatePlayerStateInformation();
        }

        public override void UpdateSpeed(float amount, float duration = 0)
        {
            base.UpdateSpeed(amount, duration);
            interfaceDrawer.UpdatePlayerStateInformation();
        }

        private void Awake()
        {
            InitializeEntity();
            InitializePlayer();
        }
    }
}
