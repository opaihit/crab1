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
    int[] points;

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

    /*void Start()
    {
        
        if(kind == clawDamage)
        {
            points = {1, 3, 4, 5};
            cooling = 0;
            if(level == 1)
            {
                //print("All damage caused by claws +5");

            }
            if(level == 2)
            {

            }
            if(level == 3)
            {

            }

        }
        
        if(kind == clawSpeed)
        {
            if(level == 1)
            {

            }
            if(level == 2)
            {

            }
            if(level == 3)
            {

            }

        }
        
        if(kind == impactWave)
        {
            points = {1, 2, 3, 4, 5};

            if(level == 1)
            {
                cooling = 4;
                damage = 10;
                //print("causes 10 damage per 4s");

            }
            if(level == 2)
            {

            }
            if(level == 3)
            {

            }

        }
        
        if(kind == shield)
        {
            if(level == 1)
            {

            }
            if(level == 2)
            {

            }
            if(level == 3)
            {

            }

        }
        
        if(kind == cheering)
        {
            points = {1, 2, 3, 4, 5};
            cooling = 0;
            if(level == 1)
            {

            }
            if(level == 2)
            {
                //print("All damage caused by anemones x1.5");

            }
            if(level == 3)
            {

            }
        }
        
    }
    */

    // Update is called once per frame
    void Update()
    {

    }
}
