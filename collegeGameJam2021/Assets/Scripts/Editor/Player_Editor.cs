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

        EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
        SerializedProperty speed = serializedObject.FindProperty("speed");
        speed.floatValue = EditorGUILayout.FloatField("Movement/Rotational Speed", speed.floatValue);

        SerializedProperty actions = serializedObject.FindProperty("directionsToMove");
        EditorGUILayout.PropertyField(actions, true);


        EditorGUILayout.LabelField("Completion", EditorStyles.boldLabel);
        SerializedProperty completionType = serializedObject.FindProperty("CompleteType");
        completionType.enumValueIndex = (int)(CompletionType)EditorGUILayout.EnumPopup("Completion Type", (CompletionType)completionType.enumValueIndex);

        switch ((CompletionType)completionType.enumValueIndex)
        {
            case CompletionType.Collection:
            case CompletionType.CollectThenMoveToLocation:
                Collection();
                break;
            case CompletionType.MoveTargetToLocation:
                MovingMinion();
                break;
            case CompletionType.HoldForTime:
            case CompletionType.HoldToggle:
                HoldForTime();
                break;
            case CompletionType.MoveToLocationAndHoldKey:
                HoldForTime();
                EventTrigger();
                break;
            case CompletionType.PressKeyMultipleTimes:
                PressNumTimes();
                EventTrigger();
                break;
            case CompletionType.CollectToLocationAndHold:
                Collection();
                HoldForTime();
                EventTrigger();
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }

    void Collection()
    {
        SerializedProperty NumItems = serializedObject.FindProperty("NumItemsToCollect");
        NumItems.intValue = EditorGUILayout.DelayedIntField("Number Of Items To Collect", NumItems.intValue);
    }

    void MovingMinion()
    {
        SerializedProperty minion = serializedObject.FindProperty("PlayerMinion");
        minion.objectReferenceValue = EditorGUILayout.ObjectField("Moving Minion", minion.objectReferenceValue, typeof(GameObject), true) as GameObject;
    }

    void HoldForTime()
    {
        SerializedProperty timeToHold = serializedObject.FindProperty("TimeToHold");
        timeToHold.floatValue = EditorGUILayout.FloatField("Time To Hold", timeToHold.floatValue);

        SerializedProperty Continuous = serializedObject.FindProperty("ContinuousHold");
        Continuous.boolValue = EditorGUILayout.Toggle("Continuous Hold", Continuous.boolValue);
    }

    void EventTrigger()
    {
        SerializedProperty trigger = serializedObject.FindProperty("ShouldTriggerEvent");
        trigger.boolValue = EditorGUILayout.Toggle("Should Trigger Event", trigger.boolValue);
        if (trigger.boolValue)
        {
            SerializedProperty eventSlots = serializedObject.FindProperty("eventSlots");
            EditorGUILayout.PropertyField(eventSlots, true);
        }
    }

    void PressNumTimes()
    {
        SerializedProperty numTimes = serializedObject.FindProperty("TimesToPress");
        numTimes.intValue = EditorGUILayout.DelayedIntField("Times To Press", numTimes.intValue);
    }
}

#endif
