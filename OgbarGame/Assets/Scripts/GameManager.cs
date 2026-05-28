using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    SaveData saveData;

    public int score;
    public int otherScore;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath + "/Save.json");
    }

    public void SaveGame()
    {
        SaveData data = new();

        data.score = score; //Här skulle man Tex hämta info från inventoriet eller gamemanager
        data.otherScore = otherScore;

        string json = JsonUtility.ToJson(data, true); //False gör text filen mer compact men svårare att läsa

        File.WriteAllText(Application.persistentDataPath + "/Save.json", json); //Skriv ALL info som finns i detta script till "filePath" 

    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Save.json"); //Läs json info från "filePath"

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            score = data.score;
            otherScore = data.otherScore;
        }
    }
}
