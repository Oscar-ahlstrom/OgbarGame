using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] public int playerDamage;
    [SerializeField] public int playerMaxHealth;
    public int playerHealth;

    //Gör om till en scriptableObject
    [Header("EnemyStats")]
    [SerializeField] public Sprite[] enemySprite;
    [SerializeField] public int[] enemyDamage;
    [SerializeField] public int[] enemyMaxHealth;
    public int[] enemyHealth;
    

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
        for (int Loop = 0; Loop < enemyTargetButtons.Length; Loop++)
        {
            enemyTargetButtons[Loop].active = false;
        }

        for (int Loop = 0; Loop < enemyMaxHealth.Length; Loop++)
        {
            enemyHealth[Loop] = enemyMaxHealth[Loop];
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
        enemyHealth[Target] = Mathf.Clamp(enemyHealth[Target], 0, enemyMaxHealth[Target]);

        if (enemyHealth[Target] <= 0)
        {
            //MAJOR_FIX Jag vill vinna när alla är döda och inte bara när en dör
            state = GameState.Win;
        }
    }

    public void TakeDamage()
    {
        playerHealth -= enemyDamage[Random.Range(0, enemyDamage.Length)];
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);

        if (playerHealth == 0)
        {
            state = GameState.Lose;
        }  
    }
}
