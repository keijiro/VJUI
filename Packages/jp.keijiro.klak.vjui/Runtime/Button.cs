// VJUI - Custom UI controls for VJing
// https://github.com/keijiro/VJUI

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Klak.VJUI
{
    [AddComponentMenu("UI/VJing/VJUI Button")]
    [RequireComponent(typeof(RectTransform))]
    public sealed class Button : Selectable, IPointerDownHandler, IPointerUpHandler
    {
        #region Editable properties

        [System.Serializable] public class ButtonEvent : UnityEvent {} 

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
