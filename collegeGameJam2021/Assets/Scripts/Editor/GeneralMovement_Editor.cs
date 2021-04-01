using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(GeneralMovement))]
public class GeneralMovement_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty rotation = serializedObject.FindProperty("ShouldRotate");
        rotation.boolValue = EditorGUILayout.Toggle("Should Rotate", rotation.boolValue);
        
        if(rotation.boolValue)
        {
            SerializedProperty flipped = serializedObject.FindProperty("Flipped");
            flipped.boolValue = EditorGUILayout.Toggle("Is Flipped", flipped.boolValue);

            SerializedProperty pivot = serializedObject.FindProperty("Pivot");
            pivot.objectReferenceValue = EditorGUILayout.ObjectField("Pivot", pivot.objectReferenceValue, typeof(GameObject), true) as GameObject;

            SerializedProperty endAngle = serializedObject.FindProperty("EndAngle");
            endAngle.floatValue = EditorGUILayout.FloatField("End Angle", endAngle.floatValue);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
