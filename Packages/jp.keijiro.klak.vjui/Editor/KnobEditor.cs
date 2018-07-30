// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Klak.VJUI
{
    [CustomEditor(typeof(Knob), true)]
    [CanEditMultipleObjects]
    sealed class KnobEditor : SelectableEditor
    {
        SerializedProperty _minValue;
        SerializedProperty _maxValue;
        SerializedProperty _value;
        SerializedProperty _graphic;
        SerializedProperty _onValueChanged;

        protected override void OnEnable()
        {
            base.OnEnable();
            _minValue = serializedObject.FindProperty("_minValue");
            _maxValue = serializedObject.FindProperty("_maxValue");
            _value = serializedObject.FindProperty("_value");
            _graphic = serializedObject.FindProperty("_graphic");
            _onValueChanged = serializedObject.FindProperty("_onValueChanged");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_minValue);
            EditorGUILayout.PropertyField(_maxValue);
            EditorGUILayout.Slider(_value, _minValue.floatValue, _maxValue.floatValue);
            EditorGUILayout.PropertyField(_graphic);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_onValueChanged);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
