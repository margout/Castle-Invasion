using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle")]
    [SerializeField] private float idleDuration; 
    private float idleTimer; 

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if(movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                //allagh kateythhnshs
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                //allagh kateythhnshs
                DirectionChange();
            }
        }
    }
    private void DirectionChange()
        {
            anim.SetBool("moving", false);
            idleTimer += Time.deltaTime;
             if(idleTimer > idleDuration)
                movingLeft = !movingLeft;
        }


    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs (initScale.x) * _direction, initScale.y, initScale.z);
        //Mathf.Abs giati to x mporei na einai thetiko h kai arnhtiko
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime* _direction * speed, enemy.position.y , enemy.position.z);
    }
}
