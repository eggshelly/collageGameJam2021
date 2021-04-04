using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(UpdateSprite))]
public class UpdateSprite_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty UpdateType = serializedObject.FindProperty("UpdateType");
        UpdateType.enumValueIndex = (int)(UpdateSpriteType)EditorGUILayout.EnumPopup("Type Of Sprite Change", (UpdateSpriteType)UpdateType.enumValueIndex);

        if ((UpdateSpriteType)UpdateType.enumValueIndex != UpdateSpriteType.None)
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spritesToUse"), true);
        switch ((UpdateSpriteType)UpdateType.enumValueIndex)
        {
            case UpdateSpriteType.None:
                break;
            case UpdateSpriteType.OnEvent:
                break;
            default:
                switch ((UpdateSpriteType)UpdateType.enumValueIndex)
                {
                    case UpdateSpriteType.Cycle:
                        SerializedProperty actionToCycle = serializedObject.FindProperty("ActionToCycle");
                        actionToCycle.enumValueIndex = (int)(Actions)EditorGUILayout.EnumPopup("Key To Cycle", (Actions)actionToCycle.enumValueIndex);
                        break;
                    case UpdateSpriteType.UpdateOnCertainKeys:
                    case UpdateSpriteType.UpdateOnCertainKeysWithIdle:
                        SerializedProperty actionsToMap = serializedObject.FindProperty("actionsToMap");
                        EditorGUILayout.PropertyField(actionsToMap, true);
                        break;
                    case UpdateSpriteType.Toggle:
                        SerializedProperty actionToToggle = serializedObject.FindProperty("ActionToToggle");
                        actionToToggle.enumValueIndex = (int)(Actions)EditorGUILayout.EnumPopup("Key To Toggle", (Actions)actionToToggle.enumValueIndex);
                        break;
                }

                SerializedProperty changeResult = serializedObject.FindProperty("ShouldChangeResult");
                changeResult.boolValue = EditorGUILayout.Toggle("Should Change Result", changeResult.boolValue);

                if (changeResult.boolValue)
                {
                    SerializedProperty newResult = serializedObject.FindProperty("ChangedResult");
                    newResult.enumValueIndex = (int)(ResultType)EditorGUILayout.EnumPopup("Changed Result", (ResultType)newResult.enumValueIndex);
                }
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}

#endif