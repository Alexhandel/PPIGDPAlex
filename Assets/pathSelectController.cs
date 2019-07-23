using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathSelectController : MonoBehaviour
{
    private float rand1, rand2;
    private string tag1, tag2;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rand1=Random.Range(1, 4);
        rand2 = Random.Range(4, 6);
        if (rand1 == 4)
        {
            rand1 = 3;
        }
        if (rand2 == 6)
        {
            rand2 = 5;
        }
        tag1 = "button" + rand1;
        tag2 = "button" + rand2;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            if (child.tag==tag1 || child.tag==tag2)
            {
                child.gameObject.SetActive(false);
            }
            
        }

    }
}
