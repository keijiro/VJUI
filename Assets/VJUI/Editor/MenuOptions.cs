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
            var path = System.IO.Path.Combine("Assets/VJUI/Resources/", filename);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        [MenuItem("GameObject/VJUI/Knob", false, 10)]
        static void AddKnob(MenuCommand menuCommand)
        {
            var go = DefaultControls.CreateKnob(
                LoadResource<Material>("Knob.mat"),
                LoadResource<Sprite>("Knob.png"),
                LoadResource<Font>("DejaVuSans-ExtraLight.ttf")
            );

            // Retrieve an internal method "MenuOptions.PlaceUIElementRoot".
            var type = Type.GetType("UnityEditor.UI.MenuOptions,UnityEditor.UI");
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var method = type.GetMethod("PlaceUIElementRoot", flags);

            // PlaceUIElementRoot(go, menuCommand)
            method.Invoke(null, new System.Object[]{ go, menuCommand });
        }
    }
}
