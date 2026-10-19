using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util{
    public class DisableComponentsOnStart : MonoBehaviour{
        public Behaviour[] components;
        void Start()
        {
           foreach(Behaviour b in components)
            {
                b.enabled=false;
            }
        }
    }
}