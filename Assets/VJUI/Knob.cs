using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace VJUI
{
    [RequireComponent(typeof(RectTransform))]
    public class Knob : Selectable, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
    {
        #region Editable properties

        [SerializeField] RectTransform _handleRect;

        public RectTransform handleRect
        {
            get { return _handleRect; }
            set {
                if (SetPropertyUtility.SetClass(ref _handleRect, value))
                {
                    UpdateCachedReferences();
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] float _minValue = 0;

        public float minValue
        {
            get { return _minValue; }
            set {
                if (SetPropertyUtility.SetStruct(ref _minValue, value))
                {
                    Set(_value);
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] float _maxValue = 1;

        public float maxValue
        {
            get { return _maxValue; }
            set {
                if (SetPropertyUtility.SetStruct(ref _maxValue, value))
                {
                    Set(_value);
                    UpdateVisuals();
                }
            }
        }

        [SerializeField] float _value;

        public float value
        {
            get { return _value; }
            set { Set(value); }
        }

        public float normalizedValue
        {
            get
            {
                if (Mathf.Approximately(minValue, maxValue)) return 0;
                return Mathf.InverseLerp(minValue, maxValue, value);
            }
            set
            {
                this.value = Mathf.Lerp(minValue, maxValue, value);
            }
        }

        [Serializable] public class KnobEvent : UnityEvent<float> {} 

        [SerializeField] KnobEvent _onValueChanged = new KnobEvent();
        public KnobEvent onValueChanged {
            get { return _onValueChanged; }
            set { _onValueChanged = value; }
        }

        #endregion

        #region Private methods

        // Reference cache
        Transform _handleTransform;
        RectTransform _handleContainer;

        DrivenRectTransformTracker _tracker;

        Vector2 _dragPoint;
        float _dragOffset;

        protected Knob() {}

        void Set(float input, bool sendCallback = true)
        {
            var newValue = Mathf.Clamp(input, minValue, maxValue);
            if (_value == newValue) return;

            _value = newValue;
            UpdateVisuals();

            if (sendCallback) _onValueChanged.Invoke(newValue);
        }

        void UpdateCachedReferences()
        {
            if (_handleRect != null)
            {
                _handleTransform = _handleRect.transform;
                _handleContainer = _handleTransform.parent.GetComponent<RectTransform>();
            }
            else
            {
                _handleTransform = null;
                _handleContainer = null;
            }
        }

        bool MayDrag(PointerEventData eventData)
        {
            return IsActive() && IsInteractable() && _handleRect != null &&
                eventData.button == PointerEventData.InputButton.Left;
        }

        void UpdateVisuals()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) UpdateCachedReferences();
#endif
            _tracker.Clear();

            if (_handleRect != null)
            {
                var angle = 179.9f - normalizedValue * 359.9f;
                _tracker.Add(this, _handleRect, DrivenTransformProperties.Rotation);
                _handleRect.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        void UpdateDrag(PointerEventData eventData, Camera cam)
        {
            if (_handleContainer == null) return;

            Vector2 input;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_handleContainer, eventData.position, cam, out input)) return;

            input = input - _dragPoint;

            normalizedValue = _dragOffset + (input.x + input.y) * 0.004f;
        }

        #endregion

        #region Selectable functions

        protected override void OnEnable()
        {
            base.OnEnable();
            UpdateCachedReferences();
            Set(_value, false);
            UpdateVisuals();
        }

        protected override void OnDisable()
        {
            _tracker.Clear();
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            _value = Mathf.Clamp(_value, minValue, maxValue);
            UpdateVisuals();
            onValueChanged.Invoke(_value);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!MayDrag(eventData)) return;

            base.OnPointerDown(eventData);

            _dragPoint = Vector2.zero;
            _dragOffset = normalizedValue;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_handleContainer, eventData.position, eventData.pressEventCamera, out _dragPoint);
        }

        #endregion

        #region IInitializePotentialDragHandler implementation

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
        }

        #endregion

        #region IDragHandler implementation

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!MayDrag(eventData)) return;
            UpdateDrag(eventData, eventData.pressEventCamera);
        }

        #endregion

        #region ICanvasElement implementation

        public void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onValueChanged.Invoke(value);
#endif
        }

        public void LayoutComplete() {}
        public void GraphicUpdateComplete() {}

        #endregion
    }
}
