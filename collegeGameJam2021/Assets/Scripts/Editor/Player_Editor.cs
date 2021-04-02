using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(Player))]
public class Player_Editor : Editor
{
    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        SerializedProperty speed = serializedObject.FindProperty("speed");
        speed.floatValue = EditorGUILayout.FloatField("Movement/Rotational Speed", speed.floatValue);

        SerializedProperty actions = serializedObject.FindProperty("directionsToMove");
        EditorGUILayout.PropertyField(actions, true);


        SerializedProperty completionType = serializedObject.FindProperty("CompleteType");
        completionType.enumValueIndex = (int)(CompletionType)EditorGUILayout.EnumPopup("Completion Type", (CompletionType)completionType.enumValueIndex);

        switch ((CompletionType)completionType.enumValueIndex)
        {
            case CompletionType.Collection:
            case CompletionType.CollectThenMoveToLocation:
                SerializedProperty NumItems = serializedObject.FindProperty("NumItemsToCollect");
                NumItems.intValue = EditorGUILayout.DelayedIntField("Number Of Items To Collected", NumItems.intValue);
                break;
            case CompletionType.MoveTargetToLocation:
                SerializedProperty minion = serializedObject.FindProperty("PlayerMinion");
                minion.objectReferenceValue = EditorGUILayout.ObjectField("Moving Minion", minion.objectReferenceValue, typeof(GameObject), true) as GameObject;
                break;
            case CompletionType.HoldForTime:
            case CompletionType.HoldToggle:
                SerializedProperty timeToHold = serializedObject.FindProperty("TimeToHold");
                timeToHold.floatValue = EditorGUILayout.FloatField("Time To Hold", timeToHold.floatValue);
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }
}

#endif
