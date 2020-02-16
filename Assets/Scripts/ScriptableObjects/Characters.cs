using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "new characters database", menuName = "Character/Characters Database", order = 2)]
public class CharactersDatabase : ScriptableObject
{
    public List<Character> characters = new List<Character>();

    public void UpdateDatabase()
    {
        characters.Clear();
        Character[] charactersList = Resources.FindObjectsOfTypeAll<Character>();
        characters.AddRange(charactersList.OrderBy(character => character.characterName));

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharactersDatabase))]
public class CharactersEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Update Character List"))
        {
            CharactersDatabase characters = (CharactersDatabase)target;
            characters.UpdateDatabase();
        }
    }
}
#endif