using UnityEngine;

namespace Ending
{
    [CreateAssetMenu(fileName = "Ending Asset", menuName = "Ending Asset", order = 0)]
    public class EndingAsset : ScriptableObject
    {
        public int Number;
        public Sprite Graphic;
        [TextArea(2, 8)] public string Text;
    }
}