using UnityEngine;

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
    [SerializeField] public GameObject[] enemyRenderers;
    public int[] enemyHealth;
    public int enemiesDead;


    [Header("Misc")]
    [SerializeField] public GameObject[] enemyTargetButtons;
    

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
        //Turn off target buttons by default
        for (int Loop = 0; Loop < enemyTargetButtons.Length; Loop++)
        {
            enemyTargetButtons[Loop].active = false;
        }
        //setup enemy health
        for (int Loop = 0; Loop < enemies.Length; Loop++)
        {
            enemyHealth[Loop] = enemies[Loop].maxHealth;
        }

        attackButtonIsPressed = false;
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
        playerHealth -= enemies[Random.Range(0, enemies.Length)].damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        if (playerHealth == 0)
        {
            state = GameState.Lose;
        }  
    }
}
