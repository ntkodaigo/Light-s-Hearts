using UnityEngine;
//using UnityEditor;
using System.IO;
using System;

public static class CustomAssetUtility
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        /*var asset = ScriptableObject.CreateInstance<T>();
        var path = AssetDatabase.GetAssetPath(Selection.activeObject);

        if (string.IsNullOrEmpty(path))
            path = "Assets";
        else if (!string.IsNullOrEmpty(Path.GetExtension(path)))
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

        var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}_{2:yyyyMMdd-HHmmss}.asset", path, typeof(T), DateTime.UtcNow));

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;*/
    }
}
