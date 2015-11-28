using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GrenadeTosser))]
[CanEditMultipleObjects]
public class GrenadeTosser_Editor : Editor {

	public SerializedProperty
		k_Bomb,
		blackDeath;

	void OnEnable(){
		k_Bomb = serializedObject.FindProperty("k_Bomb");
		blackDeath = serializedObject.FindProperty("blackDeath");
	}

	public override void OnInspectorGUI (){
		serializedObject.Update();
		EditorGUILayout.PropertyField(k_Bomb);
		EditorGUILayout.PropertyField(blackDeath);
		serializedObject.ApplyModifiedProperties();
	}
}
