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
    private int current_element = 0;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] elements;
    private GameObject loop_fire;

    //audio
    public AudioSource PlayerAudio;
    public AudioClip CelsusAttack;

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

        // switch element
        ElementSwitching switch_element = GetComponent<ElementSwitching>();

        if (switch_element != null)
        {
            current_element = switch_element.GetCurrentElement();
        }

        if (Input.GetMouseButtonDown(0) && cooldown_timer > attack_cooldown) 
        {
            attack();

        }
        cooldown_timer += Time.deltaTime;
    }
    private void attack()
    {
        anim.SetTrigger("attack");
        PlayerAudio.clip = CelsusAttack;
        PlayerAudio.Play();
        cooldown_timer = 0;

        //Instantiate(elements, firepoint.position, firepoint.rotation);

        loop_fire = Instantiate(elements[current_element], firepoint.position, Quaternion.identity);
        loop_fire.GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
