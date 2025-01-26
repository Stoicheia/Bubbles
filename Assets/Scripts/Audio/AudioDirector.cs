using System;
using System.Collections.Generic;
using Bubbles.GamePanels;
using Bubbles.Graphics.Transitions;
using Bubbles.InteractableInput;
using UnityEngine;
using InteractionManager = Bubbles.InteractableInput.InteractionManager;

namespace Audio
{
    public class AudioDirector : MonoBehaviour
    {
        public static AudioDirector Instance;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private DragInteractionHandler _dragger;
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip _mainMusic;
        [SerializeField] private AudioClip _hoverCharacter;
        [SerializeField] private AudioClip _invalidPlacement;
        [SerializeField] private AudioClip _placeOnBubble;
        [SerializeField] private AudioClip _placeOnTable;
        [SerializeField] private AudioClip _bubbleIn;
        [SerializeField] private AudioClip _bubbleOut;
        [SerializeField] private AudioClip _bubbleNew;

        [Header("Princess")] 
        [SerializeField] private ExpressionAudio _blushingP;
        [SerializeField] private ExpressionAudio _confidentP;
        [SerializeField] private ExpressionAudio _curiousP;
        [SerializeField] private ExpressionAudio _happyP;
        [SerializeField] private ExpressionAudio _sadP;
        [SerializeField] private ExpressionAudio _scaredP;
        [SerializeField] private ExpressionAudio _sleepingP;
        
        [Header("Knight")]
        [SerializeField] private ExpressionAudio _blushingK;
        [SerializeField] private ExpressionAudio _confusedK;
        [SerializeField] private ExpressionAudio _deadK;
        [SerializeField] private ExpressionAudio _happyK;
        [SerializeField] private ExpressionAudio _heroicK;
        [SerializeField] private ExpressionAudio _jealousK;
        [SerializeField] private ExpressionAudio _undeadK;
        [SerializeField] private ExpressionAudio _worriedK;
        
        [Header("Dragon")]
        [SerializeField] private ExpressionAudio _blushingD;
        [SerializeField] private ExpressionAudio _dieD;
        [SerializeField] private ExpressionAudio _happyD;
        [SerializeField] private ExpressionAudio _rampageD;
        [SerializeField] private ExpressionAudio _sadD;
        [SerializeField] private ExpressionAudio _sleepD;

        [Serializable]
        struct ExpressionAudio
        {
            public List<string> ExpressionNameContains;
            public AudioClip Clip;
        }
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            _musicSource.clip = _mainMusic;
            _musicSource.Play();
        }

        private void OnEnable()
        {
            _dragger.OnHoverPanel += HandleHoverPanel;
            _dragger.OnHoverPickup += HandleHoverPickup;
            _dragger.OnStartPickup += HandleStartPickup;
            InteractionManager.OnInteract += HandleInteract;
            GameSceneTransitionAnims.OnPop += HandlePop;
        }

        private void HandlePop(SlotID slotID, Panel panel)
        {
            _sfxSource.PlayOneShot(_bubbleNew);
            panel.GetAssociatedSpriteNames().ForEach(x => Debug.Log(x));
        }

        private void HandleInteract(SceneInteraction interaction, bool success)
        {
            if (!success)
            {
                _sfxSource.PlayOneShot(_invalidPlacement);
            }
            else
            {
                SlotID from = interaction.From;
                SlotID to = interaction.To;

                if (to == SlotID.Bubble1 || to == SlotID.Bubble2 || to == SlotID.Bubble3)
                {
                    _sfxSource.PlayOneShot(_placeOnBubble);
                }
                else
                {
                    _sfxSource.PlayOneShot(_placeOnTable);
                }
            }
        }

        private void HandleStartPickup(PanelPickup pickup, Vector2 _)
        {
            
        }

        private void HandleHoverPickup(PanelPickup panel)
        {
            switch (panel.ParentID)
            {
                case SlotID.Bubble1:
                case SlotID.Bubble2:
                case SlotID.Bubble3:
                    _sfxSource.PlayOneShot(_bubbleIn);
                    break;
                case SlotID.Char1:
                case SlotID.Char2:
                case SlotID.Char3:
                    _sfxSource.PlayOneShot(_hoverCharacter);
                    break;
                case SlotID.Environment:
                    _sfxSource.PlayOneShot(_bubbleIn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleHoverPanel(Panel panel)
        {
            
        }
    }
}