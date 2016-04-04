using UnityEngine;
using System.Collections;
using System;

namespace Enigma.LightsHearts
{
    [AddComponentMenu("LightsHearts/Entities/Spell base behaviour")]
    public partial class SpellBaseBehaviour : MonoBehaviour
    {
        #region Database fields
        public EntityBaseData entityData;

        [SerializeField]
        private LightsHeartsEnums.SpellType _spellType;
        public LightsHeartsEnums.SpellType SpellType { get { return _spellType; } set { _spellType = value; } }

        [SerializeField]
        private LightsHeartsEnums.ProjectileOption _projectileOption;
        public LightsHeartsEnums.ProjectileOption ProjectileOption { get { return _projectileOption; } set { _projectileOption = value; } }

        [SerializeField]
        private LightsHeartsEnums.PointOption _pointOption;
        public LightsHeartsEnums.PointOption PointOption { get { return _pointOption; } set { _pointOption = value; } }

        [SerializeField]
        private int _damage;
        public int Damage { get { return _damage; } set { _damage = value; } }

        [SerializeField]
        private float _castRange;
        public float CastRange { get { return _castRange; } set { _castRange = value; } }

        [SerializeField]
        private float _coolDown;
        public float CoolDown { get { return _coolDown; } set { _coolDown = value; } }
        #endregion

        [NonSerialized]
        private float _lastUsageTime;
        public float LastUsageTime { get { return _lastUsageTime; } set { _lastUsageTime = value; }}

        public bool IsInCooldown
        {
            get
            {
                return Time.timeSinceLevelLoad - LastUsageTime < CoolDown && LastUsageTime > 0f;
            }
        }

        public float CooldownFactor
        {
            get
            {
                return (Time.timeSinceLevelLoad - LastUsageTime) / CoolDown;
            }
        }
    }
}
