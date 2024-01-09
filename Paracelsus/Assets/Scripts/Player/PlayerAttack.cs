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
    [SerializeField] private GameObject[] elements;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && cooldown_timer > attack_cooldown) 
        {
            attack();
        }
        cooldown_timer += Time.deltaTime;
    }
    private void attack()
    {
        cooldown_timer = 0;

        elements[LoopFire()].transform.position = firepoint.position;
        elements[LoopFire()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        anim.SetTrigger("attack");
    }
    private int LoopFire()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (!elements[i].activeInHierarchy) 
                return i;
        }
        return 0;
    }
}
