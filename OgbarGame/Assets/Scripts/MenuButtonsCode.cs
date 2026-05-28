using UnityEngine;

public class MenuButtonsCode : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    public void Save()
    {
        gameManager.SaveFile();
    }

    public void Load()
    {
        gameManager.LoadFile();
    }

    public void Scene()
    {
        gameManager.LoadScene();
    }
}
