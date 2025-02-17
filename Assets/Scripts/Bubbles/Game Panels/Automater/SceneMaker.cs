﻿using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Bubbles.GamePanels.Automater
{
    public class SceneMaker : MonoBehaviour
    {
        [SerializeField] private BubbleMaker _princessBubble;
        [SerializeField] private BubbleMaker _knightBubble;
        [SerializeField] private BubbleMaker _dragonBubble;
        [SerializeField] private CharacterMaker _princessCharacter;
        [SerializeField] private CharacterMaker _knightCharacter;
        [SerializeField] private CharacterMaker _dragonCharacter;
        [SerializeField] private EnvironmentMaker _itemOnGround;

        [Button]
        public void Associate()
        {
            GameScene scene = GetComponent<GameScene>();
            var prefabs = scene.PanelPrefabs;
            _princessBubble = prefabs[SlotID.Bubble1].GetOrAddComponent<BubbleMaker>();
        }

        [Button(ButtonSizes.Large)]
        public void CreateScene(SceneDefinition scene)
        {
            _princessBubble.Create(scene.PrincessBubble);
            _knightBubble.Create(scene.KnightBubble);
            _dragonBubble.Create(scene.DragonBubble);
            _princessCharacter.Create(scene.PrincessCharacter);
            _knightCharacter.Create(scene.KnightCharacter);
            _dragonCharacter.Create(scene.DragonCharacter);
            _itemOnGround.Create(scene.Environment);
        }
    }
}