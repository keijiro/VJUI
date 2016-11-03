using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

namespace VJUI
{
    public static class DefaultControls
    {
        const float kWidth = 80;

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

        static void SetDefaultColorTransitionValues(Selectable selectable)
        {
            var colors = selectable.colors;
            colors.normalColor      = new Color32(60, 60, 60, 255);
            colors.highlightedColor = new Color32(70, 70, 70, 255);
            colors.pressedColor     = new Color32(70, 70, 70, 255);
            colors.disabledColor    = new Color32(20, 20, 20, 128);
            colors.fadeDuration     = 0.01f;
            selectable.colors = colors;
        }

        static void FitToParent(GameObject go, Vector2 offset)
        {
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            rt.offsetMin = new Vector2(offset.x, 0);
            rt.offsetMax = new Vector2(0, offset.y);
        }

        // Actual controls

        public static GameObject CreateKnob(Material material, Sprite sprite, Font font)
        {
            var root = CreateUIElementRoot("Knob", Vector2.one * kWidth);
            var graphic = CreateUIObject("Graphic", root);
            var label = CreateUIObject("Label", root);

            FitToParent(graphic, Vector2.zero);
            FitToParent(label, new Vector2(4, 15));

            var image = graphic.AddComponent<Image>();
            image.material = material;
            image.sprite = sprite;
            image.color = Color.white;

            var text = label.AddComponent<Text>();
            text.text = "Knob";
            text.alignment = TextAnchor.UpperLeft;
            text.font = font;

            var knob = root.AddComponent<Knob>();
            SetDefaultColorTransitionValues(knob);
            knob.targetGraphic = image;
            knob.graphic = image;

            return root;
        }
    }
}
