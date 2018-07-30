// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEngine;

namespace Klak.VJUI
{
    [AddComponentMenu("UI/VJing/VJUI Configuration")]
    public sealed class Configuration : MonoBehaviour
    {
        #region Editable properties

        [SerializeField] float _knobSensitivity = 1;

        public float knobSensitivity {
            get { return _knobSensitivity; }
        }

        #endregion

        #region Static members

        // Search a given hierarchy for a configuration.
        static public Configuration Search(GameObject go)
        {
            // Return the default configuration if there is no
            // configuration instance in the parent hierarchy.
            var candid = go.GetComponentInParent<Configuration>();
            return candid != null ? candid : defaultInstance;
        }

        // Default configuration instance
        static Configuration s_defaultInstance;

        static Configuration defaultInstance {
            get {
                if (s_defaultInstance == null)
                {
                    // Create a hidden game object to store the configuration.
                    var go = new GameObject("VJUI Configuration");
                    go.hideFlags = HideFlags.HideAndDontSave;
                    s_defaultInstance = go.AddComponent<Configuration>();
                }
                return s_defaultInstance;
            }
        }

        #endregion
    }
}
