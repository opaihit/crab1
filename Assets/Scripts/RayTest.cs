using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayTest : MonoBehaviour
{
    
    public AnemonesData data;
    public GameObject DragObj;
    public GameObject points;
    public string kind;
    public string level;
    //public Image background;

    public GameObject LvUpCard;

    public Image LeftInfo_Back, RightInfo_Back;

    public GameObject HoverOverObj;
    public int HoverObjNum;
    public GameObject OverlapOverObj;

    public GameObject PickupBubble;
    public GameObject PutDownStar;

    public GameObject CanPutLight;
    public GameObject newEffect;
    int findOutNumber = 0;

    public string[] canputpoints;//can put p of drag a
    public string[] ex_canputpoints;//can put point of ex a



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
            RightInfo_Back.sprite = null;
            RightInfo_Back.enabled = false;
        }
        if (OverlapOverObj && !RightInfo_Back.sprite)
        {
            RightInfo_Back.enabled = true;
            RightInfo_Back.sprite = OverlapOverObj.GetComponent<AnemonesData>().PickedSprite();
        }

    }

    //exchange two anemones
    private void ExchangeItemPos(GameObject PutTo_Point, GameObject OldObj, GameObject DragObjLastPoint)
    {
        Debug.Log(PutTo_Point.name);
        //DragObj.transform.position = PutTo_Point.transform.position;//bug
        DragObj.transform.position = OldObj.GetComponent<AnemonesData>().OverlayPoint.transform.position; //bug
        DragObj.transform.position = OldObj.GetComponent<AnemonesData>().PickupPoint.transform.position; //bug
        DragObj.transform.SetParent(PutTo_Point.transform, true);

        OldObj.transform.position = DragObjLastPoint.transform.position;
        OldObj.transform.SetParent(DragObjLastPoint.transform, true);

        Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
        Instantiate(PutDownStar, OldObj.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        //Debug.DrawRay(Camera.main,)
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs hited by the ray

        //show detials of anemone on left when hover obj
        if (hits.Length > 0)//hit sth
        {
            HoverObjNum = 0;
            GameObject HoverObj = null;//hited anemone

            for (int i = 0; i < hits.Length; i++)//look every hits
            {
                GameObject hitObj = hits[i].collider.gameObject;

                if (hitObj.tag == "CanDrag")//count hited anemones
                {
                    HoverObjNum++;
                    HoverObj = hitObj;
                }
                
            }

            if (HoverObjNum == 0)//hit no anmone
            {
                HoverOverObj = null;
            }

            if (HoverObjNum == 1)//hit 1 anmone
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
                        //pick
                        if (!DragObj)//no anemone is dragging, then can darg obj 
                        {
                            DragObj = hitObj;//drag hited obj

                            DragObj.GetComponent<AnemonesData>().LastPoint = DragObj.transform.parent.gameObject;
                            DragObj.GetComponent<AnemonesData>().PickupPoint = DragObj.transform.parent.gameObject;
                            //draging obj has no parent
                            DragObj.transform.SetParent(null, true);//clean parent
                            Instantiate(PickupBubble, DragObj.transform.position, Quaternion.identity);//pick up bubble
                        }

                        //if overlap with other obj show compare info on right, or clean right info
                        if (DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.childCount > 0)
                        {
                            CompareObj = DragObj.GetComponent<AnemonesData>().OverlapAnemone;
                            OverlapOverObj = CompareObj;
                            ShowInfoR();
                        }
                        else if (DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.childCount == 0)
                        {
                            CompareObj = null;
                            OverlapOverObj = null;
                            ShowInfoR();
                        }

                        //show which point can put
                        int pointNumber = points.transform.childCount;
                        canputpoints = DragObj.GetComponent<AnemonesData>().AblePoints;
                        if (findOutNumber < canputpoints.Length)
                        {
                            for (int m = 0; m < canputpoints.Length; m++)
                            {
                                for (int n = 0; n < pointNumber; n++)
                                {
                                    if (points.transform.GetChild(n).name == canputpoints[m])
                                    {
                                        findOutNumber += 1;
                                        //Debug.Log(findOutNumber);
                                        //Debug.Log(points.transform.GetChild(n).name);
                                        //show can put light
                                        GameObject newEffect = Instantiate(CanPutLight, points.transform.GetChild(n).transform.position, Quaternion.identity);
                                        newEffect.name = "newEffect";
                                        newEffect.tag = "LightEffect";
                                    }
                                }
                            }
                        }

                    }

                    //up
                    if (hitObj.name == "UpperFloor")
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
                //turn off all can_put_lights
                GameObject[] LightEffects = GameObject.FindGameObjectsWithTag("LightEffect");
                foreach (GameObject obj in LightEffects)
                {
                    Destroy(obj);
                }

                //clear find_out_can_put_number
                findOutNumber = 0;

                //on point, put down
                if (DragObj.GetComponent<AnemonesData>().OverlayPoint)
                {
                    //whether there is an anemone on overlay point
                    int PointChildNum = DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.childCount;

                    //different conditions of put down
                    for (int s1 = 0; s1 < canputpoints.Length; s1 ++)
                    {
                        //empty and can put, put
                        if (PointChildNum == 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s1])
                        {
                            GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                            DragObj.transform.position = parentPoint.transform.position;
                            DragObj.transform.SetParent(parentPoint.transform, true);
                            //show put down star
                            GameObject putEffect = Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
                            putEffect.name = "putEffect";
                            putEffect.tag = "StarEffect";
                            //Debug.Log("put on empty point");
                        }
                        break;
                    }

                    for (int s2 = 0; s2 < canputpoints.Length; s2 ++)
                    {
                        //empty but cant put, put back
                        if (PointChildNum == 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name != canputpoints[s2])
                        {
                            DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                        }
                        break;
                    }

                    for (int s3 = 0; s3 < canputpoints.Length; s3 ++)
                    {
                        //not empty but can put
                        if (PointChildNum != 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s3])
                        {
                            //prepare for exchange
                            GameObject DragObjLastPoint = DragObj.GetComponent<AnemonesData>().PickupPoint;//now a de pick up p
                            GameObject Overlay_Point = DragObj.GetComponent<AnemonesData>().OverlayPoint;//now a & ex a de now p
                            //GameObject OldObj = DragObj.GetComponent<AnemonesData>().OverlapAnemone;
                            GameObject OldObj = Overlay_Point.transform.GetChild(0).gameObject;//ex a in now p
                            //GameObject PutTo_Point = OldObj.transform.parent.gameObject;//may bug
                            GameObject PutTo_Point = OldObj.GetComponent<AnemonesData>().OverlayPoint;

                            ex_canputpoints = OldObj.GetComponent<AnemonesData>().AblePoints;//ex a de can put p

                            bool CanExchange = false;

                            //can exchange?
                            for (int p = 0; p < ex_canputpoints.Length; p++)
                            {
                                if (DragObjLastPoint.name == ex_canputpoints[p])//ex a can put to now a de last p
                                {
                                    CanExchange = true;
                                    Debug.Log("can exchange");                                
                                }   
                            } //can work

                            //can exchange, exchange
                            if (CanExchange == true)
                            {
                                ExchangeItemPos(PutTo_Point, OldObj, DragObjLastPoint);
                            }
                            //cant exchange, put back
                            else if(CanExchange == false)
                            {
                                DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                            }
                            break;
                        }
                    }

                    for (int s4 = 0; s4 < canputpoints.Length; s4 ++)
                    {
                        //not empty and cant put, put back
                        if(PointChildNum != 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name != canputpoints[s4])
                        {
                            DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                        }
                        break;
                    }

                    /*
                    //compare name of overlay point and can put point
                    for (int s = 0; s < canputpoints.Length; s++)
                    {
                        //empty and can put, put
                        if (PointChildNum == 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s])
                        {
                            GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                            DragObj.transform.position = parentPoint.transform.position;
                            DragObj.transform.SetParent(parentPoint.transform, true);
                            //show put down star
                            GameObject putEffect = Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
                            putEffect.name = "putEffect";
                            putEffect.tag = "StarEffect";
                            //Debug.Log("put on empty point");
                        }
                        //empty but cant put, put back
                        else if (PointChildNum == 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name != canputpoints[s])
                        {
                            DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                        }

                        //not empty but can put
                        else if (PointChildNum != 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s])
                        {
                            //prepare for exchange
                            GameObject DragObjLastPoint = DragObj.GetComponent<AnemonesData>().PickupPoint;//now a de pick up p
                            GameObject Overlay_Point = DragObj.GetComponent<AnemonesData>().OverlayPoint;//now a & ex a de now p
                            //GameObject OldObj = DragObj.GetComponent<AnemonesData>().OverlapAnemone;
                            GameObject OldObj = Overlay_Point.transform.GetChild(0).gameObject;//ex a in now p
                            GameObject PutTo_Point = OldObj.transform.parent.gameObject;

                            ex_canputpoints = OldObj.GetComponent<AnemonesData>().AblePoints;//ex a de can put p

                            bool CanExchange = false;

                            //can exchange?
                            for (int p = 0; p < ex_canputpoints.Length; p++)
                            {
                                if (DragObjLastPoint.name == ex_canputpoints[p])//ex a can put to now a de last p
                                {
                                    CanExchange = true;
                                }   
                            }
                            //can exchange, exchange
                            if (CanExchange == true)
                            {
                                ExchangeItemPos(PutTo_Point, OldObj, DragObjLastPoint);
                                    //Debug.Log(points.transform.GetChild(n).name);
                                    Debug.Log(PutTo_Point.name);
                                    DragObj.transform.position = PutTo_Point.transform.position;
                                    DragObj.transform.SetParent(PutTo_Point.transform, true);

                                    OldObj.transform.position = DragObjLastPoint.transform.position;
                                    OldObj.transform.SetParent(DragObjLastPoint.transform, true);

                                    Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
                                    Instantiate(PutDownStar, OldObj.transform.position, Quaternion.identity);
                                    
                            }
                            //cant exchange, put back
                            else if(CanExchange == false)
                            {
                                DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                            }
                        }
                        //not empty and cant put, put back
                        else if(PointChildNum != 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name != canputpoints[s])
                        {
                            DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                        }
                    }
                    */

                }

                //not on point, putback
                else if(!DragObj.GetComponent<AnemonesData>().OverlayPoint)
                {
                    DragObj.transform.position = DragObj.GetComponent<AnemonesData>().PickupPoint.transform.position;
                }

                //clean
                DragObj = null;
                RightInfo_Back.sprite = null;
                RightInfo_Back.enabled = false;
                //DragObj.GetComponent<AnemonesData>().PickupPoint = null;

                //close put down stars
                GameObject[] StarEffects = GameObject.FindGameObjectsWithTag("StarEffect");
                foreach (GameObject obj in StarEffects)
                {
                    Destroy(obj);
                }
            }

        }

    }
}
