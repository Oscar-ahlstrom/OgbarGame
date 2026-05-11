using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    //MAJOR: 
    // make the menus and code scale with infinite enemies <---- DANGEROUS
    // Setup healthbars and connect them to the enemies current health
    //
    //MINOR:
    // Make a cool sprite in asprite and dither it 
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

    private float enemySpace;
    public GameState state;
    [SerializeField] public bool attackButtonIsPressed;
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


        //Spawna Enemies utifrån hur många som finns i arrayen och hämta alla komponenter som krävs
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemyTarget = //Spara referencen till objectet som spawnas 
            Instantiate(
            enemyTargetPrefab,   //Vad ska spawnas
            new Vector3(-enemySpace / 2 + (enemySpace * (i + 1)), Screen.height / 2, 0),    //Vart dom ska spawnas 
            quaternion.identity, //hur ska den roteras när den spawnar
            canvas.transform);   //Vilken transform ska den bo under  

            EnemyButton enemyButton = enemyTarget.GetComponentInChildren<EnemyButton>(); //Hämta Componenten från enemyTarget
            enemyButton.enabled = true;
            enemyButton.enemyId = i; //Componentens ID är loops
            enemyButton.combatManager = this;
            enemyTargetButtons[i] = enemyButton.gameObject;
            enemyButton.gameObject.SetActive(false);

            //FIX Hitta ett sätt att referera så att den kan hämta spriten till enemyn consitentlyoch inte randomly mellan 
            //Dubbel Verifikation med Tag och enemyTarget?

            Image image = enemyTarget.GetComponentInChildren<Image>();
            
            image.sprite = enemies[i].sprite;


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
        if (enemiesDead == enemies.Length)
        {
            state = GameState.Win;
        }
    }

    public void TakeDamage()
    {
        //Make all enemies do damage to you
        playerHealth -= enemies[UnityEngine.Random.Range(0, enemies.Length)].damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        if (playerHealth == 0)
        {
            state = GameState.Lose;
        }  
    }
}
