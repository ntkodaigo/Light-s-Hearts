using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    public class LightsHeartsEnums
    {
        public enum AttackType
        {
            Melee,
            Range
        }

        public enum EnemyType
        {
            Creep,
            Boss
        }

        public enum SpellType
        {
            /// <summary>
            /// Require a projectile prefab
            /// </summary>
            Projectile,
            /// <summary>
            /// Require a point to choice
            /// </summary>
            Point
        }

        public enum ProjectileOption
        {
            /// <summary>
            /// Need any objetive to choice
            /// </summary>
            Target,
            /// <summary>
            /// Automatic search target in range
            /// </summary>
            Auto
        }

        public enum PointOption
        {
            /// <summary>
            /// Teleports to point
            /// </summary>
            Blink,
            /// <summary>
            /// Require a burst prefab to launch to point
            /// </summary>
            Burst,
            /// <summary>
            /// Send caster to point
            /// </summary>
            Force
        }

        public enum CombatMode
        {
            Weaponless,
            TwoHandsWeapon
        }

        public enum GameState
        {
            GameOver,
            Playing,
            Pause
        }
    }
}
