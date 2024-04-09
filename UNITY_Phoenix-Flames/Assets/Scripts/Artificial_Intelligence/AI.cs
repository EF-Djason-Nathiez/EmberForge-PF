using UnityEngine;

namespace Artificial_Intelligence
{
    [CreateAssetMenu(menuName = "AI/Create a AI", fileName = "new npc", order = 0)]
    public class AI : ScriptableEntity
    {
        [HideInInspector] public GameObject go;
        public AttackBehavior attackBehavior;
        public ChaseBehavior chaseBehavior;
        public AIFollowBehavior followBehavior;
        public AIPatrolBehavior patrolBehavior;

        public Range followRange;
        public Range attackRange;
        public Distance triggerDistance;
        public Distance interactDistance;
    
        public void Initialize(GameObject gameObject)
        {
            go = gameObject;
            followRange = new Range();
            attackRange = new Range();
            triggerDistance = new Distance();
            interactDistance = new Distance();
        }
    }

    public class Range : DetectionValue
    {
        public float minRange;
        public float maxRange;

        public bool InRange(float value)
        {
            return value < maxRange && value > minRange;
        }
    }

    public class Distance : DetectionValue
    {
        public float distance;

        public bool InDistance(float value)
        {
            return value < distance;
        }
    }

    public class DetectionValue
    {
    
    }
}