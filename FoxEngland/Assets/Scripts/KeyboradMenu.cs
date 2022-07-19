using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboradMenu : MonoBehaviour
{

    public float timeCounter = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
