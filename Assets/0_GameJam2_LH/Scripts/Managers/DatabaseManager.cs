using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Enigma.LightsHearts
{
    [AddComponentMenu("LightsHearts/Managers/Database manager")]
    public partial class DatabaseManager : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("LightsHeartsDatabase")]
        private LightsHeartsDatabase _sceneDatabase;
        public LightsHeartsDatabase SceneDatabase
        {
            get { return _sceneDatabase; }
        }

        private static LightsHeartsDatabase _database;

        public static LightsHeartsDatabase GetDatabase()
        {
            /*if (SceneDatabase != null)
                return SceneDatabase;*/

            if (_database != null)
                return _database;

//#if UNITY_EDITOR
            _database = Resources.Load("LightsHeartsDatabase", typeof(ScriptableObject)) as LightsHeartsDatabase;
//#endif
            //Debug.Log(_database != null ? "Database correctly loaded." : "Database error, database not found in Resources.");
            return _database;
        }

#if UNITY_EDITOR
        static void CreateDatabase()
        {
        }
#endif
    }
}
