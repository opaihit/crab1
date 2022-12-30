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
    public bool collided;

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        //Debug.DrawRay(Camera.main,)
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs that hited by the ray

        if (Input.GetMouseButton(0))//when hold mouse left down
        {
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
                if (DragObj.GetComponent<AnemonesData>().OverlayPoint)//when dragging sth
                {
                    GameObject parentPoint = DragObj.GetComponent<AnemonesData>().OverlayPoint;
                    DragObj.transform.position = parentPoint.transform.position;
                    DragObj.transform.SetParent(parentPoint.transform, true);
                }
                else
                {
                    DragObj.transform.position = DragObj.GetComponent<AnemonesData>().LastPoint.transform.position;
                }

                DragObj = null;
            }

        }

    }
}
