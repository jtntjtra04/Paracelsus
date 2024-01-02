using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameController playerHealth;
    [SerializeField] private Image totalHP;
    [SerializeField] private Image currHP;

    void Start()
    {
        totalHP.fillAmount = playerHealth.currHP / 10;
    }

    void Update()
    {
        currHP.fillAmount = playerHealth.currHP / 10;
    }
}
