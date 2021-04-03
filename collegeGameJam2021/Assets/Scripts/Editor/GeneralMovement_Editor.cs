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

        SerializedProperty movementType = serializedObject.FindProperty("TypeOfMovement");
        movementType.enumValueIndex = (int)(MovementType)EditorGUILayout.EnumPopup("Type Of Movement", (MovementType)movementType.enumValueIndex);

        switch((MovementType) movementType.enumValueIndex)
        {
            case MovementType.RotateAroundPivot:
                SerializedProperty flipped = serializedObject.FindProperty("Flipped");
                flipped.boolValue = EditorGUILayout.Toggle("Is Flipped", flipped.boolValue);

                SerializedProperty pivot = serializedObject.FindProperty("Pivot");
                pivot.objectReferenceValue = EditorGUILayout.ObjectField("Pivot", pivot.objectReferenceValue, typeof(GameObject), true) as GameObject;

                SerializedProperty endAngle = serializedObject.FindProperty("EndAngle");
                endAngle.floatValue = EditorGUILayout.FloatField("End Angle", endAngle.floatValue);
                break;

            case MovementType.LookDirectional:
                SerializedProperty DistanceToMoveCollider = serializedObject.FindProperty("DistanceToMoveCollider");
                DistanceToMoveCollider.floatValue = EditorGUILayout.DelayedFloatField("Distance To Move Collider", DistanceToMoveCollider.floatValue);

                //SerializedProperty StartingAction = serializedObject.FindProperty("StartingAction");
                //StartingAction.enumValueIndex = (int)(Actions)EditorGUILayout.EnumPopup("Starting Action", (Actions)StartingAction.enumValueIndex);
                break;


        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
