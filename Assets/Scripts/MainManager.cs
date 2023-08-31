using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color TeamColor;

    public void NewColorSelected(Color color)
    {
        MainManager.Instance.TeamColor = color;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor() 
    {
        //create a new instance of the save data and filled its team color class member with the TeamColor 
        //variable saved in the MainManager.
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;
        //Transform that instance to JSON with JsonUtility.ToJson:
        string json = JsonUtility.ToJson(data);
        //Used special method to write string to a file.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {   
            //if file exists then the method will read "File.ReadAllText".
            string json = File.ReadAllText(path);
            //it will then give the resulting text to JsonUtility.FromJson to transform it back into a SaveData instance.
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Set the TeamColor to the color saved in that SaveData.
            TeamColor = data.TeamColor;
        }
    }
}
