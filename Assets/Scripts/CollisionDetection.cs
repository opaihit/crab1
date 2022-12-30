using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public bool isCollided = false;
    
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        isCollided = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isCollided = false;
    }
}
