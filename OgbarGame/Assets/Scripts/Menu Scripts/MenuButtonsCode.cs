using TMPro;
using UnityEngine;

public class MenuButtonsCode : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI SceneLoadButton;
    public int slot;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void Save()
    {
        gameManager.SaveFile(slot);
    }

    public void Load()
    {
        gameManager.LoadFile(slot);
    }

    public void Scene()
    {
        gameManager.LoadScene();
    }

    public void ChangeText()
    {
        SceneLoadButton.text = ("Load "+slot).ToString();
    }
}
