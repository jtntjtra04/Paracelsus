using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WindCrystal : MonoBehaviour
{
    [SerializeField] private GameObject Crystal;
    [SerializeField] private GameObject Door;
    [SerializeField] private Animator door_animator;

    private PolygonCollider2D polygon_collider;
    private Animator anim;

    private void Awake()
    {
        polygon_collider = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind")) // if crystal got hit by wind element
        {
            if (Door != null)
            {
                if(door_animator != null)
                {
                    door_animator.SetTrigger("Open");
                }
                Collider2D door_collider = Door.GetComponent<Collider2D>();
                if (door_collider != null)
                {
                    door_collider.enabled = false;
                }
            }

            polygon_collider.enabled = false;
            anim.SetTrigger("break"); // the crystal also break
        }
    }
    private void CrystalBreak()
    {
        Crystal.SetActive(false);
    }
}
