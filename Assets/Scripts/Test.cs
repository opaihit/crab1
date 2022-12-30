using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    public int newint;
    public float newfloat;
    public bool newbool;
    public string newstring; //newSting
    public GameObject newgameobj;


    private void Awake()
    {

    }
    void Start()
    {
        int number = newint + (int)newfloat;
        int other_number = (int)((float)newint + newfloat);
        Debug.Log(number + " " + other_number);
        Debug.Log("newint is " + newint);
        print("newbool is " + newbool);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (newgameobj.activeSelf)
                newgameobj.SetActive(false);
            else
                newgameobj.SetActive(true);
        }

    }

    private void FixedUpdate()
    {

    }
}
