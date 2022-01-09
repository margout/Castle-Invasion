using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header ("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered; //otan energopoieitai
    private bool active; //otan mporei na kanei damage

    private Health playerHealth;

     private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

       private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
                StartCoroutine(ActivateFiretrap()); 
                // energopoihsh

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
                // kanei damage
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null;
    }

    private IEnumerator ActivateFiretrap()
    { 
        triggered = true;
        spriteRend.color = Color.red; //to trap ginetai kokkino
        //perimenei X lepta , energopoiei to trap
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white; //ginetai pali lefko
        active = true;
        anim.SetBool("activated", true);
        //perimenei X lepta apenergopoiei to trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
