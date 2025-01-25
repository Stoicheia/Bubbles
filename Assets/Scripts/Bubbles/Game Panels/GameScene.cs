using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles.GamePanels
{
    public class GameScene : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<SlotID, Panel> _panels;
        [OdinSerialize] private Dictionary<SceneInteraction, GameScene> _activePanels;

        public bool IsValidInteraction(SceneInteraction interaction)
        {
            return _activePanels.ContainsKey(interaction);
        }

        public GameScene GetNextScene(SceneInteraction interaction)
        {
            if (!IsValidInteraction(interaction)) return null;
            return _activePanels[interaction];
        }
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
    }
}