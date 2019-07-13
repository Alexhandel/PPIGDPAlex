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
    private float healthNumber;
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
    }

    // Update is called once per frame
    void Update()
    {
        updateText(text);
        if ((health <= 0) && alive==true)
        {
            alive = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/rip");
            transform.localScale -= new Vector3(-7.5f, 2.5f, 0);
        }
        if (alive==true)
        {
            if (Random.value <= 0.01)
            {
                player.GetComponent<playerController>().health -= (attack - player.GetComponent<playerController>().defense);
            }
        }        
    }
    void updateText(TextMeshProUGUI text)
    {
        healthNumber = health;
        if (health <= 0)
        {
            healthNumber = 0;
        }
        text.SetText("Enemy " + enemyCount + "\nHealth: " + healthNumber + "\nAttack: " + attack + "\nDefense: " + defense);
    }
}
