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
    public GameObject points;

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

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        //Debug.DrawRay(Camera.main,)
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs hited by the ray

        //show detials of on left when hover obj
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
                            DragObj = hitObj;//change dragobj to the hited obj

                            DragObj.GetComponent<AnemonesData>().LastPoint = DragObj.transform.parent.gameObject;
                            //draging obj has no parent
                            DragObj.transform.SetParent(null, true);//clean parent
                            Instantiate(PickupBubble, DragObj.transform.position, Quaternion.identity);
                        }
                        //if overlay with other obj show compare info on right
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
                        //show which point can put
                        int pointNumber = points.transform.childCount;
                        canputpoints = DragObj.GetComponent<AnemonesData>().points;
                        
                        if(findOutNumber < canputpoints.Length)
                        {
                            for (int m = 0; m < canputpoints.Length; m++)
                            {
                                for (int n = 0; n < pointNumber; n++)
                                {
                                    if (points.transform.GetChild(n).name == canputpoints[m])
                                    {
                                        findOutNumber += 1;
                                        Debug.Log(findOutNumber);
                                        //Debug.Log(points.transform.GetChild(n).name);
                                        //if(no) set parent
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
                
                //put down
                if (DragObj.GetComponent<AnemonesData>().OverlayPoint)//when anemone on a point
                {
                    int PointChildNum = DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.childCount;
                    for (int s = 0; s < canputpoints.Length; s++)
                    {
                        //empty and can put, put
                        if (PointChildNum == 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s])
                        {
                            GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                            DragObj.transform.position = parentPoint.transform.position;
                            DragObj.transform.SetParent(parentPoint.transform, true);
                            Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
                        }
                        //not empty but can put
                        if (PointChildNum != 0 && DragObj.GetComponent<AnemonesData>().OverlayPoint.name == canputpoints[s])
                        {
                            //prepare for exchange
                            GameObject DragObjLastPoint = DragObj.GetComponent<AnemonesData>().LastPoint;//now a de last p
                            GameObject Overlay_Point = DragObj.GetComponent<AnemonesData>().OverlayPoint;//now a & ex a de now p
                            GameObject OldObj = Overlay_Point.transform.GetChild(0).gameObject;//ex a in now p
                            //GameObject OldObj = DragObj.GetComponent<AnemonesData>().OverlapAnemone;
                            ex_canputpoints = OldObj.GetComponent<AnemonesData>().points;//ex a de can put p

                            //can exchange, exchange
                            for (int p = 0; p < ex_canputpoints.Length; p++)
                            {
                                if (DragObjLastPoint.name == ex_canputpoints[p])//ex a can put to now a de last p
                                {
                                    //Debug.Log(points.transform.GetChild(n).name);
                                    OldObj.transform.position = DragObjLastPoint.transform.position;
                                    OldObj.transform.SetParent(DragObjLastPoint.transform, true);
                                    
                                    DragObj.transform.SetParent(Overlay_Point.transform, true);
                                    DragObj.transform.position = Overlay_Point.transform.position;

                                    Instantiate(PutDownStar, DragObj.transform.position, Quaternion.identity);
                                    Instantiate(PutDownStar, OldObj.transform.position, Quaternion.identity);                                   
                                }
                            }
                        }
                        else//not on point and other qingkuang, put back
                        {
                            DragObj.transform.position = DragObj.GetComponent<AnemonesData>().LastPoint.transform.position;
                        }
                    }
                    /*else//not empty, exchange
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
                    }*/
                }

                //clean
                DragObj = null;
                RightInfo_Back.sprite = null;
                RightInfo_Back.enabled = false;
            }

        }

    }
}
