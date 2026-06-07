using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("PlayerStats")]
    [SerializeField] public int playerDamage;
    public int playerMaxHealth;
    public float playerHealth;
    [SerializeField] public Image playerHealthBar;
    [SerializeField] public TextMeshProUGUI playerHealthText;

    [Header("Enemy")]
    public Enemy[] enemies;
    public GameObject[] enemyImageObj;
    public Animator[] enemyAnimationController;
    public GameObject[] enemyTargetButtons;
    public Image[] enemyHealthBar;
    public TextMeshProUGUI[] enemyHealthText;
    public float[] enemyHealth; //Jag skulle helst vilja hålla enemyhealth till heltal men för att räkna ut divisioner så behövde den vara en float
    public int enemiesDead; //How many are dead
    public bool[] enemyIsAlive; //is THIS enemy dead


    [Header("Misc")]
    [SerializeField] public GameObject enemyTargetPrefab;
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject uiPanel;
    public RectTransform panelTransform;
    private GameManager gameManager;

    [SerializeField] public Color deadColorMult;

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
        gameManager = GameManager.instance;
        enemies = new Enemy[gameManager.enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = gameManager.enemies[i];
        }


        enemySpace = (Screen.width / enemies.Length);

        //Definera Array Längder
        enemyTargetButtons = new GameObject[enemies.Length];
        enemyImageObj = new GameObject[enemies.Length];
        enemyAnimationController = new Animator[enemies.Length];
        enemyHealth = new float[enemies.Length];
        enemyHealthBar = new Image[enemies.Length];
        enemyHealthText = new TextMeshProUGUI[enemies.Length];
        enemyIsAlive = new bool[enemies.Length];

        //Spawna Enemies utifrån hur många som finns i arrayen
        //Hitta pointerScript för att hitta rätt object
        //hämta komponent från objecktet 
        //Gör ändringar på komponenten 


        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemyTarget = //Spara referencen till objectet som spawnas 
            Instantiate(
            enemyTargetPrefab,   //Vad ska spawnas
            new Vector3(-enemySpace / 2 + (enemySpace * (i + 1)), Screen.height / 2, 0),    //Vart dom ska spawnas 
            quaternion.identity, //hur ska den roteras när den spawnar
            canvas.transform);   //Vilken transform ska den bo under  

            ButtonInfo buttonInfo = enemyTarget.GetComponentInChildren<ButtonInfo>(); //Hämta Componenten från enemyTarget
            buttonInfo.gameObject.SetActive(true);
            buttonInfo.enemyId = i; //Componentens ID är loops
            buttonInfo.combatManager = this;

            TargetButtonText t = enemyTarget.GetComponentInChildren<TargetButtonText>(); //Hitta pointer script 
            TextMeshProUGUI targetButtonText = t.gameObject.GetComponent<TextMeshProUGUI>();  //Hitta komponent på gameobject
            targetButtonText.text = ("Target " + (1 + i)); //Namnge texten efter vilken nummer target den är(Sätt in fiende namn?)  

            //Här skulle man kunna spara Texten till en array också om vi kanske vill ändra den vid ett senare tillfälle


            enemyTargetButtons[i] = buttonInfo.gameObject;
            buttonInfo.gameObject.SetActive(false); //Stäng av knappen efter att den är färdig uppsatt eftersom att vi inte vill att den ska synas by default

            EnemySpriteImage esi = enemyTarget.GetComponentInChildren<EnemySpriteImage>();
            Image enemySprite = esi.gameObject.GetComponent<Image>();
            Animator enemyAnimator = esi.gameObject.GetComponent<Animator>();

            enemyAnimationController[i] = enemyAnimator;
            enemyImageObj[i] = enemySprite.gameObject;
            enemySprite.sprite = enemies[i].aliveSprite;

            HealthBar hb = enemyTarget.GetComponentInChildren<HealthBar>();
            Image healthFillImage = hb.gameObject.GetComponent<Image>();

            enemyHealthBar[i] = healthFillImage;
            healthFillImage.fillAmount = 100;

            HealthNumber hn = enemyTarget.GetComponentInChildren<HealthNumber>();
            TextMeshProUGUI HealthText = hn.gameObject.GetComponent<TextMeshProUGUI>();

            panelTransform = uiPanel.GetComponent<RectTransform>();
            panelTransform.anchoredPosition = new Vector2(0, -Screen.height / 2);

            enemyHealth[i] = enemies[i].maxHealth; //Definera hur mycket HP fienderna har 
            enemyHealthText[i] = HealthText;
            HealthText.text = ("" + (enemyHealth[i]));
            enemyIsAlive[i] = true;
        }

        //Spelaren ska börja 
        state = GameState.PlayerTurn;
        //Setup Spelar hälsan
        playerMaxHealth = gameManager.playerMaxHealth;
        playerHealth = gameManager.playerCurrentHealth;
        playerHealthBar.fillAmount = playerHealth / playerMaxHealth;
        playerHealthText.text = ("" + (playerHealth));

    }

    private void Update()
    {
        switch (state)
        {
            case GameState.PlayerTurn:
                panelTransform.anchoredPosition = new Vector3(0, 135, 0);

                break;


            case GameState.EnemyTurn:
                panelTransform.anchoredPosition = new Vector3(0, -135, 0);

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
                for (int Loop = 0; Loop < enemyTargetButtons.Length; Loop++)
                {
                    enemyTargetButtons[Loop].active = false;

                }
                attackButtonIsPressed = false;
                break;
        }

    }

    public void EnemyTakeDamage(int Target)
    {
        enemyHealth[Target] -= playerDamage;
        enemyHealth[Target] = Mathf.Clamp(enemyHealth[Target], 0, enemies[Target].maxHealth);
        enemyHealthBar[Target].fillAmount = enemyHealth[Target] / enemies[Target].maxHealth;
        enemyHealthText[Target].text = ("" + (enemyHealth[Target]));


        if (enemyHealth[Target] > 0)
        {
            enemyAnimationController[Target].SetTrigger("Damage");
            return;
        }

        enemyAnimationController[Target].SetBool("IsDead", true);
        enemyIsAlive[Target] = false;

        gameManager.score += 1;
        gameManager.money += enemies[Target].money;
        gameManager.xp += enemies[Target].xp;


        enemiesDead++;
        Image image = enemyImageObj[Target].GetComponent<Image>();
        image.color = deadColorMult;
        image.sprite = enemies[Target].deadSprite;

        if (enemiesDead == enemies.Length)
        {

            gameManager.playerCurrentHealth = (int)playerHealth;
            Invoke("LoadHomeScene",1f);
            //Switcha gamestate för att ta fram en win screen så att spelaren kan själv välja när man ska gå vidare
        }
    }


    public void PlayEnemyTurn()
    {
        StartCoroutine(PlayEnemyTurnCoroutine());
    }
    public IEnumerator PlayEnemyTurnCoroutine()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemyIsAlive[i])
            {
                enemyAnimationController[i].SetTrigger("Attack");
                playerHealth -= enemies[i].damage;
                playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
                playerHealthBar.fillAmount = (playerHealth / playerMaxHealth);
                playerHealthText.text = ("" + (playerHealth));
                yield return new WaitForSeconds(1f);
            }
        }
        FlipState();
    }

    public void FlipState()
    {
        switch (state)
        {
            case GameState.PlayerTurn:
                state = GameState.EnemyTurn;

                break;


            case GameState.EnemyTurn:
                state = GameState.PlayerTurn;

                break;


        }
    }

    public void LoadHomeScene()
    {
        SceneManager.LoadScene(0);
    }
}
