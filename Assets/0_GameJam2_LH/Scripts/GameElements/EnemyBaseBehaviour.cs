using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [AddComponentMenu("LightsHearts/Entities/Enemy base behaviour")]
    public partial class EnemyBaseBehaviour : MonoBehaviour, ITakeDamage
    {
        #region Database fields
        public EntityBaseData entityData;

        public CombatBaseData combatData;

        [SerializeField]
        private LightsHeartsEnums.EnemyType _enemyType;
        public LightsHeartsEnums.EnemyType EnemyType { get { return _enemyType; } set { _enemyType = value; } }

        [SerializeField]
        private float _movementSpeed;
        public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }

        [SerializeField]
        private int _health;
        public int Health { get { return _health; } set { _health = value; } }

        /// <summary>
        /// The spell of the enemy
        /// </summary>
        [SerializeField]
        public uint _spellID;
        public SpellBaseBehaviour Spell
        {
            get
            {
                foreach (var spell in DatabaseManager.GetDatabase().spells)
                {
                    if (spell.entityData.ID == _spellID)
                    {
                        return spell;
                    }
                }

                Debug.LogError("Couldn't find spell. (EnemyID #" + entityData.ID + ")");
                return null;
            }
            set
            {
                _spellID = value.entityData.ID;
            }
        }
        #endregion

        #region delegates and events
        public delegate void TakedDamage();
        public event TakedDamage OnTakedDamage;

        public delegate void Death();
        public event Death OnDeath;
        #endregion

        public bool IsDead { get { return Health == 0; } }
        public float HealthPercentage { get { return (float) Health / _initialHealth; } }
        public NavMeshAgent Agent { get; private set; }

        private ThirdPersonCharacter _character;
        private Transform _targetT;
        private HeroBaseBehaviour _targetHero;
        private int _initialHealth;

        public float visionRange;
        public float visionAngle;
        public Transform[] patrolPoints;
        public LightsHeartsEnums.CombatMode combatMode;

        void Awake()
        {
            _character = GetComponent<ThirdPersonCharacter>();
            Agent = GetComponentInChildren<NavMeshAgent>();

            Agent.updateRotation = true;
            Agent.updatePosition = true;

            _initialHealth = Health;
        }

        void OnEnable()
        {
            OnDeath += Desactive;
        }

        void OnDisable()
        {
            OnDeath -= Desactive;
        }

        void Start()
        {
            UpdateAnimator();

            SwitchCombatMode(combatMode);
        }

        void UpdateAnimator()
        {
            _character.Animator.SetFloat("AttackSpeed", combatData.BaseAttackSpeed);
        }

        public void SetTarget(Transform t)
        {
            _targetT = t;
        }

        public void TakeDamage(int damage, Vector3 force)
        {
            if (IsDead)
                return;

            Health -= damage;

            if (Health > 0)
            {
                _character.Animator.SetTrigger("GetHit");

                if (OnTakedDamage != null)
                    OnTakedDamage();
            }
            else
            {
                Health = 0;

                _character.Animator.SetBool("IsDead", true);
                _character.Animator.SetTrigger("Death");

                if (OnDeath != null)
                    OnDeath();
            }

            _character.Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        void Desactive()
        {
            StartCoroutine(DesactiveC());
        }

        IEnumerator DesactiveC()
        {
            Agent.enabled = false;
            _character.Rigidbody.isKinematic = true;
            _character.enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(5f);

            gameObject.SetActive(false);
        }

        public void StartCustomBehaviour()
        {
            StopCoroutine(CustomBehaviourC());
            StartCoroutine(CustomBehaviourC());
        }

        protected virtual IEnumerator CustomBehaviourC()
        {
            while (true)
            {
                if (IsDead || GameManager.Instance.GameState == LightsHeartsEnums.GameState.GameOver)
                    yield break;

                if (_targetT)
                    Agent.SetDestination(_targetT.position);
                else if (_targetHero && !_targetHero.IsDead)
                {
                    Agent.SetDestination(_targetHero.transform.position);

                    CheckAttack();
                }
                else
                {
                    SearchHero();
                }

                if (Agent.remainingDistance > Agent.stoppingDistance)
                    _character.Move(Agent.desiredVelocity, false, false);
                else
                    _character.Move(Vector3.zero, false, false);

                yield return new WaitForSeconds(0.1f);
            }
        }

        void SearchHero()
        {
            var heroes = Physics.OverlapSphere(transform.position, visionRange);

            foreach (var hero in heroes)
            {
                if (hero.CompareTag("Player"))
                {
                    if (Vector3.Angle(hero.transform.position - transform.position, transform.forward) < visionAngle)
                        _targetHero = hero.GetComponent<HeroBaseBehaviour>();
                }
            }
        }

        void CheckAttack()
        {
            if (Vector3.Distance(_targetHero.transform.position, transform.position) < combatData.BaseAttackRange)
            {
                StartAttack();
            }
        }

        void StartAttack()
        {
            _character.Animator.SetBool("OnAttack", true);
        }

        void CastAttack()
        {
            _character.Animator.SetBool("OnAttack", false);

            if (Vector3.Distance(_targetHero.transform.position, transform.position) < combatData.BaseAttackRange)
            {
                _targetHero.TakeDamage(combatData.Damage, Vector3.zero);
            }
        }

        public void SwitchCombatMode(LightsHeartsEnums.CombatMode combatMode)
        {
            switch (combatMode)
            {
                case LightsHeartsEnums.CombatMode.Weaponless:
                    _character.Animator.SetInteger("CombatIndex", 0);
                    break;
                case LightsHeartsEnums.CombatMode.TwoHandsWeapon:
                    _character.Animator.SetInteger("CombatIndex", 1);
                    break;
            }

            _character.Animator.SetBool("OnCombat", true);
        }
    }
}
