// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Klak.VJUI
{
    [CustomEditor(typeof(Toggle), true)]
    [CanEditMultipleObjects]
    sealed class ToggleEditor : SelectableEditor
    {
        SerializedProperty _isOn;
        SerializedProperty _graphic;
        SerializedProperty _onValueChanged;

        protected override void OnEnable()
        {
            base.OnEnable();
            _isOn = serializedObject.FindProperty("_isOn");
            _graphic = serializedObject.FindProperty("_graphic");
            _onValueChanged = serializedObject.FindProperty("_onValueChanged");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_isOn);
            EditorGUILayout.PropertyField(_graphic);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_onValueChanged);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
