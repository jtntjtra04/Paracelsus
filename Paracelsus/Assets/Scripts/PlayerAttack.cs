using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;
    [SerializeField] private float attack_cooldown;
    private float cooldown_timer = 100;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldown_timer > attack_cooldown) 
        {
            attack();
        }
        cooldown_timer += Time.deltaTime;
    }
    private void attack()
    {
        anim.SetTrigger("attack");
        cooldown_timer = 0;

        fireballs[LoopFire()].transform.position = firepoint.position;
        fireballs[LoopFire()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int LoopFire()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy) 
                return i;
        }
        return 0;
    }
}
