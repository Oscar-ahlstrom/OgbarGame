using System.IO;
using TMPro;
using UnityEngine;

public class LoadSaveToScoreboard : MonoBehaviour
{
    [SerializeField] GameObject MenuButtonCode;

    TextMeshProUGUI scoreText;
    MenuButtonsCode SaveVariable;

    private void Awake()
    {
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
        SaveVariable = MenuButtonCode.GetComponent<MenuButtonsCode>();


        if (File.Exists(Application.persistentDataPath + "/Save" + SaveVariable.slot + ".json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/Save" + SaveVariable.slot + ".json"); //Läs json info från "filePath"

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scoreText.text = ("Score = " + data.score);
            return;
        }
        else if (!File.Exists(Application.persistentDataPath + "/Save" + SaveVariable.slot + ".json"))
        {
            scoreText.text = ("Empty Save");
            Debug.Log("No Save found on slot" + SaveVariable.slot);
        }
    }
}
