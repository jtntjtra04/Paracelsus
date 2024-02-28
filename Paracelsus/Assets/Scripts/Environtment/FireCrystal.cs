using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystal : MonoBehaviour
{
    [SerializeField] private GameObject Crystal;
    [SerializeField] private GameObject Door;

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
        if (collision.CompareTag("Fire")) // if crystal got hit by wind element
        {
            if (Door != null)
            {
                Destroy(Door); // the door is destroy
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
