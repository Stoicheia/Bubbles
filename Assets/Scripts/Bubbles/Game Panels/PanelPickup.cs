using System;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels
{
    public class PanelPickup : SerializedMonoBehaviour, IMouseInteractor
    {
        public static event Action<PanelPickup, Vector2> OnPickup;
        public static event Action<PanelPickup> OnRelease;
        public event Action OnHover;
        public event Action OnClick;
        [field: SerializeField][field: ReadOnly] public Panel ParentPanel { get; set; }
        public SlotID ParentID => ParentPanel.ID;
        [SerializeField] private PickupGraphics _graphics;
        public Sprite SpriteInTransit => _overrideSpriteInTransit == null ? _graphics.Sprite : _overrideSpriteInTransit;
        [SerializeField] private Sprite _overrideSpriteInTransit;
        [SerializeField] private float _transitScale = 0.8f;
        private PickupState _state;
        private Vector2 _dragStartPos;
        private Canvas _myCanvas;
        public RectTransform Rect { get; private set; }

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _myCanvas = FindAnyObjectByType<Canvas>();
            _state = PickupState.AtRest;
        }

        private void Update()
        {
            Vector2 mousePos = Input.mousePosition;
            if (_state == PickupState.Click)
            {
                if (_dragStartPos != mousePos)
                {
                    BeginDrag();
                }
            }

            if (_state == PickupState.Dragging)
            {
                OnDrag();
                if (Input.GetMouseButtonUp(0))
                {
                    EndDrag();
                }
            }
        }

        public void OnDrag()
        {
            Debug.Log($"Dragging pickup from slot: {ParentID.ToString()}");
        }

        public void BeginDrag()
        {
            _state = PickupState.Dragging;
            _graphics.SetGraphics(_state);
            OnPickup?.Invoke(this, _dragStartPos);
        }

        public void EndDrag()
        {
            _state = PickupState.AtRest;
            _graphics.SetGraphics(_state);
            OnRelease?.Invoke(this);
        }

        public void Hover()
        {
            if (_state == PickupState.Dragging) return;
            _state = PickupState.Hover;
            _graphics.SetGraphics(_state);
            OnHover?.Invoke();
        }

        public void Click()
        {
            _state = PickupState.Click;
            _graphics.SetGraphics(_state);
            Vector3 myWorldPosition = transform.position;
            Vector2 myPixelPosition = RectTransformUtility.WorldToScreenPoint(_myCanvas.worldCamera, myWorldPosition);
            _dragStartPos = (Vector2)Input.mousePosition - myPixelPosition;
            OnClick?.Invoke();
        }

        public void Unhover()
        {
            if (_state == PickupState.Dragging) return;
            _state = PickupState.AtRest;
            _graphics.SetGraphics(_state);
        }

        public bool IsActive()
        {
            return _state != PickupState.Dragging;
        }
    }
}