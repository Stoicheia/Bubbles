using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bubbles.GamePanels.Automater
{
    [CreateAssetMenu(fileName = "Soccer", menuName = "Soccer", order = 0)]
    public class SceneAssociator : ScriptableObject
    {
        [SerializeField] private string _suffix;
        [SerializeField] private string _folderWithScenes;
        [SerializeField] private string _folderWithScriptables;

        [Button]
        public void CreateScenes()
        {
            Dictionary<string, SceneDefinition> sceneDefs = new Dictionary<string, SceneDefinition>();
            string[] files = Directory.GetFiles(_folderWithScriptables, "*.asset", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                SceneDefinition sceneDef = AssetDatabase.LoadAssetAtPath<SceneDefinition>(file);
                string defName = sceneDef.name;
                string soccerName = defName.Replace(_suffix, "");
                sceneDefs.Add(soccerName, sceneDef);
            }
            
            List<string> scenes = Directory.GetFiles(_folderWithScenes, "*.prefab", SearchOption.TopDirectoryOnly).ToList();
            foreach (var kvp in sceneDefs)
            {
                string sceneName = kvp.Key;
                if (!scenes.Contains(sceneName))
                {
                    Debug.LogWarning($"No associated scene found for scriptable {sceneName}.");
                    continue;
                }

                SceneDefinition def = sceneDefs[sceneName];
                
            }
        }
    }
}