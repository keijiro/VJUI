// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

namespace Klak.VJUI
{
    static class DefaultControls
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

        static void SetDefaultColorTransitionValues(Selectable selectable, bool whiteOnPress)
        {
            var colors = selectable.colors;
            colors.normalColor = new Color32(72, 72, 72, 255);
            colors.highlightedColor = new Color32(72, 72, 72, 255);
            if (whiteOnPress)
                colors.pressedColor = Color.white;
            else
                colors.pressedColor = new Color32(72, 72, 72, 255);
            colors.disabledColor = new Color32(20, 20, 20, 128);
            colors.fadeDuration = 0.03f;
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

        // Knob
        public static GameObject CreateKnob(Material material, Sprite sprite, Font font)
        {
            // UI hierarchy
            var root = CreateUIElementRoot("Knob", Vector2.one * kWidth);
            var graphic = CreateUIObject("Graphic", root);
            var label = CreateUIObject("Label", root);

            // Stretch settings
            FitToParent(graphic, Vector2.zero);
            FitToParent(label, new Vector2(4, 15));

            // Graphic
            var image = graphic.AddComponent<Image>();
            image.material = material;
            image.sprite = sprite;
            image.color = Color.white;

            // Label
            var text = label.AddComponent<Text>();
            text.text = "Knob";
            text.alignment = TextAnchor.UpperLeft;
            text.font = font;

            // Knob
            var knob = root.AddComponent<Knob>();
            SetDefaultColorTransitionValues(knob, false);
            knob.targetGraphic = image;
            knob.graphic = image;

            return root;
        }

        // Button
        public static GameObject CreateButton(Sprite sprite, Font font)
        {
            // UI hierarchy
            var root = CreateUIElementRoot("Button", Vector2.one * kWidth);
            var label = CreateUIObject("Label", root);

            // Stretch settings
            FitToParent(label, new Vector2(4, 15));

            // Graphic
            var image = root.AddComponent<Image>();
            image.sprite = sprite;
            image.color = Color.white;

            // Label
            var text = label.AddComponent<Text>();
            text.text = "Button";
            text.alignment = TextAnchor.UpperLeft;
            text.font = font;

            // Button
            var button = root.AddComponent<Button>();
            SetDefaultColorTransitionValues(button, true);

            return root;
        }

        // Toggle
        public static GameObject CreateToggle(Sprite bgSprite, Sprite fillSprite, Font font)
        {
            // UI hierarchy
            var root = CreateUIElementRoot("Toggle", Vector2.one * kWidth);
            var background = CreateUIObject("Background", root);
            var checkmark = CreateUIObject("Checkmark", background);
            var label = CreateUIObject("Label", root);

            // Stretch settings
            FitToParent(background, Vector2.zero);
            FitToParent(checkmark, Vector2.zero);
            FitToParent(label, new Vector2(4, 15));

            // Background image
            var bgImage = background.AddComponent<Image>();
            bgImage.sprite = bgSprite;
            bgImage.color = Color.white;

            // Checkmark image
            var ckImage = checkmark.AddComponent<Image>();
            ckImage.sprite = fillSprite;
            ckImage.color = new Color32(240, 240, 240, 255);

            // Label
            var text = label.AddComponent<Text>();
            text.text = "Toggle";
            text.alignment = TextAnchor.UpperLeft;
            text.font = font;

            // Toggle
            var toggle = root.AddComponent<Toggle>();
            SetDefaultColorTransitionValues(toggle, true);
            toggle.targetGraphic = bgImage;
            toggle.graphic = ckImage;

            return root;
        }
    }
}
