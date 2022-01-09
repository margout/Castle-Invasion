using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        


        //peristrofh gia kinhsh deksia/aristera
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        
            
        // animations otan trexei , dhaldh otan exei input deksia h aristera
        anim.SetBool("Running", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if(wallJumpCooldown > 0.2f)
        {
            

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                //kollhmenos ston toixo
                body.gravityScale = 0;
                body.velocity = Vector2.zero; 
            }
            else 
            body.gravityScale = 5; 

             if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
            
        }
        else 
        wallJumpCooldown += Time.deltaTime; 
        
        print(onWall());
    }

    private void Jump()
    {
        if(isGrounded())
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                 body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                 //allagh kateythnshs otan o paikths thelei na fygei apo ton toixo
                 transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                  // alagh kateythhnshs otan phdaei apo ton toixo

            }
            else
             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 4, 6);


            wallJumpCooldown = 0; 
            

        }
    }

   

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        // elegxos an einai prosgeiwmenos
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
        //elegxos an einai ston toixo
    }
    public bool canAttack()
{
    return horizontalInput == 0 && isGrounded() && !onWall();
}
// pyrovolei mono otan einai prosgeiwmenos, kai otan DEN einai ston toixo

}
