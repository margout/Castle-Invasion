using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    //get gia na paroume to current health apo to HealthBar script
    // private set gia na to xrishmopoihsoume mono se auto to script
    private Animator anim; 
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
         if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage,0, startingHealth);
        
        if (currentHealth > 0) 
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);

        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

            foreach (Behaviour component in components)
                    component.enabled = false;


            dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }

            

        }
        

    }
    public void AddHealth(float _value)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value,0, startingHealth);
        }
        // h vohtheia zwhs 
     private IEnumerator Invunerability()
     {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numOfFlashes; i++ )
        {
            invulnerable = true;
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;

     }
     private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    //gia na svinetai teleiws meta to animation o paikths  h o enemy

}
