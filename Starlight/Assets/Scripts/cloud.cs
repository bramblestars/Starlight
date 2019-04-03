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
    public bool act = false;


    // Start is called before the first frame update
    void Start()
    {
        if (type == "movingX")
        {
            float currX = transform.position.x;
            float furthDist = parameters[4];
            parameters[0] = currX - furthDist;
            parameters[1] = currX + furthDist;
        }
        else if (type == "movingY")
        {
            float currY = transform.position.y;
            float furthDist = parameters[4];
            parameters[0] = currY - furthDist;
            parameters[1] = currY + furthDist;

        }
        else if (type == "vanish")
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void flipDirection()
    {
        if (parameters[2] < 1)
            parameters[2] = 1;
        else parameters[2] = -1;
    }
    public void interact()
    {
        Debug.Log("interacted");
        switch(type)
        {
            case "button":
                {
                    int x = (int)parameters[0];
                    GameObject[] cL;
                    switch (x)
                    {
                        case 0:
                            {
                                cL = cloudListX;
                                for (int counter = 0; counter < cL.Length; counter++)
                                {
                                    cL[counter].GetComponent<cloud>().act = !cL[counter].GetComponent<cloud>().act;

                                    if (cL[counter].GetComponent<cloud>().act)
                                    {
                                        Debug.Log("Hi");
                                       
                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                                        Debug.Log(cL[counter].gameObject.GetComponent<SpriteRenderer>().color);
                                    }
                                    else
                                    {
                                      
                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                    }

                                }
                            }
                            break;
                        case 1:
                            {
                                cL = cloudListY;
                                for (int counter = 0; counter < cL.Length; counter++)
                                {


                                    cL[counter].GetComponent<cloud>().act = !cL[counter].GetComponent<cloud>().act;

                                    if (cL[counter].GetComponent<cloud>().act)
                                    {
                                        Debug.Log("Hi");

                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                                        Debug.Log(cL[counter].gameObject.GetComponent<SpriteRenderer>().color);
                                    }
                                    else
                                    {

                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                    }


                                }
                            }
                            break;
                        case 2:
                            {
                                cL = vanishing;
                                for (int counter = 0; counter < cL.Length; counter++)
                                {


                                    cL[counter].GetComponent<cloud>().act = !cL[counter].GetComponent<cloud>().act;

                                    if (cL[counter].GetComponent<cloud>().act)
                                    {
                                        Debug.Log("Hi");
                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                                        Debug.Log(cL[counter].gameObject.GetComponent<SpriteRenderer>().color);
                                    }
                                    else
                                    {
                                        cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                    }


                                }
                            }
                            break;
                        default:
                            {
                                cL = bouncing;
                                for (int counter = 0; counter < cL.Length; counter++)
                                {


                                    cL[counter].GetComponent<cloud>().act = !cL[counter].GetComponent<cloud>().act;
                           
                                        if (cL[counter].GetComponent<cloud>().act)
                                        {
                                            Debug.Log("Hi");
                                            cL[counter].gameObject.GetComponent<BoxCollider2D>().GetComponent<PhysicsMaterial2D>().bounciness = 1;
                                            cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                                            Debug.Log(cL[counter].gameObject.GetComponent<SpriteRenderer>().color);
                                        }
                                        else
                                        {
                                            cL[counter].gameObject.GetComponent<BoxCollider2D>().GetComponent<PhysicsMaterial2D>().bounciness = 0;
                                            cL[counter].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                                        }

                                    
                                }
                            }
                            break;
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
                        float tF = parameters[1];
                        float v = parameters[2];
                        if (t > tF)
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
