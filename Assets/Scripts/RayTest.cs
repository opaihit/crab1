using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public AnemonesData data;
    public GameObject DragObj;

    public string kind;
    public string level;
    //public Image background;

    public Image LeftInfo_Back, RightInfo_Back;

    public GameObject HoverOverObj;
    public int HoverObjNum;
    public GameObject OverlapOverObj;
    //public Sprite cheer1;
    //public GameObject CanvasObj;
    public GameObject PickupBubble;


    //***no use le*** show details de method
    /*public void changeimage()
    {
        //background.sprite = Canvas.transform.Find(kind + level);***x***
        //GameObject.Find("xxxxxx");
        //gameObject.transform.Find("  xxxx");
    }*/

    //Information of selected obj
    public void ShowInfoL()
    {
        if (HoverOverObj == null && LeftInfo_Back.sprite)
        {
            //LeftInfo_Back.sprite = GameObject.Find("LeftDetails").transform.sprite;
            LeftInfo_Back.sprite = null;
            LeftInfo_Back.enabled = false;
        }
        if (HoverOverObj && !LeftInfo_Back.sprite)
        {
            LeftInfo_Back.enabled = true;
            LeftInfo_Back.sprite = HoverOverObj.GetComponent<AnemonesData>().PickedSprite();
        }

    }
    //Comparison information
    public void ShowInfoR()
    {
        if (OverlapOverObj == null && RightInfo_Back.sprite)
        {
            RightInfo_Back.sprite = null;//problem: cant get background, only white image
        }
        if (OverlapOverObj && !RightInfo_Back.sprite)
        {
            RightInfo_Back.sprite = OverlapOverObj.GetComponent<AnemonesData>().PickedSprite();
        }

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        //Debug.DrawRay(Camera.main,)
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs hited by the ray

        //show detials of hover obj on left
        if (hits.Length > 0)//hit sth
        {
            HoverObjNum = 0;
            GameObject HoverObj = null;

            for (int i = 0; i < hits.Length; i++)//look every hits
            {
                GameObject hitObj = hits[i].collider.gameObject;

                if (hitObj.tag == "CanDrag")
                {
                    HoverObjNum++;
                    HoverObj = hitObj;
                }
            }

            if (HoverObjNum == 0)
            {
                HoverOverObj = null;
            }

            if (HoverObjNum == 1)
            {
                HoverOverObj = HoverObj;
            }
        }
        ShowInfoL();

        //when hold mouse left down
        if (Input.GetMouseButton(0))
        {
            //pick up
            if (hits.Length > 0)//hit sth
            {
                GameObject CompareObj = null;
                for (int i = 0; i < hits.Length; i++)//look every hits
                {
                    GameObject hitObj = hits[i].collider.gameObject;

                    if (hitObj.tag == "CanDrag")//find hited anemone
                    {
                        if (!DragObj)//no obj is dragging, then can darg obj 
                        {
                            DragObj = hitObj;//change dragobj to the hited obj

                            DragObj.GetComponent<AnemonesData>().LastPoint = DragObj.transform.parent.gameObject;
                            //draging obj no parent
                            DragObj.transform.SetParent(null, true);
                            Instantiate(PickupBubble, DragObj.transform.position, Quaternion.identity);

                        }
                        //if overlay with other obj show compare info on right
                        //1
                        if (DragObj.GetComponent<AnemonesData>().overlap == true)
                        {
                            CompareObj = DragObj.GetComponent<AnemonesData>().OverlapAnemone;
                            OverlapOverObj = CompareObj;
                            ShowInfoR();
                        }
                        else if (DragObj.GetComponent<AnemonesData>().overlap == false)
                        {
                            CompareObj = null;
                            OverlapOverObj = null;
                            ShowInfoR();
                        }
                        //2
                        /*if(HoverObjNum == 2)
                        {
                            CompareObj = hits[i + 1].collider.gameObject;
                            OverlapOverObj = CompareObj;
                            ShowInfoR();
                        }*/




                    }

                    if (hitObj.name == "UpperFloor")//up
                    {
                        Vector3 newposition = hits[i].point;
                        if (DragObj)//when dragobj is not null
                            DragObj.transform.position = newposition;
                    }
                }
            }
        }

        //when loose mouse left
        if (Input.GetMouseButtonUp(0))
        {
            if (DragObj)
            {
                //put to point
                if (DragObj.GetComponent<AnemonesData>().OverlayPoint)
                {
                    int PointChildNum = DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.childCount;
                    if (PointChildNum == 0)//empty, put
                    {
                        GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                        DragObj.transform.position = parentPoint.transform.position;
                        DragObj.transform.SetParent(parentPoint.transform, true);
                    }

                    else//not empty, exchange
                    {
                        GameObject DragObjLastPoint = DragObj.GetComponent<AnemonesData>().LastPoint;
                        GameObject Overlay_Point = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                        GameObject OldObj = Overlay_Point.transform.GetChild(0).gameObject;

                        OldObj.transform.position = DragObjLastPoint.transform.position;
                        OldObj.transform.SetParent(DragObjLastPoint.transform, true);

                        DragObj.transform.position = Overlay_Point.transform.position;
                        DragObj.transform.SetParent(Overlay_Point.transform, true);

                        //DragObj.transform.position = DragObj.GetComponent<AnemonesData>().LastPoint.transform.position;
                        //DragObj.transform.SetParent(DragObj.GetComponent<AnemonesData>().LastPoint.transform, true);
                    }
                }

                else//not on point, put back
                {
                    DragObj.transform.position = DragObj.GetComponent<AnemonesData>().LastPoint.transform.position;
                }

                DragObj = null;
                RightInfo_Back.sprite = null;
            }

        }

    }
}
