using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public AnemonesData data;
    public GameObject DragObj;
    public string kind;
    public int level;

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        //Debug.DrawRay(Camera.main,)
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs that hited by the ray

        //show detials
        /*
        if (hits.Length > 0)//hit sth
            {
                for (int i = 0; i < hits.Length; i++)//look every hits
                {
                    GameObject hitObj = hits[i].collider.gameObject;

                    if (hitObj.tag == "CanDrag")
                    {
                        kind = hitObj.GetComponent<AnemonesData>().kind;
                        level = hitObj.GetComponent<AnemonesData>().level;
                        //Canvas.transform.Find(kind + level).SetActive(true);
                        
                        
                    }
                }
            }
        */

        if (Input.GetMouseButton(0))//when hold mouse left down
        {
            //pick up
            if (hits.Length > 0)//hit sth
            {
                for (int i = 0; i < hits.Length; i++)//look every hits
                {
                    GameObject hitObj = hits[i].collider.gameObject;

                    if (hitObj.tag == "CanDrag")
                    {
                        if (!DragObj)//no obj is dragging, then can darg sth 
                        {
                            DragObj = hitObj;//change dragobj to the hited obj
                            DragObj.GetComponent<AnemonesData>().LastPoint = DragObj.transform.parent.gameObject;
                        }
                    }

                    if (hitObj.name == "UpperFloor")
                    {
                        Vector3 newposition = hits[i].point;
                        if (DragObj)//when dragobj is not null
                            DragObj.transform.position = newposition;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))//when loose mouse left
        {
            if (DragObj)
            {
                //put to point
                //empty point
                if (DragObj.GetComponent<AnemonesData>().OverlayPoint && !DragObj.GetComponent<AnemonesData>().OverlayPoint.transform.GetChild(0))
                {
                    GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                    DragObj.transform.position = parentPoint.transform.position;
                    DragObj.transform.SetParent(parentPoint.transform, true);
                }

                //not empty point
                else if(DragObj.GetComponent<AnemonesData>().OverlayPoint)
                {
                    GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                    DragObj.transform.position = parentPoint.transform.position;
                    DragObj.transform.SetParent(parentPoint.transform, true);
                    //exchange dragobj to ojb in point
                    DragObj = DragObj.GetComponent<AnemonesData>().OverlayPoint.gameObject.GetChild(0).transform.GameObject;
                }
                
                else//put back
                {
                    DragObj.transform.position = DragObj.GetComponent<AnemonesData>().LastPoint.transform.position;
                }

                DragObj = null;
            }

        }

    }
}
