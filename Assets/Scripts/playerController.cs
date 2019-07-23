using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float attack;
    public float defense;
    public bool alive=true;
    private float damage = 0;
    private float healthNumber;
    public float counter, timetoAttack, minTime, maxTime;
    public TextMeshProUGUI text;
    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (currentEnemy == null)
        {
            currentEnemy = GameObject.FindWithTag("Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateText(text);
        if (currentEnemy!=null&&alive==true)
        {
            if (currentHealth < 0.500001)
            {
                alive = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/rip");
                transform.localScale = new Vector3(2.24f, 2.24f, 0);
            }
            else if (counter >= timetoAttack)
            {
                Ataca();
                counter = 0;
                timetoAttack = Random.Range(minTime, maxTime);
            }
            else
            {
                counter += Time.deltaTime;
            }
        }
        
    }
    public void Ataca()
    {
        if ((attack - currentEnemy.GetComponent<enemyController>().defense) <= 1)
        {
            damage = 1;
        }
        else
        {
            damage = attack - currentEnemy.GetComponent<enemyController>().defense;
        }
        currentEnemy.GetComponent<enemyController>().health -= damage;
        if (currentEnemy.GetComponent<enemyController>().health>=1)
        {
            currentEnemy.GetComponent<Animation>().Play();
        }
        
    }
    void updateText(TextMeshProUGUI text)
    {
        
        if (currentHealth <= 0)
        {
            healthNumber = 0;
        }
        else
        {
            healthNumber = currentHealth;
        }
        text.SetText("Player \nHealth: " + Mathf.Round(healthNumber) + "/" + Mathf.Round(maxHealth) + "\nAttack: " + Mathf.Round(attack) + "\nDefense: " + Mathf.Round(defense));
    }
}
