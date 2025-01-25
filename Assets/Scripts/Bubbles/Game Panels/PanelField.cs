using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.GamePanels
{
    public class PanelField : MonoBehaviour
    {
        [field: SerializeField][field: ReadOnly] public SlotID ID { get; set; }
        [field: SerializeField] public Panel ActivePanelInstance { get; set; }

        public void LoadInstantly(Panel panelPrefab)
        {
            UnloadInstantly();
            ActivePanelInstance = Instantiate(panelPrefab, transform);
            ActivePanelInstance.transform.localPosition = Vector3.zero;
        }

        public void UnloadInstantly()
        {
            if (ActivePanelInstance != null)
            {
                Destroy(ActivePanelInstance.gameObject);
            }
        }
    }
}