using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnemonesData : MonoBehaviour
{
    public string code;
    public string kind;
    public int level;
    public float damage;
    public float cooling;
    public string[] points;

    public Sprite[] InfoSprites;//information images

    public GameObject OverlayPoint;//now point
    public GameObject LastPoint;//last point

    public bool overlap = false;//whether overlap with other anemone
    public GameObject OverlapAnemone;//which anemone overlaped


    public Sprite PickedSprite()
    {
        return InfoSprites[level - 1];//show detail
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "points")
        {
            OverlayPoint = other.gameObject;
        }
        else if (other.gameObject.tag == "CanDrag")
        {
            overlap = true;
            OverlapAnemone = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "points" && OverlayPoint == other.gameObject)
        {
            OverlayPoint = null;

        }
        if (other.gameObject.tag == "CanDrag")
        {
            overlap = false;
            OverlapAnemone = null;
        }

    }

    //all the data of all anemones
    //give name and level to define a anemones

    void Start()
    {
        //which point can put
        if(kind == "cheer")
        {
            points = new string[3]{"2","4","5"};
        }
        
        if(kind == "shockwave")
        {
            points = new string[5]{"1","2","3","4","5"};
        }

        if(kind == "dexterity")
        {
            points = new string[2]{"4","5"};
        }

        if(kind == "power")
        {
            points = new string[2]{"1","3"};
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
