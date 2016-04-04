using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    [AddComponentMenu("LightsHearts/Entities/Weapon base behaviour")]
    public partial class WeaponBaseBehaviour : MonoBehaviour
    {
        #region Database fields
        public EntityBaseData entityData;

        public CombatBaseData combatData;
        #endregion

        [SerializeField]
        private Sprite _disabledIcon;
        public Sprite DisabledIcon { get { return _disabledIcon; } set { _disabledIcon = value; } }

        public Transform attackLocationT;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<HeroBaseBehaviour>().AddWeapon(this, false);

                Destroy(gameObject);
            }
        }
    }
}