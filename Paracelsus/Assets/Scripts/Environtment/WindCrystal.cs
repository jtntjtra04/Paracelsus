using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WindCrystal : MonoBehaviour
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
        if (collision.CompareTag("Wind"))
        {
            if (Door != null)
            {
                Destroy(Door);
            }

            polygon_collider.enabled = false;
            anim.SetTrigger("break");
        }
    }
    private void CrystalBreak()
    {
        Crystal.SetActive(false);
    }
}
