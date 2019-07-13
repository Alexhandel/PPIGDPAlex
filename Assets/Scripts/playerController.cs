using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerController : MonoBehaviour
{
    public float health;
    public float attack;
    public float defense;
    public TextMeshProUGUI text;
    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (currentEnemy!=null)
        {
            if (Input.anyKeyDown)
            {
                currentEnemy.GetComponent<enemyController>().health -= (attack - currentEnemy.GetComponent<enemyController>().defense);
            }
        }
        
    }

    void updateText(TextMeshProUGUI text)
    {
        text.SetText("Player \nHealth: " + health + "\nAttack: " + attack + "\nDefense: " + defense);
    }
}
