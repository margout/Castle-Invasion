using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    //giati einai 10 stoixeia sthn eikona

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
        if (currenthealthBar.fillAmount == 0)
            SceneManager.LoadScene("LoseScreen");
    }

   

  
    
}