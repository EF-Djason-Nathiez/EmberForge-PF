
using UnityEngine;

namespace Artificial_Intelligence
{
    [CreateAssetMenu(menuName = "AI/Create Movement Module", fileName = "new movement module", order = 0)]
    public class MovementModule : ScriptableObject
    {
        public MovementModuleReader moduleReader;
        
        public MoveType moveType;
        public float moveSpeed;
        public DestinationType destinationType;
        public Path path;
        public Transform target;
        public Vector3 pointZone;
        public float zoneRange;

        public void Initialize()
        {
            moduleReader = new MovementModuleReader();
        }
    }

    public enum MoveType
    {
        Walk,
        Fly,
        Swim
    }

    public enum DestinationType
    {
        ToPoints,
        ToTarget,
        FreeInRangeFromPoint
    }
}
