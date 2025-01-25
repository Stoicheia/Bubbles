using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.IO;
public class SaveManager : MonoBehaviour
{
    private static SaveManager Instance;

    public bool[] Endings = new bool[35];


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        // don't use in web
        GetEndings();
    }
    public void SaveEndings()
    {
        //may wanna get rid of this when webgl
        string path = "Endings.txt";
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (bool value in Endings)
            {
                writer.WriteLine(value); 
            }
        }
    }

    public void GetEndings()
    {
        string path = "Endings.txt";
        if (!File.Exists(path))
        {
            SaveEndings();
        }
        else
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < Endings.Length; i++)
            {
                if (bool.TryParse(lines[i], out bool result))
                {
                    Endings[i] = result;
                }
            }
        }
    }
}
