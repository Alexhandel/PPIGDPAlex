using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVariables : MonoBehaviour
{
    public int highScore;
    private void Start()
    {
        //highScore = -2;
    }
    private void Update()
    {
        if (highScore<0)
        {
            Destroy(gameObject);
        }
    }
}
