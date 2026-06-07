using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Enemy[] enemies;
    public static GameManager instance;
    public int score;
    public int xp;
    public int money;

    public string filepath;

    [Header("Player Stats")]
    [SerializeField] public int playerMaxHealth;
    [SerializeField] public int playerCurrentHealth;

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

    public void SaveFile(int slot)
    {
        SaveData data = new();

        data.score = score; //Här skulle man Tex hämta info från inventoriet eller gamemanager
        data.xp = xp;
        data.money = money;

        data.playerMaxHealth = playerMaxHealth;
        data.playerCurrentHealth = playerCurrentHealth;

        string json = JsonUtility.ToJson(data, true); //False gör text filen mer compact men svårare att läsa

        File.WriteAllText(Application.persistentDataPath + "/Save" + slot + ".json" , json); //Skriv ALL info som finns i detta script till "filePath" 

    }
    public void LoadFile(int slot)
    {
        if (File.Exists(Application.persistentDataPath + "/Save" + slot + ".json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Save" + slot + ".json"); //Läs json info från "filePath"

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            score = data.score;
            xp = data.xp;
            money = data.money;

            playerMaxHealth = data.playerMaxHealth;
            playerCurrentHealth = data.playerCurrentHealth;



            return;
        }
        Debug.Log("No Save found on slot" + slot);
    }

    public void LoadScene()
    {
        //Load CombatScene
        SceneManager.LoadScene(1);

    }
}