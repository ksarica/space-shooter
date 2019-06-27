using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common.ObjectPooling
{
    public class ObjectPool
    {
        GameObject prefab;
        private List<GameObject> pool;
        public ObjectPool(GameObject prefab)
        {
            pool = new List<GameObject>();
            this.prefab = prefab;
        }

        public void CreateAtPosition(Vector3 position, Quaternion rotation)
        {
            IPoolableObject available = GetNextAvailableObject();
            if (available != null)
            {
                available.ResetPoolObject(position);
                //Debug.Log("Create From Pool");
            }
            else
            {
                pool.Add(GameObject.Instantiate(prefab, position, rotation));
                //Debug.Log("Create New");
            }
        }

        private IPoolableObject GetNextAvailableObject()
        {
            foreach (GameObject go in pool)
            {
                if (!go.activeSelf)
                {
                    return go.GetComponent<IPoolableObject>();
                }
            }
            return null;
        }
    }
}
