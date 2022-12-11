using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Play 
    {
        public interface IBuilder
        {
            public void StartBuildPlatforms(int platformsValue, Transform[] platforms);
        }
    }
}