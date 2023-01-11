using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack01 : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage, cooldown;
    public GameObject TargetObj;
    private float lastAttackTime = 0;
    public Animator Anim;

    private void Attack()
    {
        Anim.SetTrigger("Attack");
        Invoke("Damage", .4f);
    }

    private void Damage()
    {
        TargetObj.GetComponent<Target>().Health -= damage;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastAttackTime + cooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }
}
