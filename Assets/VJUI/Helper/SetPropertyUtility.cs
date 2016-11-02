using UnityEngine;

namespace VJUI
{
    internal static class SetPropertyUtility
    {
        public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
        {
            if (currentValue.Equals(newValue)) return false;
            currentValue = newValue;
            return true;
        }

        public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
        {
            if (currentValue == null && newValue == null ||
                currentValue != null && currentValue.Equals(newValue)) return false;
            currentValue = newValue;
            return true;
        }
    }
}
