// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Klak.VJUI
{
    [AddComponentMenu("UI/VJing/VJUI Toggle")]
    public sealed class Toggle : Selectable, IPointerClickHandler, ICanvasElement
    {
        #region Editable properties

        [SerializeField] bool _isOn;

        public bool isOn
        {
            get { return _isOn; }
            set { Set(value); }
        }

        [SerializeField] Graphic _graphic;

        public Graphic graphic
        {
            get { return _graphic; }
            set { _graphic = value; UpdateVisuals(); }
        }

        [System.Serializable] public class ToggleEvent : UnityEvent<bool> {}

        [SerializeField] ToggleEvent _onValueChanged = new ToggleEvent();

        public ToggleEvent onValueChanged {
            get { return _onValueChanged; }
            set { _onValueChanged = value; }
        }

        #endregion

        #region Private methods

        void Set(bool value, bool sendCallback = true)
        {
            if (_isOn == value) return;

            _isOn = value;
            UpdateVisuals();

            if (sendCallback) _onValueChanged.Invoke(_isOn);
        }

        void InternalToggle()
        {
            if (!IsActive() || !IsInteractable()) return;
            isOn = !_isOn;
        }

        void UpdateVisuals()
        {
            if (_graphic == null) return;

            _graphic.canvasRenderer.SetAlpha(_isOn ? 1 : 0);
        }

        #endregion

        #region Selectable functions

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateVisuals();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (IsActive()) UpdateVisuals();

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }
#endif

        #endregion

        #region IPointerClickHandler implementation

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            InternalToggle();
        }

        #endregion

        #region ICanvasElement implementation

        public void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onValueChanged.Invoke(_isOn);
#endif
        }

        public void LayoutComplete() {}
        public void GraphicUpdateComplete() {}

        #endregion
    }
}
