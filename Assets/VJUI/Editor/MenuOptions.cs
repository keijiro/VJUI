using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System;

namespace VJUI
{
    static class MenuOptions
    {
        static T LoadResource<T>(string filename) where T : UnityEngine.Object
        {
            var path = System.IO.Path.Combine("Assets/VJUI", filename);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        static void PlaceUIElementRoot(GameObject go, MenuCommand menuCommand)
        {
            // Retrieve an internal method "MenuOptions.PlaceUIElementRoot".
            var type = Type.GetType("UnityEditor.UI.MenuOptions,UnityEditor.UI");
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = type.GetMethod("PlaceUIElementRoot", flags);

            // PlaceUIElementRoot(go, menuCommand)
            method.Invoke(null, new System.Object[]{ go, menuCommand });
        }

        [MenuItem("GameObject/VJUI/Knob", false, 10)]
        static void AddKnob(MenuCommand menuCommand)
        {
            var go = DefaultControls.CreateKnob(
                LoadResource<Material>("Shader/Knob.mat"),
                LoadResource<Sprite>("Texture/Knob.png"),
                LoadResource<Font>("Font/DejaVuSans-ExtraLight.ttf")
            );
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/VJUI/Button", false)]
        static void AddButton(MenuCommand menuCommand)
        {
            var go = DefaultControls.CreateButton(
                LoadResource<Sprite>("Texture/Button.png"),
                LoadResource<Font>("Font/DejaVuSans-ExtraLight.ttf")
            );
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/VJUI/Toggle", false)]
        static void AddToggle(MenuCommand menuCommand)
        {
            var go = DefaultControls.CreateToggle(
                LoadResource<Sprite>("Texture/Toggle.png"),
                LoadResource<Sprite>("Texture/Toggle Fill.png"),
                LoadResource<Font>("Font/DejaVuSans-ExtraLight.ttf")
            );
            PlaceUIElementRoot(go, menuCommand);
        }
    }
}
