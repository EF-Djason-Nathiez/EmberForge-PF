using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Artificial_Intelligence
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIBrain : MonoBehaviour
    {
        public AI entity;
        [SerializeField] private GameObject aiObj;
        private NavMeshAgent agent;
        private StateMachine stateMachine;
        private MovementModuleReader movementReader;
        
        private void Awake()
        {
            if (!entity)
            {
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            stateMachine = new StateMachine();
            movementReader = new MovementModuleReader();
        
            if (!aiObj)
            {
                aiObj = gameObject;
            }
        
            entity.Initialize(aiObj);
            stateMachine.OnStateChanged += SetBehavior;
            
            stateMachine.SwitchState(AIState.OnPatrol);
        }

        void CheckForRange(float distance, AI ai)
        {
            if(ai != entity) return;

            bool trigger = entity.triggerDistance.InDistance(distance);
            bool follow = entity.followRange.InRange(distance);
            bool attack = entity.attackRange.InRange(distance);
            bool interact = entity.interactDistance.InDistance(distance);
        
            PriorEvents(trigger, follow, attack, interact);
        }

        void PriorEvents(bool trigger, bool follow, bool attack, bool interact)
        {
            switch (stateMachine.currentState)
            {
                case AIState.OnAttack:
                
                    if (!attack)
                    {
                        stateMachine.SwitchState(AIState.OnChase);
                    }

                    if (interact)
                    {
                        //CAN PET IF IT'S A PETABLE TARGET
                    }
                
                    break;
                case AIState.OnChase:
       
                    if (attack)
                    {
                        stateMachine.SwitchState(AIState.OnAttack);
                    }
                
                    if (interact)
                    {
                        //CAN PET IF IT'S A PETABLE TARGET

                    }
                    break;
                case AIState.OnFollow:
                
                    if (!follow)
                    {
                        //UPDATE SPEED
                        //if follow is near slow down, if follow is far speed up until follow is good
                    }
                 
                    if (interact)
                    {
                        //CAN PET
                        //CAN TALK
                    }
                 
                    break;
                case AIState.OnPatrol:
                    if (trigger)
                    {
                        stateMachine.SwitchState(AIState.OnChase);
                    }

                    if (interact)
                    {
                        //CAN PET
                        //CAN TALK
                    }
                
                    break;
            }
        }
        
        void SetBehavior(AIState state)
        {
            MovementModule _currentMovementModule = ScriptableObject.CreateInstance<MovementModule>();
            
            switch (state)
            {
                case AIState.OnPatrol:
                    _currentMovementModule = entity.patrolBehavior.movementModule;
                    break;
                case AIState.OnFollow:
                    _currentMovementModule = entity.followBehavior.movementModule;
                    break;
                case AIState.OnChase:
                    _currentMovementModule = entity.chaseBehavior.movementModule;
                    break;
                case AIState.OnAttack:
                    _currentMovementModule = entity.attackBehavior.movementModule;
                    break;
            }
            
            movementReader.Initialize(_currentMovementModule, agent);
            
        }

        private void OnBecameVisible()
        {
            if(!entity) return;
        
            entity.SubscribeToVisible();
            PhysicManager.instance.OnDistance += CheckForRange;
        }

        private void OnBecameInvisible()
        {
            if(!entity) return;
        
            entity.UnsubscribeToVisible();
            PhysicManager.instance.OnDistance -= CheckForRange;
        }
    }
}
