// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace VJUI
{
    [CustomEditor(typeof(Button), true)]
    [CanEditMultipleObjects]
    public class ButtonEditor : SelectableEditor
    {
        SerializedProperty _onButtonDown;
        SerializedProperty _onButtonUp;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onButtonDown = serializedObject.FindProperty("_onButtonDown");
            _onButtonUp = serializedObject.FindProperty("_onButtonUp");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_onButtonDown);
            EditorGUILayout.PropertyField(_onButtonUp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
