using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;

namespace Enigma.LightsHearts
{
    [AddComponentMenu("LightsHearts/Managers/Game manager")]
    public partial class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public LightsHeartsEnums.GameState GameState { get; set; }

        // Cinematics stuff
        public ThirdPersonUserControl userControl;
        public HeroBaseBehaviour hero;
        public Transform heroClone;
        public AntiheroBehaviour antihero;
        public ParticleSystem partSystem;
        public SmoothFollow smoothFollow;
        public Transform antiheroGoPoint;
        public AntiheroBehaviour lastBoss;

        // Checkpoints
        [SerializeField]
        private CheckPointBehaviour _currentCheckPoint;
        public CheckPointBehaviour CurrentCheckPoint { get { return _currentCheckPoint; } set { _currentCheckPoint = value; } }

        // Lifesources
        public LifeSourceBehaviour[] lifeSources;

        void Start()
        {
            Instance = this;

            GameState = LightsHeartsEnums.GameState.Playing;

            userControl.enabled = false;

            // Allow Play() ParticleSystem even PlayOnAwake is not set 
            var emi = partSystem.emission;
            emi.enabled = true;

            ChoiceRandomLifeSource();
        }

        public void Cinematics_1()
        {
            StartCoroutine(Cinematics_1C());
        }

        IEnumerator Cinematics_1C()
        {
            partSystem.Play();

            yield return new WaitForSeconds(2f);

            heroClone.gameObject.SetActive(false);
            antihero.gameObject.SetActive(true);

            yield return new WaitForSeconds(4f);

            antihero.SetTarget(antiheroGoPoint);

            smoothFollow.Target = antihero.transform;
            smoothFollow.enabled = true;

            yield return new WaitForSeconds(4f);

            smoothFollow.Target = userControl.transform;
            userControl.enabled = true;
            hero.Init();

            antihero.gameObject.SetActive(false);
        }

        void ChoiceRandomLifeSource()
        {
            // Final en Muerte Instantanea
            var rd = Random.Range(0, lifeSources.Length);

            lifeSources[rd].isSource = true;
        }

        public void KillLastBoss()
        {
            lastBoss.TakeDamage(50000, Vector3.zero);
        }

        public void ReloadScene()
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        public void LoadScene(int number)
        {
            SceneManager.LoadScene(number, LoadSceneMode.Single);
        }
    }
}
