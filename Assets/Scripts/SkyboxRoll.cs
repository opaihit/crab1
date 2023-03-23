using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyboxRoll : MonoBehaviour
{
    public float rot = 0;
    public Skybox sky;
    public GameObject SkyCamera;

    void Start()
    {
        sky = SkyCamera.GetComponent<Skybox>();
    }

    void Update ()
    {
        rot += 2 * Time.deltaTime;
        rot %= 360;
        sky.material.SetFloat ("_Rotation", rot);
    }
}

