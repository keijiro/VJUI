using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

namespace VJUI
{
    public static class DefaultControls
    {
        const float kWidth = 160;

        // Retrieve and invoke a private method "DefaultControls.CreateUIElementRoot".
        static GameObject CreateUIElementRoot(string name, Vector2 size)
        {
            var type = Type.GetType("UnityEngine.UI.DefaultControls,UnityEngine.UI");
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = type.GetMethod("CreateUIElementRoot", flags);
            return (GameObject)method.Invoke(null, new System.Object[]{ name, size });
        }

        // Retrieve and invoke a private method "DefaultControls.CreateUIObject".
        static GameObject CreateUIObject(string name, GameObject parent)
        {
            var type = Type.GetType("UnityEngine.UI.DefaultControls,UnityEngine.UI");
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = type.GetMethod("CreateUIObject", flags);
            return (GameObject)method.Invoke(null, new System.Object[]{ name, parent });
        }

        // Retrieve and invoke a private method "DefaultControls.SetDefaultColorTransitionValues".
        static void SetDefaultColorTransitionValues(Selectable slider)
        {
            var type = Type.GetType("UnityEngine.UI.DefaultControls,UnityEngine.UI");
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = type.GetMethod("SetDefaultColorTransitionValues", flags);
            method.Invoke(null, new System.Object[]{ slider });
        }

        // Actual controls

        public static GameObject CreateKnob(Material material, Sprite sprite)
        {
            GameObject root = CreateUIElementRoot("Knob", Vector2.one * kWidth);
            GameObject graphic = CreateUIObject("Graphic", root);

            var image = graphic.AddComponent<Image>();
            image.material = material;
            image.sprite = sprite;
            image.color = Color.white;

            var knob = root.AddComponent<Knob>();
            knob.graphic = image;
            SetDefaultColorTransitionValues(knob);

            return root;
        }
    }
}
