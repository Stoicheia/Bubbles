using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles
{
    public class Interactable : SerializedMonoBehaviour
    {
        [field: SerializeField] public bool IsActive { get; private set; }

    }
}