using UnityEngine;

namespace Player.PlayerComponents
{
    public class RaycastDetector : PlayerComponent
    {
        public GameObject GetObjectInFrontRay(Vector3 origin, Vector3 direction, float lenght)
        {
            Ray ray = new Ray(origin, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hit, lenght))
            {
                return hit.collider.gameObject;
            }
            
            return null;
        }

        public GameObject[] GetObjectsInFrontRectangle(Vector3 origin, float width, float height)
        {
            Collider[] hits = Physics.OverlapBox(origin, new Vector3(width, 1, height));
            return GetObjectsFromColliderArray(hits);
        }

        public GameObject[] GetObjectsInFrontCone(Vector3 origin, Vector3 direction, float radius, float angle)
        {
            Collider[] hits = Physics.OverlapSphere(origin, radius);

            GameObject[] hitObjects = GetObjectsFromColliderArray(hits);
            GameObject[] selectedObjects = new GameObject[hitObjects.Length];

            for (int i = 0; i < hitObjects.Length -1 ; i++)
            {
                float angleBetween = Vector3.Angle(direction, hitObjects[i].transform.position);

                if (angleBetween < angle)
                {
                    selectedObjects[i] = hitObjects[i];
                }
            }

            if (selectedObjects.Length != 0)
            {
                return selectedObjects;
            }

            return null;
        }

        public GameObject[] GetObjectsAroundInCircle(Vector3 origin, float radius)
        {
            Collider[] hits = Physics.OverlapSphere(origin, radius);

            return GetObjectsFromColliderArray(hits);
        }

        private GameObject[] GetObjectsFromColliderArray(Collider[] colliders)
        {
            if (colliders.Length != 0)
            {
                GameObject[] hitObjects = new GameObject[colliders.Length];

                for (int i = 0; i < colliders.Length - 1; i++)
                {
                    hitObjects[i] = colliders[i].gameObject;
                }

                return hitObjects;
            }

            return null;
        }
    }
}