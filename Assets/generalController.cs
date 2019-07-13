using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class generalController : MonoBehaviour
{
    private int enemyCount;
    private Vector3 temp;
    public float initialEnemyHealth, initialEnemyDefense, initialEnemyAttack;
    public GameObject player;
    public GameObject enemyPrefab;
    public TextMeshProUGUI enemyText;
    private bool i=true;
    private int eCount=1;

    private GameObject currentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(initialEnemyHealth, initialEnemyDefense, initialEnemyAttack);
    }

    // Update is called once per frame
    void Update()
    {
        if (i)
        {
            if (!currentEnemy.GetComponent<enemyController>().alive)
            {
                i = false;
                Debug.Log("enemy is dead");
                StartCoroutine("spawnNewEnemy");
            }
        }
        
    }

    IEnumerator spawnNewEnemy()
    {
        yield return new WaitForSeconds(3);
        Destroy(currentEnemy);
        initialEnemyHealth += 100;
        initialEnemyDefense += 1;
        initialEnemyAttack += 5;
        eCount += 1;
        SpawnEnemy(initialEnemyHealth, initialEnemyDefense, initialEnemyAttack);
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
}
