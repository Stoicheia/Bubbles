using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.Manager
{
    public class InteractablesManager : SerializedMonoBehaviour
    {
        [field: SerializeField] public List<Interactable> AllInteractables { get; private set; }

        public List<Interactable> GetActiveInteractables()
        {
            List<Interactable> active = AllInteractables.Where(x => x.IsActive).ToList();
            return active;
        }
        
        
    }
}