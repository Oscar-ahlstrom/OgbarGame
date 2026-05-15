using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    //MAJOR: 
    // make the menus scale with infinite enemies <-- Farlig
    // Setup healthbars and connect them to the enemies current health
    // 
    //
    //MINOR:
    // Make a real enemy sprite (make a dead version) <- Optional
    // make a backround
    // 
    // 

    [Header("PlayerStats")]
    [SerializeField] public int playerDamage;
    [SerializeField] public int playerMaxHealth;
    public int playerHealth;

    //Gör om till en scriptableObject
    [Header("Enemy")]
    [SerializeField] public Enemy[] enemies;
    [SerializeField] public GameObject[] enemyImageObj;
    public int[] enemyHealth;
    public int enemiesDead;


    [Header("Misc")]
    public GameObject[] enemyTargetButtons;
    [SerializeField] public GameObject enemyTargetPrefab;
    [SerializeField] public GameObject canvas;
    [SerializeField] public Color deadColorMod;

    private float enemySpace;
    public GameState state;
    public bool attackButtonIsPressed;
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose,
    }

    private void Start()
    {
        enemySpace = (Screen.width / enemies.Length);

        //Definera Array Längder
        enemyTargetButtons = new GameObject[enemies.Length];
        enemyImageObj = new GameObject[enemies.Length];
        enemyHealth = new int[enemies.Length];


        //Spawna Enemies utifrån hur många som finns i arrayen
        //hämta alla komponenter som krävs

        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemyTarget = //Spara referencen till objectet som spawnas 
            Instantiate(
            enemyTargetPrefab,   //Vad ska spawnas
            new Vector3(-enemySpace / 2 + (enemySpace * (i + 1)), Screen.height / 2, 0),    //Vart dom ska spawnas 
            quaternion.identity, //hur ska den roteras när den spawnar
            canvas.transform);   //Vilken transform ska den bo under  

            ButtonInfo buttonInfo = enemyTarget.GetComponentInChildren<ButtonInfo>(); //Hämta Componenten från enemyTarget
            buttonInfo.enabled = true;
            buttonInfo.enemyId = i; //Componentens ID är loops
            buttonInfo.combatManager = this;

            TextMeshProUGUI targetButtonText = enemyTarget.GetComponentInChildren<TextMeshProUGUI>(); //Hämta Knappens Text 
            targetButtonText.text = ("Target " + (1 + i)); //Namnge texten efter vilken nummer target den är (Sätt in fiende namn?)

            //Om Jag vill ha mer än 1 av varje component så vär jag göra nya inheritade scripts så som jag gjorde med EnemyButton

            enemyTargetButtons[i] = buttonInfo.gameObject;
            buttonInfo.gameObject.SetActive(false); //Stäng av knappen efter att den är färdig uppsatt eftersom att vi inte vill att den ska synas by default

            //FIX Hitta ett sätt att referera så att den kan hämta spriten till enemyn consitently och inte randomly mellan button imagen och enemy imagen
            //Dubbel Verifikation med Tag och enemyTarget?
            //Det bara funkar??? 
            //Borde det inte vara en konflict mellan vilken image den vill ha??
            //VI STÄNGER AV KNAPPEN OCH DÄRFÖR KAN DEN INTE HITTA KNAPPEN IMAGE KOMPONENT

            Image enemySprite = enemyTarget.GetComponentInChildren<Image>();

            enemyImageObj[i] = enemySprite.gameObject;
            enemySprite.sprite = enemies[i].aliveSprite;

            


        }


        for (int Loop = 0; Loop < enemies.Length; Loop++)
        {
            enemyHealth[Loop] = enemies[Loop].maxHealth;
        }

        state = GameState.PlayerTurn;
        playerHealth = playerMaxHealth;

    }

    private void Update()
    {
        switch (state)
        {
            case GameState.PlayerTurn:
                //Enable Everything

                break;


            case GameState.EnemyTurn:
                //Disable everything 

                break;


            case GameState.Win:
                //Enable win screen
                //Rest of game in another Scene?

                break;


            case GameState.Lose:
                //Enable Lose Screen

                break;
        }
    }

    public void PressAttackButton()
    {
        
        switch (attackButtonIsPressed)
        {
            case false:
                for (int Loop = 0; Loop < enemyTargetButtons.Length; Loop++)
                {
                    if (enemyHealth[Loop] > 0)
                    {
                        enemyTargetButtons[Loop].active = true;
                    }
                }
                attackButtonIsPressed = true;
                break;


            case true:
                for(int Loop = 0; Loop < enemyTargetButtons.Length; Loop++)
                {
                    enemyTargetButtons[Loop].active = false;
                    
                }
                attackButtonIsPressed = false;
                break;
        }
        
    }

    public void DealDamage(int Target)
    {
        enemyHealth[Target] -= playerDamage;
        enemyHealth[Target] = Mathf.Clamp(enemyHealth[Target], 0, enemies[Target].maxHealth);

        if (enemyHealth[Target] > 0) { return; }

        enemiesDead++;
        Image image = enemyImageObj[Target].GetComponent<Image>();
        image.color = deadColorMod;
        image.sprite = enemies[Target].deadSprite;

        if (enemiesDead == enemies.Length)
        {
            state = GameState.Win;
        }
    }
}
