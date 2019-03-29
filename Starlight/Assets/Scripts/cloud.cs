using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cloud : MonoBehaviour
{
    public string type = "stationary";
    public float[] parameters;
    public GameObject[] cloudListX;
    public GameObject[] cloudListY;
    public GameObject[] vanishing;
    public GameObject[] bouncing;
    public bool act = true;


    // Start is called before the first frame update
    void Start()
    {

    }
    void flipDirection()
    {
        if (parameters[2] < 1)
            parameters[2] = 1;
        else parameters[2] = -1;
    }
    void interact()
    {
        switch(type)
        {
            case "button":
                {
                    int x = (int)parameters[0];
                    GameObject[] cL;
                    switch (x)
                    {
                        case 0: cL = cloudListX;
                            break;
                        case 1: cL = cloudListY;
                            break;
                        case 2: cL = vanishing;
                            break;
                        default: cL = bouncing;
                            break;
                    }

                    for (int counter = 0; counter < cL.Length; counter++)
                    {
                        cL[counter].GetComponent<cloud>().act = !cL[counter].GetComponent<cloud>().act;
                    }
                }
                break;
            case "bounce":
                {
                    parameters[0] = 1;
                }
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (act)
        {
           
            switch (type)
            {
                case "movingX":
                    {

                        float maxX = parameters[1];
                        float minX = parameters[0];
                        float direction = parameters[2];
                        float increment = parameters[3];
                        float x = this.transform.position.x;
                        float y = this.transform.position.y;
                        float newX = x + direction*increment;
                        if (newX >= maxX)
                        {
                            flipDirection();
                            newX = maxX;
                        }
                        else if (newX <= minX)
                        {
                            flipDirection();
                            newX = minX;
                        }
                        Vector2 finalPlace = new Vector2(newX, y);
                        this.transform.position = finalPlace;

                    }
                    break;
                case "movingY":
                    {
                        float maxY = parameters[1];
                        float minY = parameters[0];
                        float direction = parameters[2];
                        float increment = parameters[3];
                        float y = this.transform.position.y;
                        float x = this.transform.position.x;
                        float newY = y + direction*increment;
                        if (newY >= maxY)
                        {
                            flipDirection();
                            newY = maxY;
                        }
                        else if (newY <= minY)
                        {
                            flipDirection();
                            newY = minY;
                        }
                        Vector2 finalPlace = new Vector2(x, newY);
                        this.transform.position = finalPlace;
                    }
                    break;
                case "vanish":
                    { 
                    float t = parameters[0];
                        float v = parameters[2];
                        if (t > 100)
                        {
                            if (v < 1)
                            {
                                this.gameObject.GetComponent<Renderer>().enabled = false;
                                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            }
                            else
                            {
                                this.gameObject.GetComponent<Renderer>().enabled = true;
                                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                            }
                            flipDirection();
                            parameters[0] = -1;
                        }
                        parameters[0] += 1;
                    }
                break;
                default:
                    break;
            }
        }
             
    }
}
