using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

namespace Bubbles.GamePanels
{
    public class GameScene : SerializedMonoBehaviour
    {
        public Dictionary<SlotID, Panel> PanelPrefabs => _panels;
        
        [OdinSerialize] private Dictionary<SlotID, Panel> _panels;
        [OdinSerialize] private List<SceneTransition> _transitions;
        
        private Dictionary<SceneInteraction, GameScene> _sceneByInteraction;

        private void Redict()
        {
            _sceneByInteraction = _transitions.ToDictionary(x => x.Interaction, x => x.ToScene);
        }

        public bool IsValidInteraction(SceneInteraction interaction)
        {
            Redict();
            return _sceneByInteraction.ContainsKey(interaction);
        }

        public GameScene GetNextScene(SceneInteraction interaction)
        {
            Redict();
            if (!IsValidInteraction(interaction)) return null;
            return _sceneByInteraction[interaction];
        }

        [Button]
        public void UpdateChildPanelIDs()
        {
            _panels.ForEach(x => x.Value.ID = x.Key);
        }
    }

    [Serializable]
    public struct SceneTransition
    {
        public SceneInteraction Interaction;
        public GameScene ToScene;
    }

    [Serializable]
    public enum SlotID
    {
        Bubble1, Bubble2, Bubble3, Char1, Char2, Char3, Environment
    }

    [Serializable]
    public struct SceneInteraction
    {
        public SlotID From;
        public SlotID To;

        public SceneInteraction(SlotID from, SlotID to)
        {
            From = from;
            To = to;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is SceneInteraction other)
            {
                return From.Equals(other.From) && To.Equals(other.To);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked // Allow overflow
            {
                int hash = 17;
                hash = hash * 23 + From.GetHashCode();
                hash = hash * 23 + To.GetHashCode();
                return hash;
            }
        }
    }
}