using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    
    
    private Animator anim; 
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; 

    private void Awake()
    {
        //epitrepei prosvasi sto Animator tou Player
        anim = GetComponent<Animator>(); 
        playerMovement = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        // ypodoxh button (mousebutton(0)= aristero click)
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
         Attack();

         cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0 ;

        fireballs[FindFireball()].transform.position = firePoint.position; 
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int FindFireball()
    {
        //energopoiei to animation
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
