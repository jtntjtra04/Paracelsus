using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Image totalPotion;
    [SerializeField] private Image currPotion;

    void Start()
    {
        UpdatePotionUI();
    }

    void Update()
    {
        UpdatePotionUI();
    }
    void UpdatePotionUI()
    {
        totalPotion.fillAmount = gameController.maxPotion;
        currPotion.fillAmount = gameController.hpPotion / 5f;
    }
}