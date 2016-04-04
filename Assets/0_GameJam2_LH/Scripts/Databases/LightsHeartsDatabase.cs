using UnityEngine;
using System.Collections.Generic;

namespace Enigma.LightsHearts
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LightsHeartsDatabase.asset", menuName = "LightsHearts/MainDatabase")]
    public partial class LightsHeartsDatabase : ScriptableObject
    {
        public HeroBaseBehaviour hero;
        public List<WeaponBaseBehaviour> weapons = new List<WeaponBaseBehaviour>();
        public List<EnemyBaseBehaviour> enemies = new List<EnemyBaseBehaviour>();
        public List<SpellBaseBehaviour> spells = new List<SpellBaseBehaviour>();
    }
}