using Canvas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;


namespace GameSystems 
{
    public class GameManager : IStartable
    {
        public RuntimePlatform platform { get; private set; }
        public void Start()
        {
            platform = Application.platform;
            Debug.Log(platform.ToString());
        }

        public void WasHit(GameObject gameObject)
        {
            Debug.Log(gameObject.name);
        }
    }
}


