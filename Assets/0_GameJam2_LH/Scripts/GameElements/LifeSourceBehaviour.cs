using UnityEngine;
using System.Collections;
using System;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(Collider))]
    public partial class LifeSourceBehaviour : MonoBehaviour, ITakeDamage
    {
        public int health;
        public bool isSource;
        public ParticleSystem partSystem;

        void Awake()
        {
            var emi = partSystem.emission;
            emi.enabled = true;
        }

        public void TakeDamage(int damage, Vector3 force)
        {
            if (health == 0)
                return;

            health -= damage;

            if (health <= 0)
            {
                health = 0;
                partSystem.Play();

                Destroy(gameObject, partSystem.duration + 0.5f);

                if (isSource)
                {
                    GameManager.Instance.KillLastBoss();
                    GameManager.Instance.GameState = LightsHeartsEnums.GameState.GameOver;
                    UIManager.Instance.ShowWonWindow();
                }
            }
        }
    }
}
