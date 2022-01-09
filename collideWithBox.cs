using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collideWithBox : MonoBehaviour
{
    public GameObject Victory;
    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject == Victory)
        {
            Debug.Log("Win!");
            SceneManager.LoadScene("GG");
        }

    }
}
