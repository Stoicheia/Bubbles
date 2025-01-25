using System;
using UnityEngine;

namespace Bubbles
{
    [Serializable]
    public class Item
    {
        [field: SerializeField] public Sprite SpriteInTransit { get; private set; }
    }
}