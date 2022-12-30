using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeExample : MonoBehaviour
{
    // Start is called before the first frame update
    public float showtime;
    public float countDownTime;
    public GameObject Cube;
    void Start()
    {
        // Time.time
        // Time.deltaTime

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= countDownTime && Cube.activeInHierarchy)
        {
            Debug.Log("turn off cube");
            Cube.SetActive(false);
        }
    }
}
