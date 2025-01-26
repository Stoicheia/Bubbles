using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles.GamePanels.Automater
{
    [CreateAssetMenu(fileName = "Scene Def", menuName = "Scene Def", order = 0)]
    public class SceneDefinition : ScriptableObject
    {
        public BubbleMakerData PrincessBubble;
        public BubbleMakerData KnightBubble;
        public BubbleMakerData DragonBubble;
        public CharacterMakerData PrincessCharacter;
        public CharacterMakerData KnightCharacter;
        public CharacterMakerData DragonCharacter;
        public EnvironmentMakerData Environment;
    }

    [Serializable]
    public struct BubbleMakerData
    {
        public Sprite PickupSprite;
        public List<Sprite> ExtraSprites;
    }

    [Serializable]
    public struct CharacterMakerData
    {
        public Sprite Base;
        public Sprite Expression;
    }

    [Serializable]
    public struct EnvironmentMakerData
    {
        public Sprite Item;
    }
}