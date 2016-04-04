using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;
using System.Collections;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [AddComponentMenu("LightsHearts/Entities/Hero base behaviour")]
    public partial class HeroBaseBehaviour : MonoBehaviour, ITakeDamage
    {
        #region Database fields
        public EntityBaseData entityData;

        public CombatBaseData combatData;

        [SerializeField]
        private float _movementSpeed;
        public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }

        [SerializeField]
        private int _health;
        public int Health { get { return _health; } set { _health = value; } }

        [SerializeField]
        private float _timeLife;
        public float TimeLife { get { return _timeLife; } set { _timeLife = value; } }

        /// <summary>
        /// The weapon of the hero
        /// </summary>
        [SerializeField]
        private uint _weaponID;
        public WeaponBaseBehaviour Weapon
        {
            get
            {
                foreach (var weapon in DatabaseManager.GetDatabase().weapons)
                {
                    if (weapon.entityData.ID == _weaponID)
                    {
                        return weapon;
                    }
                }

                Debug.LogError("Couldn't find weapon. (HeroID #" + entityData.ID + ")");
                return null;
            }
            set
            {
                _weaponID = value.entityData.ID;
            }
        }
        #endregion

        #region delegates and events
        public delegate void AddedWeapon(WeaponBaseBehaviour weapon);
        public static event AddedWeapon OnAddedWeapon;

        public delegate void EquippedWeapon(int weaponIndex);
        public static event EquippedWeapon OnEquippedWeapon;

        public delegate void TakedDamage(int damage, float healthPercentage);
        public static event TakedDamage OnTakedDamage;

        public delegate void ReturnedToLife();
        public static event ReturnedToLife OnReturnedToLife;

        public delegate void Death();
        public static event Death OnDeath;
        #endregion

        public bool IsDead { get { return Health == 0; } }
        public float HealthPercentage { get { return (float)Health / _initialHealth; } }

        public Transform weaponLocationT;
        public List<WeaponBaseBehaviour> weapons = new List<WeaponBaseBehaviour>();

        private int _previousWeaponIndex = -1;
        private ThirdPersonCharacter _character;
        private int _initialHealth;

        void Awake()
        {
            _character = GetComponent<ThirdPersonCharacter>();

            _initialHealth = Health;
        }

        void OnEnable()
        {
            OnEquippedWeapon += UpdateAnimator;
            OnDeath += ReturnToLife;
        }

        void OnDisable()
        {
            OnEquippedWeapon -= UpdateAnimator;
            OnDeath -= ReturnToLife;
        }

        public void Init()
        {
            var newWeapon = Weapon;

            if (newWeapon != null)
            {
                AddWeapon(newWeapon, true);

                SwitchCombatMode(LightsHeartsEnums.CombatMode.TwoHandsWeapon);
            }
            else
                SwitchCombatMode(LightsHeartsEnums.CombatMode.Weaponless);

            StartCoroutine(UpdateTimerUIC());
        }

        public void StartAttack()
        {
            _character.Animator.SetBool("OnAttack", true);
        }

        void CastAttack()
        {
            _character.Animator.SetBool("OnAttack", false);

            var weapon = weapons[_previousWeaponIndex];
            var targets = Physics.OverlapSphere(weapon.attackLocationT.position, weapon.combatData.BaseAttackRange);

            foreach (var target in targets)
            {
                var takeDamage = target.GetComponent<ITakeDamage>();
                if (takeDamage != null)
                {
                    if (target.CompareTag("Player"))
                        continue;

                    if (Vector3.Angle(target.transform.position - transform.position, transform.forward) < 70)
                    {
                        var force = (target.transform.position - weapon.transform.position).normalized * 16;
                        takeDamage.TakeDamage(weapon.combatData.Damage, force);

                        var enemyBeh = target.GetComponent<EnemyBaseBehaviour>();
                        if (enemyBeh)
                            UIManager.Instance.UpdateEnemyPanelUI(enemyBeh.HealthPercentage, enemyBeh.entityData.Icon);
                    }
                }
            }
        }

        public void AddWeapon(WeaponBaseBehaviour weapon, bool equip)
        {
            WeaponBaseBehaviour newWeapon;

            if (!IsCarriedWeapon(weapon.entityData.ID))
            {
                newWeapon = Instantiate(weapon) as WeaponBaseBehaviour;
                newWeapon.GetComponent<Collider>().enabled = false;
                newWeapon.transform.SetParent(weaponLocationT);
                newWeapon.transform.localPosition = Vector3.zero;
                newWeapon.transform.localRotation = Quaternion.identity;
                newWeapon.transform.localScale = Vector3.one;
                newWeapon.gameObject.SetActive(false);
                weapons.Add(newWeapon);

                if (OnAddedWeapon != null)
                    OnAddedWeapon(newWeapon);

                if (equip)
                {
                    SwitchWeapon(weapons.Count - 1);
                }
            }
        }

        bool IsCarriedWeapon(uint weaponID)
        {
            foreach (var weapon in weapons)
            {
                if (weaponID == weapon.entityData.ID)
                    return true;
            }

            return false;
        }

        public void SwitchWeapon(int weaponindex)
        {
            if (weaponindex != _previousWeaponIndex)
            {
                weapons[weaponindex].gameObject.SetActive(true);

                if (_previousWeaponIndex != -1)
                    weapons[_previousWeaponIndex].gameObject.SetActive(false);

                _previousWeaponIndex = weaponindex;

                if (OnEquippedWeapon != null)
                    OnEquippedWeapon(weaponindex);
            }               
        }

        void UpdateAnimator(int weaponIndex)
        {
            var weapon = weapons[weaponIndex];
            _character.Animator.SetFloat("AttackSpeed", weapon.combatData.BaseAttackSpeed);
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

        public void TakeDamage(int damage, Vector3 force)
        {
            if (IsDead)
                return;

            Health -= damage;

            if (Health > 0)
            {
                _character.Animator.SetTrigger("GetHit");
            }
            else
            {
                Health = 0;

                _character.Animator.SetBool("IsDead", true);
                _character.Animator.SetTrigger("Death");

                if (OnDeath != null)
                    OnDeath();
            }

            if (OnTakedDamage != null)
                OnTakedDamage(damage, HealthPercentage);

            if (force != Vector3.zero)
                _character.Rigidbody.AddForce(force, ForceMode.Impulse);
        }

        void ReturnToLife()
        {
            if (TimeLife <= 0)
                return;

            StartCoroutine(ReturnToLifeC());
        }

        IEnumerator ReturnToLifeC()
        {
            yield return new WaitForSeconds(5f);

            Health = _initialHealth;

            _character.Animator.SetBool("IsDead", false);

            transform.position = GameManager.Instance.CurrentCheckPoint.transform.position;

            if (OnReturnedToLife != null)
                OnReturnedToLife();
        }

        IEnumerator UpdateTimerUIC()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                if (TimeLife <= 0)
                {
                    GameManager.Instance.GameState = LightsHeartsEnums.GameState.GameOver;
                    UIManager.Instance.ShowGameOverWindow();

                    yield break;
                }

                TimeLife -= 1f;

                UIManager.Instance.UpdateTimer(TimeLife);
            }
        }
    }
}
