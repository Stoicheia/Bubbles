using System;
using Bubbles.GamePanels;
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
            
        }

        private void HandleHoverPanel(Panel panel)
        {
            switch (panel.ID)
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
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}