﻿using System;
using System.Collections.Generic;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels
{
    [RequireComponent(typeof(Image))]
    public class Panel : MonoBehaviour, IMouseInteractor
    {
        [field: SerializeField][field: HideInInspector] public SlotID ID { get; set; }
        [SerializeField][HideInInspector] private bool _isLocked;
        
        [Header("Dependencies")]
        [SerializeField] private RectTransform _background;
        [SerializeField] private Image _toHighlight;
        [SerializeField] private PanelPickup _pickup;
        [SerializeField] private List<PanelPickup> _additionalPickups;

        [Header("Graphics Settings")] 
        [SerializeField] private PanelHighlight _panelHighlight;
        [SerializeField] private GraphicsSettings _graphicsSettings;

        private void Start()
        {
            GetComponent<Image>().color = Color.clear;
            _pickup.ParentPanel = this;
            _additionalPickups.ForEach(x => x.ParentPanel = this);
            PanelField parentField = GetComponentInParent<PanelField>();
            if (parentField != null)
            {
                ID = parentField.ID;
            }
        }

        public bool IsActive()
        {
            return !_isLocked;
        }

        public void SetHighlight(HighlightState highlight)
        {
            _panelHighlight.SetHighlightState(highlight);
        }
    }
}