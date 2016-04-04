using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(Collider))]
    public class CheckPointBehaviour : MonoBehaviour
    {
        public ParticleSystem partSystem;

        void Awake()
        {
            if (partSystem)
            {
                var emi = partSystem.emission;
                emi.enabled = true;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                partSystem.Play();

                GameManager.Instance.CurrentCheckPoint = this;
            }
        }

    }
}
