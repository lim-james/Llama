using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "new fruit database", menuName = "Fruit/Fruit Database", order = 1)]
public class FruitDatabase : ScriptableObject
{
    public List<FruitInfo> fruits = new List<FruitInfo>();

    public void UpdateDatabase()
    {
        fruits.Clear();
        FruitInfo[] fruitInfos = Resources.FindObjectsOfTypeAll<FruitInfo>();
        fruits.AddRange(fruitInfos);

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(FruitDatabase))]
public class FruitDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Update Fruit Database"))
        {
            FruitDatabase fruitDatabase = (FruitDatabase)target;
            fruitDatabase.UpdateDatabase();
        }

    }
}
#endif