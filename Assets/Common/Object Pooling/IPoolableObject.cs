using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common.ObjectPooling
{
    public interface IPoolableObject
    {
        void ResetPoolObject(Vector3 position);
    }
}
