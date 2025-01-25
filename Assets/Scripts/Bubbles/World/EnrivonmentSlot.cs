using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public class EnrivonmentSlot : Interactable
    {
        [ReadOnly][SerializeField] private ItemAsset _activeItem;
        [SerializeField] private Image _itemField;
        [SerializeField] private Image _itemContainerField;
  

        public override Image GetImageToHighlight()
        {
            return _itemContainerField;
        }

        public override bool CanInteract(ItemAsset item)
        {
            if (_activeItem == null)
            {
                return true;
            }
            return _activeItem.CanInteractEnvironment(item);
        }

        public override bool Interact(ItemAsset item)
        {
            throw new System.NotImplementedException();
            // wait for storyboard... tower of hanoi? always?
        }

        public override bool RemoveChildPickup(Pickup pickup)
        {
            if (pickup.ParentInteractable != this) return false;
            _activeItem = null;
            return true;
        }

        public void RenderCurrentItemInstantly()
        {
            
        }
    }
}