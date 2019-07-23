using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemyController : MonoBehaviour
{
    public float health;
    public float attack;
    public float defense;
    public int enemyCount;
    public float counter;
    public float minTime, maxTime, timetoAttack;
    private float healthNumber;
    private float damage = 0;
    public bool alive = true;
    public TextMeshProUGUI text;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (text==null)
        {
            Debug.Log("Help me");
        }
        timetoAttack = Random.Range(minTime, maxTime);
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateText(text);
        if ((health < 0.500001) && alive==true)
        {
            alive = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/rip");
            transform.localScale = new Vector3(2.5f, 2.5f, 0);
        }
        if (alive==true)
        {
            if(counter >= timetoAttack)
            {
                Ataca();
                counter = 0;
                timetoAttack = Random.Range(minTime, maxTime);
            } else
            {
                counter += Time.deltaTime;
            }
        }        
    }
    public void Ataca()
    {
        if ((attack - player.GetComponent<playerController>().defense)<=1)
        {
            damage = 1;
        }
        else
        {
            damage = attack - player.GetComponent<playerController>().defense;
        }
        player.GetComponent<playerController>().currentHealth -= damage;
        if (player.GetComponent<playerController>().currentHealth >= 1)
        {
            player.GetComponent<Animation>().Play();
        }
    }
    void updateText(TextMeshProUGUI text)
    {
        healthNumber = health;
        if (health <= 0)
        {
            healthNumber = 0;
        }
        text.SetText("Enemy " + enemyCount + "\nHealth: " + Mathf.Round(healthNumber) + "\nAttack: " + Mathf.Round(attack) + "\nDefense: " + Mathf.Round(defense));
    }
}
