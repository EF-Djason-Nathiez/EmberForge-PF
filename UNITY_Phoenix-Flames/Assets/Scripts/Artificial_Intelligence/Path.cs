using System.Collections.Generic;
using UnityEngine;

namespace Artificial_Intelligence
{
    public class Path
    {
        public List<Vector3> points;
        public int index;

        public void Initialize()
        {
            //set the object at points[0];
        }
        //TODO : Make the y axis to 0 always.
        public Vector3 Step()
        {
            if (index < points.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        
            return points[index];
        }
    
        public void AddPoint(Vector3 p)
        {
            if (!points.Contains(p))
            {
                points.Add(p);
            }
        }

        public void RemovePoint(Vector3 p)
        {
            if (points.Contains(p))
            {
                points.Remove(p);
            }
        }
    }
}