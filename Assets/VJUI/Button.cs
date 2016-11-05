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
    [AddComponentMenu("UI/VJUI/VJUI Button")]
    [RequireComponent(typeof(RectTransform))]
    public class Button : Selectable, IPointerDownHandler, IPointerUpHandler
    {
        #region Editable properties

        [Serializable] public class ButtonEvent : UnityEvent {} 

        [SerializeField] ButtonEvent _onButtonDown = new ButtonEvent();
        [SerializeField] ButtonEvent _onButtonUp = new ButtonEvent();

        public ButtonEvent onButtonDown {
            get { return _onButtonDown; }
            set { _onButtonDown = value; }
        }

        public ButtonEvent onButtonUp {
            get { return _onButtonUp; }
            set { _onButtonUp = value; }
        }

        #endregion

        #region Private methods

        protected Button() {}

        #endregion

        #region Selectable functions

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!IsActive() || !IsInteractable()) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            base.OnPointerDown(eventData);
            _onButtonDown.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!IsActive() || !IsInteractable()) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            base.OnPointerUp(eventData);
            _onButtonUp.Invoke();
        }

        #endregion
    }
}
