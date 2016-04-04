using UnityEngine;
using UnityEditor;

namespace Enigma.LightsHearts.Editors
{
    [CustomEditor(typeof(HeroBaseBehaviour), true)]
    public class HeroBaseBehaviourEditor : Editor
    {
        protected SerializedProperty baseData;
        protected SerializedProperty movementsSpeed;
        protected SerializedProperty attackType;
        protected SerializedProperty projectilePrefab;
        protected SerializedProperty baseDamage;
        protected SerializedProperty baseAttackSpeed;
        protected SerializedProperty baseAttackRange;
        protected SerializedProperty weaponID;

        protected HeroBaseBehaviour t;

        void OnEnable()
        {
            baseData = serializedObject.FindProperty("baseData");
            movementsSpeed = serializedObject.FindProperty("_movementSpeed");
            attackType = serializedObject.FindProperty("_attackType");
            projectilePrefab = serializedObject.FindProperty("_projectilePrefab");
            baseDamage = serializedObject.FindProperty("_baseDamage");
            baseAttackSpeed = serializedObject.FindProperty("_baseAttackSpeed");
            baseAttackRange = serializedObject.FindProperty("_baseAttackRange");
            weaponID = serializedObject.FindProperty("_weaponID");

            t = (HeroBaseBehaviour)target;
        }

        public override void OnInspectorGUI()
        {
            //var db = DatabaseManager.GetDatabase();
            DrawDefaultInspector();

        }
    }
}
