using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Bubbles.GamePanels.Automater
{
    [CreateAssetMenu(fileName = "Soccer", menuName = "Soccer", order = 0)]
    public class SceneAssociator : ScriptableObject
    {
        [Header("Scriptables")]
        [SerializeField] private string _suffix;
        [SerializeField] private string _folderWithScenes;
        [SerializeField] private string _folderWithScriptables;

        [Header("Transitions")]
        [SerializeField] private string _newScenePrefabPath;
        [SerializeField] private string _oldNormalPrefix;
        [SerializeField] private string _oldEndingPrefix;
        [SerializeField] private string _oldSceneFolder;
        [SerializeField] private string _newNormalPrefix;
        [SerializeField] private string _newEndingPrefix;
        [SerializeField] private string _newSceneFolder;

        [Button(ButtonSizes.Gigantic)]
        public void Rebuild()
        {
            ZipScenes();
            CreateScenesFromScriptables();
        }

        [Button(ButtonSizes.Large)]
        public void ZipScenes()
        {
            string[] existingSceneFiles = Directory.GetFiles(_newSceneFolder, "*.prefab", SearchOption.AllDirectories);
            foreach (string file in existingSceneFiles)
            {
                AssetDatabase.DeleteAsset(file);
            }
            AssetDatabase.SaveAssets();
            
            string[] oldSceneFiles = Directory.GetFiles(_oldSceneFolder, "*.prefab", SearchOption.TopDirectoryOnly);
            Dictionary<int, List<SceneTransitionData>> interactions = new Dictionary<int, List<SceneTransitionData>>();
            // endings are negative
            foreach (string file in oldSceneFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                int fileNumber = SceneTransitionData.SceneNameToNumber(fileName, _oldNormalPrefix, _oldEndingPrefix);
                if (fileNumber == Int32.MinValue) continue;
                GameScene gameScene = AssetDatabase.LoadAssetAtPath<GameScene>(file);
                List<SceneTransitionData> datas =
                    gameScene.Transitions.Where(x => x.ToScene != null).Select(x => SceneTransitionData.FromInteraction(x, _oldNormalPrefix, _oldEndingPrefix)).ToList();
                interactions.Add(fileNumber, datas);
            }

            // debug 
            foreach (var kvp in interactions)
            {
                int from = kvp.Key;
                foreach (var transition in kvp.Value)
                {
                    Debug.Log($"Transition detected from {from} to {transition.ToScene}.");
                }
            }

            Dictionary<int, GameScene> numberToPrefab = new Dictionary<int, GameScene>();
            foreach (var kvp in interactions)
            {
                int fromSceneNumber = kvp.Key;
                // create new prefab
                string prefix = fromSceneNumber > 0 ? _newNormalPrefix : _newEndingPrefix;
                string number = $"{Math.Abs(fromSceneNumber)}";
                string toPath = Path.Combine(_newSceneFolder, prefix + number + ".prefab");
                bool success = AssetDatabase.CopyAsset(_newScenePrefabPath, toPath);
                
                if (success)
                {
                    Debug.Log($"Successfully made prefab at {toPath}");
                    GameScene scene = AssetDatabase.LoadAssetAtPath<GameScene>(toPath);
                    numberToPrefab.Add(fromSceneNumber, scene);
                    if (fromSceneNumber < 0)
                    {
                        int id = -fromSceneNumber;
                        scene.IsEndingScene = true;
                        scene.Ending = id;
                    }
                }
                else
                {
                    Debug.LogWarning($"Failed to make prefab at {toPath}");
                }
            }
            
            foreach (var kvp in interactions)
            {
                int fromSceneNumber = kvp.Key;
                List<SceneTransition> transitions = new List<SceneTransition>();
                foreach (SceneTransitionData interaction in kvp.Value)
                {
                    GameScene toScene = numberToPrefab[interaction.ToScene];
                    SceneTransition transition = new SceneTransition(interaction.From, interaction.To, toScene);
                    transitions.Add(transition);
                }
                GameScene fromScene = numberToPrefab[fromSceneNumber];
                fromScene.Transitions = transitions;
                Debug.Log($"Set {transitions.Count} transitions for scene {fromScene.name}");
                EditorUtility.SetDirty(fromScene);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button(ButtonSizes.Large)]
        public void CreateScenesFromScriptables()
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
            foreach (string sceneName in scenes)
            {
                string truncSceneName = Path.GetFileNameWithoutExtension(sceneName);
                if (!sceneDefs.ContainsKey(truncSceneName))
                {
                    Debug.LogWarning($"No associated script found for scene {truncSceneName}");
                    continue;
                }
                
                SceneMaker sceneMaker = AssetDatabase.LoadAssetAtPath<SceneMaker>(sceneName);
                sceneMaker.CreateScene(sceneDefs[truncSceneName]);
                AssetDatabase.SaveAssets();
                Debug.Log($"Made scene {sceneName}.");
            }
        }
    }

    public struct SceneTransitionData
    {
        public SlotID From;
        public SlotID To;
        public int ToScene;

        public SceneTransitionData(SlotID from, SlotID to, int toScene)
        {
            From = from;
            To = to;
            ToScene = toScene;
        }

        public static SceneTransitionData FromInteraction(SceneTransition transition, string normalPrefix, string endingPrefix)
        {
            string toSceneName = transition.ToScene.name;
            int toSceneNumber = SceneNameToNumber(toSceneName, normalPrefix, endingPrefix);
            SceneTransitionData data = new SceneTransitionData(transition.Interaction.From, transition.Interaction.To, toSceneNumber);
            return data;
        }

        public static int SceneNameToNumber(string fileName, string normalPrefix, string endingPrefix)
        {
            int fileNumber;
            if (fileName.Contains(normalPrefix))
            {
                string numberInt = fileName.Replace(normalPrefix, "");
                Debug.Log(numberInt);
                fileNumber = int.Parse(numberInt);
            }
            else if (fileName.Contains(endingPrefix))
            {
                string numberInt = fileName.Replace(endingPrefix, "");
                Debug.Log(numberInt);
                fileNumber = -int.Parse(numberInt);
            }
            else
            {
                Debug.LogWarning($"Invalid old file name {fileName}");
                fileNumber = Int32.MinValue;
            }

            return fileNumber;
        }

        public static string SceneNumberToName(int number, string normalPrefix, string endingPrefix)
        {
            if (number > 0)
            {
                return $"{normalPrefix}{number}";
            }
            if (number < 0)
            {
                return $"{endingPrefix}{-number}";
            }
            else
            {
                Debug.LogError("Scene number cannot be 0");
                return GUID.Generate().ToString();
            }
        }
    }
}