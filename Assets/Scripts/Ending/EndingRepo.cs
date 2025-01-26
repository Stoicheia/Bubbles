using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ending
{
    [CreateAssetMenu(fileName = "Ending Repo", menuName = "Ending Repo", order = 0)]
    public class EndingRepo : ScriptableObject
    {
        public List<EndingAsset> Endings;

        public EndingAsset GetEndingAsset(int number)
        {
            return Endings[number];
        }

        private void OnValidate()
        {
            for (int i = 0; i < Endings.Count; i++)
            {
                if (Endings[i] == null) continue;
                Endings[i].Number = i + 1;
            }
        }
    }
}