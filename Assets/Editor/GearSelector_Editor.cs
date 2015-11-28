using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GearSelector))]
[CanEditMultipleObjects]
public class GearSelector_Editor : Editor {

    public SerializedProperty
        UIMovementAnimator;

    void OnEnable()
    {
        UIMovementAnimator = serializedObject.FindProperty("UIMovementAnimator");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(UIMovementAnimator);
        serializedObject.ApplyModifiedProperties();
    }
}
