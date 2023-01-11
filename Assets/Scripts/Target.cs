using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    public Text HealthText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = Health.ToString();

    }
}
