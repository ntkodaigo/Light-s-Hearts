using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    [System.Serializable]
    public partial struct CombatBaseData
    {
        #region Database fields
        [SerializeField]
        private LightsHeartsEnums.AttackType _attackType;
        public LightsHeartsEnums.AttackType AttackType { get { return _attackType; } set { _attackType = value; } }

        [SerializeField]
        private GameObject _projectilePrefab;
        public GameObject ProjectilePrefab { get { return _projectilePrefab; } set { _projectilePrefab = value; } }

        [SerializeField]
        private int _baseDamageMin;
        public int BaseDamageMin { get { return _baseDamageMin; } set { _baseDamageMin = value; } }

        [SerializeField]
        private int _baseDamageMax;
        public int BaseDamageMax { get { return _baseDamageMax; } set { _baseDamageMax = value; } }

        [SerializeField]
        private float _baseAttackSpeed;
        public float BaseAttackSpeed { get { return _baseAttackSpeed; } set { _baseAttackSpeed = value; } }

        [SerializeField]
        private float _baseAttackRange;
        public float BaseAttackRange { get { return _baseAttackRange; } set { _baseAttackRange = value; } }
        #endregion

        public int Damage
        {
            get { return Random.Range(BaseDamageMin, BaseDamageMax + 1); }
        }
    }
}
