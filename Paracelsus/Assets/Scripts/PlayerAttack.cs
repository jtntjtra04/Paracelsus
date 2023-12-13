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
    }
}
