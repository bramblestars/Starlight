using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{

    //private StarText starCounter;

    void Awake()
    {
       // starCount = GameObject.Find("CoinText").getComponent<StarText>
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            print("You picked up a star!");

        //Update GUI
        //starCounter.starCount++;

        gameObject.SetActive(false);
        
    }
}
