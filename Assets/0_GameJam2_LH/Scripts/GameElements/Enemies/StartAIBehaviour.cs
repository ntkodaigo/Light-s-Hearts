using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    public class StartAIBehaviour : MonoBehaviour
    {
        public EnemyBaseBehaviour enemyBehaviour;

        /// <summary>
        /// Called in object with a Renderer Component
        /// </summary>
        void OnBecameVisible()
        {
            enemyBehaviour.StartCustomBehaviour();
        }
    }
}