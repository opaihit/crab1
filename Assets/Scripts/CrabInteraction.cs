using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabInteraction : MonoBehaviour
{
    public GameObject Crab;
    public GameObject clawL, clawR;
    public Rigidbody RigidbodyL, RigidbodyR;
    public Animator Animator;
    public bool clawsAction = false;
    public bool hitClaws = false;

    void Start()
    {
        RigidbodyL = clawL.GetComponent<Rigidbody>();
        RigidbodyR = clawR.GetComponent<Rigidbody>();
        Animator = Crab.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;//ojbs hited by the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//ray from camera to mouse
        
        hits = Physics.RaycastAll(ray, Mathf.Infinity);//record all objs hited by the ray

        //show detials of on left when hover obj
        if (hits.Length > 0)//hit sth
        {
            for (int i = 0; i < hits.Length; i++)//look every hits
            {
                GameObject hitObj = hits[i].collider.gameObject;

                if (hitObj.tag == "claws")//hit claws
                {
                    //Debug.Log("hited claws, if click then play animation");
                    clawsAction = true;
                    hitClaws = true;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (clawsAction == true && hitClaws == true)
            {
                Animator.SetTrigger("clickClaw");
            }
            
        }
    }
}
