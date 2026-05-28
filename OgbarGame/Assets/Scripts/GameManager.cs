using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Enemy[] enemies;
    public static GameManager instance;
    public int score;
    
    public string filepath;

    private void Start()
    {
        filepath = Application.persistentDataPath + "/Save.json";
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveFile()
    {
        SaveData data = new();

        data.score = score; //Här skulle man Tex hämta info från inventoriet eller gamemanager

        string json = JsonUtility.ToJson(data, true); //False gör text filen mer compact men svårare att läsa

        File.WriteAllText(Application.persistentDataPath + "/Save.json", json); //Skriv ALL info som finns i detta script till "filePath" 

    }
    public void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Save.json"); //Läs json info från "filePath"

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            score = data.score;
        }
    }

    public void LoadScene()
    {
        //Load CombatScene
        SceneManager.LoadScene(1);

    }
}