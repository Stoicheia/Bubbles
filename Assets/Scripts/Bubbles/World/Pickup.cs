using UnityEngine;

namespace Bubbles
{
    public class Pickup : MonoBehaviour
    {
        [field: SerializeField] public Item Item { get; private set; }
    }
}