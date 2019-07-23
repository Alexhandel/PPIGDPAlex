using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class generalController : MonoBehaviour
{
    private int enemyCount;
    private Vector3 temp;
    private float evEnemyAtkMod, evEnemyDefMod, evEnemyHpMod, evPlayerAtkMod, evPlayerDefMod, evPlayerHealthMod, randN = 1f;
    private string eventType, eventStat, firstLine, secondLine, evText;
    private float tempPlayerAtk, tempPlayerDef, tempPlayerHp, tempEnemyAtk, tempEnemyDef, tempEnemyHp;
    public int atkIncrement, defIncrement, hpIncrement;
    public float initialEnemyHealth, initialEnemyDefense, initialEnemyAttack;
    public float eventChance;
    public GameObject player;
    public GameObject enemyPrefab;
    public TextMeshProUGUI enemyText;
    public GameObject lvlupUI, pathSelectUI, lossUI, eventUI, background, variables;
    private bool i=true;
    private int eCount=1;

    private GameObject currentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(initialEnemyHealth, initialEnemyDefense, initialEnemyAttack);
        tempPlayerAtk = player.GetComponent<playerController>().attack;
        tempPlayerDef = player.GetComponent<playerController>().defense;
        tempPlayerHp = player.GetComponent<playerController>().maxHealth;
        tempEnemyAtk = initialEnemyAttack;
        tempEnemyDef = initialEnemyDefense;
        tempEnemyHp = initialEnemyHealth;
        variables = GameObject.FindWithTag("persistentVariables");
        if (variables.GetComponent<globalVariables>().highScore<1)
        {
            variables.GetComponent<globalVariables>().highScore = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (i)
        {
            if (player.GetComponent<playerController>().alive == false)
            {
                i = false;
                if (eCount-1> variables.GetComponent<globalVariables>().highScore)
                {
                    variables.GetComponent<globalVariables>().highScore = eCount-1;
                }
                lossUI.GetComponentInChildren<TextMeshProUGUI>().SetText("High Score:" + variables.GetComponent<globalVariables>().highScore);
                lossUI.SetActive(true);
            }
            else if (!currentEnemy.GetComponent<enemyController>().alive)
            {
                i = false;
                generateEvent();
                evText=generateEventText();
                eventUI.GetComponentInChildren<TextMeshProUGUI>().SetText(evText);
                eventUI.SetActive(true);
            }
        }
        
    }
    public void generateEvent()
    {
        evEnemyAtkMod = 1;
        evEnemyDefMod = 1;
        evEnemyHpMod = 1;
        evPlayerAtkMod = 1;
        evPlayerDefMod = 1;
        evPlayerHealthMod = 1;
        randN = UnityEngine.Random.Range(0f, 1f);
        if (UnityEngine.Random.Range(0f,1f)>=eventChance)
        {
            eventType = "none";
        }
        else
        {
            if (UnityEngine.Random.Range(0f, 1f) >= 0.4)
            {
                eventType = "blessing";
                if (randN <= 0.33)
                {
                    eventStat = "attack";
                    evPlayerAtkMod = 1.3f;
                }
                else if (randN >= 0.67)
                {
                    eventStat = "defense";
                    evPlayerDefMod = 1.3f;
                }
                else
                {
                    eventStat = "health";
                    evPlayerHealthMod = 1.3f;
                }
            }
            else
            {
                eventType = "curse";
                if (randN <= 0.33)
                {
                    eventStat = "attack";
                    evEnemyAtkMod = 1.2f;
                }
                else if (randN >= 0.67)
                {
                    eventStat = "defense";
                    evEnemyDefMod = 1.2f;
                }
                else
                {
                    eventStat = "health";
                    evEnemyHpMod = 1.2f;
                }
            }
        }
    }
    public void goToLevelUp()
    {
        eventUI.SetActive(false);
        lvlupUI.SetActive(true);
    }
    private string generateEventText()
    {
        if (eventType=="none")
        {
            firstLine = "You continue with your adventure";
            secondLine = "";
        }
        else
        {
            if (eventType=="blessing")
            {
                firstLine = "You have received a blessing from the fairies!";
                secondLine = "Your " + eventStat + " will be increased next battle!";
            }
            else
            {
                firstLine = "You have received a curse from the witches!";
                secondLine = "The enemy's " + eventStat + " will be increased next battle!";
            }
        }
        return firstLine + "\n" + secondLine;
    }
    public void levelUp(string statUp)
    {
        lvlupUI.SetActive(false);
        player.GetComponent<playerController>().attack = tempPlayerAtk+(1+0.1f*eCount);
        player.GetComponent<playerController>().defense = tempPlayerDef+ (1 + 0.1f * eCount);
        player.GetComponent<playerController>().maxHealth = tempPlayerHp+ (10 + 2 * eCount);
        if (statUp == "atk")
        {
            player.GetComponent<playerController>().attack += atkIncrement;
        }
        else if (statUp == "def")
        {
            player.GetComponent<playerController>().defense += defIncrement;
        }
        else if (statUp == "hp")
        {
            player.GetComponent<playerController>().maxHealth += hpIncrement;
        }
        tempPlayerAtk = player.GetComponent<playerController>().attack;
        tempPlayerDef = player.GetComponent<playerController>().defense;
        tempPlayerHp = player.GetComponent<playerController>().maxHealth;
        player.GetComponent<playerController>().attack *= evPlayerAtkMod;
        player.GetComponent<playerController>().defense *= evPlayerDefMod;
        player.GetComponent<playerController>().maxHealth *= evPlayerHealthMod;
        player.GetComponent<playerController>().currentHealth = player.GetComponent<playerController>().maxHealth;
        
        pathSelectUI.SetActive(true);
    }
    public void resumeGame(string path)
    {
        pathSelectUI.SetActive(false);
        Destroy(currentEnemy);
        eCount += 1;
        initialEnemyHealth = tempEnemyHp + (10 + (2.4f*eCount));
        initialEnemyDefense = tempEnemyDef + (1 + 0.25f*eCount);
        initialEnemyAttack = tempEnemyAtk + (1 +(0.25f*eCount));
        tempEnemyHp = initialEnemyHealth;
        tempEnemyDef = initialEnemyDefense;
        tempEnemyAtk = initialEnemyAttack;
        initialEnemyHealth *= evEnemyHpMod;
        initialEnemyDefense *= evEnemyDefMod;
        initialEnemyAttack *= evEnemyAtkMod;
        //enemyAtkIncrement = initialEnemyAttack;
        //enemyDefIncrement = initialEnemyDefense;
        //enemyHpIncrement = initialEnemyHealth;
        if (path=="plains")
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/plains");
            background.GetComponent<Transform>().localScale = new Vector3(-1.84f, 1.54f, 0f);
            SpawnEnemy(initialEnemyHealth*1.2f, initialEnemyDefense, initialEnemyAttack*0.9f);
        }
        else if (path=="mountain")
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/mountain");
            background.GetComponent<Transform>().localScale = new Vector3(3.55f, 4.3006f, 0f);
            SpawnEnemy(initialEnemyHealth*0.8f, initialEnemyDefense, initialEnemyAttack*1.2f);
            currentEnemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemySprites/mountainEnemy");
            currentEnemy.GetComponent<Transform>().localScale = new Vector3(-1.05f, 0.92f, 1f);
        }
        else if (path == "forest")
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/forest");
            background.GetComponent<Transform>().localScale = new Vector3(-3.0625f, 3.2f, 0f);
            SpawnEnemy(initialEnemyHealth * 1.4f, initialEnemyDefense*0.6f, initialEnemyAttack*0.95f);
            currentEnemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemySprites/forestEnemy");
            currentEnemy.GetComponent<Transform>().localScale = new Vector3(-1.02f, 0.92f, 1f);
        }
        else if (path == "swamp")
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/swamp");
            background.GetComponent<Transform>().localScale = new Vector3(2.35f, 2.1127f, 0f);
            SpawnEnemy(initialEnemyHealth, initialEnemyDefense*0.6f, initialEnemyAttack * 1.3f);
            currentEnemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemySprites/swampEnemy");
            currentEnemy.GetComponent<Transform>().localScale = new Vector3(-2.2f, 2.09f, 1f);
        }
        else if (path == "island")
        {
            background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/island");
            background.GetComponent<Transform>().localScale = new Vector3(1.8375f, 1.6876f, 0f);
            SpawnEnemy(initialEnemyHealth * 0.7f, initialEnemyDefense*1.15f, initialEnemyAttack*0.95f);
            currentEnemy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/EnemySprites/islandEnemy");
            currentEnemy.GetComponent<Transform>().localScale = new Vector3(1.04f, 1.04f, 1f);
        }

        i = true;
    }

    void SpawnEnemy (float health, float defense, float attack)
    {
        currentEnemy = Instantiate(enemyPrefab);
        currentEnemy.GetComponent<enemyController>().text = enemyText;
        currentEnemy.GetComponent<enemyController>().player = player;
        currentEnemy.GetComponent<enemyController>().health = health;
        currentEnemy.GetComponent<enemyController>().defense = defense;
        currentEnemy.GetComponent<enemyController>().attack = attack;
        currentEnemy.GetComponent<enemyController>().enemyCount = eCount;

    }

    public void gameReset()
    {
        SceneManager.LoadScene(1);
    }
}
