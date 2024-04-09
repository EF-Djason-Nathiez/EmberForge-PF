using System.Threading.Tasks;
using UnityEngine.AI;

namespace Artificial_Intelligence
{
    public class MovementModuleReader
    {
        private NavMeshAgent agent;
        private MovementModule currentMovementModule;

        public void Initialize(MovementModule movementModule, NavMeshAgent agent)
        {
            currentMovementModule = movementModule;
            currentMovementModule.target = GameRepository.instance.playerManager.player;
            
            this.agent = agent;
            agent.speed = currentMovementModule.moveSpeed;
            
            SetWalkableArea();
            SetMovementMode();
        }

        private void SetWalkableArea( )
        {
            switch (currentMovementModule.moveType)
            {
                case MoveType.Walk:
                    break;
                case MoveType.Swim:
                    break;
                case MoveType.Fly:
                    break;
            }
        }

        private void SetMovementMode()
        {
            switch (currentMovementModule.destinationType)
            {
                case DestinationType.ToPoints:
                    MoveToPoints();
                    break;
                
                case DestinationType.ToTarget:
                    MoveToTarget();
                    break;
                
                case DestinationType.FreeInRangeFromPoint:
                    MoveAround();
                    break;
            }
        }

        private void MoveToTarget()
        {
            agent.SetDestination(currentMovementModule.target.transform.position);
        }

        private async void MoveToPoints()
        {
            agent.SetDestination(currentMovementModule.path.Step());
            await Task.FromResult(agent.destination = agent.transform.position);
            await Task.Delay(1000);
            
            MoveToPoints();
        }

        private  void MoveAround()
        {
            
        }

        public void DisableBehavior()
        {
            
        }
    }
}