using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnemonesData : MonoBehaviour
{
    public string code;
    public string kind;
    public string level;
    public float damage;
    public float cooling;
    int[] points;

    public Image Lv1, Lv2, Lv3;

    public GameObject OverlayPoint;
    public GameObject LastPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "points")
        {
            OverlayPoint = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "points" && OverlayPoint == other.gameObject)
        {
            OverlayPoint = null;
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
