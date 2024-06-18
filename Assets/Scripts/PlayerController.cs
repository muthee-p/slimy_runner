
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Animator animator;
    private float moveInput;
    public CameraShake cameraShake;
    private Rigidbody _rb;
    private bool facingRight =true;
    
    public float acceleration; 
    public float accelerationTime;
    public bool isGrounded,isJumping;
    public float checkRadius;
    public LayerMask Ground;
    public int extraJumps;
    public float jumpTime;
    public float jumpSpeed;
    public int extraJumpsValue;
    private float jumpTimeCounter;
    

    void Start (){
        _rb = GetComponent<Rigidbody> ();
        extraJumps = extraJumpsValue;
    }
    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, checkRadius, Ground);
        Debug.DrawRay(transform.position, Vector3.down * checkRadius, isGrounded ? Color.green : Color.red);

        moveInput = Input.GetAxis("Horizontal");
        //animator.SetFloat ("Speed", Mathf.Abs(moveInput));

        _rb.velocity = new Vector3(Mathf.MoveTowards(_rb.velocity.x, moveInput * speed, acceleration * accelerationTime), _rb.velocity.y, _rb.velocity.z);
    


        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight * jumpSpeed, _rb.velocity.z);
                extraJumps--;
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight * jumpSpeed, _rb.velocity.z);
                jumpTimeCounter -= Time.deltaTime;
            }else
        {
            isJumping = false;
        }
    }

    // Release the jump key
    if (Input.GetKeyUp(KeyCode.Space))
    {
        isJumping = false;
    }

    // Apply gravity
    _rb.velocity += new Vector3(Vector2.up.x, Vector2.up.y, 0) * Physics2D.gravity.y * (jumpSpeed - 1) * Time.deltaTime;
   
        // Draw a line from (0, 0, 7.5) to (0, 0, 2007.5)
        Vector3 startPoint = new Vector3(600, 1, 27f);
        Vector3 endPoint = new Vector3(6000f, 800,27f) ;
        Debug.DrawLine(startPoint, endPoint, Color.red);
    
}

     
   void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("wall")){
            cameraShake.ShakeCamera();
            Invoke("StopShake", 1);
        }
    }
    void StopShake(){
        cameraShake.StopShake();
    }
}
