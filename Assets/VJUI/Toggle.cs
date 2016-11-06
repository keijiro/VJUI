//
// VJUI - Custom UI controls for VJing
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace VJUI
{
    [AddComponentMenu("UI/VJUI/VJUI Toggle")]
    public class Toggle : Selectable, IPointerClickHandler, ICanvasElement
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

        [Serializable] public class ToggleEvent : UnityEvent<bool> {} 

        [SerializeField] ToggleEvent _onValueChanged = new ToggleEvent();

        public ToggleEvent onValueChanged {
            get { return _onValueChanged; }
            set { _onValueChanged = value; }
        }

        #endregion

        #region Private methods

        protected Toggle() {}

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

            var prefabType = UnityEditor.PrefabUtility.GetPrefabType(this);
            if (prefabType != UnityEditor.PrefabType.Prefab && !Application.isPlaying)
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
