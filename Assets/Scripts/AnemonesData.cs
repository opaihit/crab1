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

    public int Thislv;
    public float cooling;
    public string[] AblePoints;

    public Sprite[] InfoSprites;//information images

    public GameObject PickupPoint;
    public GameObject OverlayPoint;//now point
    public GameObject LastPoint;//last point

    public GameObject[] LvObj;

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
            //LastPoint = null;
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
            LastPoint = OverlayPoint;
            OverlayPoint = null;
        }
        else if (other.gameObject.tag == "CanDrag")
        {
            overlap = false;
            OverlapAnemone = null;
        }

    }

    //all data of all anemones
    //give name and level to define a anemone

    void Start()
    {
        //set original pick up point, but cant find parent of itself, cant "self".transform.parent
        //OnTriggerEnter(other); //cant run in start, no declaration of "other"
        //PickupPoint = OverlayPoint;
        
        //which point can put
        if (name == "cheer")
        {
            AblePoints = new string[6] { "1", "2", "3", "4", "5", "new" };
        }

        else if (name == "shockwave")
        {
            AblePoints = new string[6] { "1", "2", "3", "4", "5", "new" };
        }

        else if (name == "dexterity")
        {
            AblePoints = new string[3] { "4", "5", "new" };
        }

        else if (name == "power")
        {
            AblePoints = new string[3] { "1", "3", "new" };
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
