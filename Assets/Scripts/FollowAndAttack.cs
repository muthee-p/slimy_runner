using System.Collections;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class FollowAndAttack : MonoBehaviour
{
    public float lineOfSight;
    public float attackZone;
    public float speed;
    public float addedSpeed;
    public float attackTime;
    public Animator animator;
    public AudioSource sound;
    public float startFollow;
    public float endFollow;
    public float flyingHeight;
    public bool playerSpotted;

    private float moveInput;


    private Rigidbody rb;
    public float groundCheckDistance = 1.0f;
    public float acceleration;
    public float accelerationTime;
    public bool isGrounded, isJumping;
    public float checkRadius, tartgetZ;
    public LayerMask Ground;

    private Transform player;
    private float currentAttackTime;
    private bool isAttacking;
  
    float playerX, playerY, playerZ;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sound.Play();
    }

    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
       
        bool withinFollowRange = distanceFromPlayer < lineOfSight;
        playerX = player.position.x;
        playerY = player.position.y;
        playerZ = player.position.z;

        float x = this.gameObject.transform.position.x;

        if (withinFollowRange && playerX >= startFollow && playerX <= endFollow)
        {
            playerSpotted = true;
            rb.isKinematic = false;
            if (playerX >= attackZone)
            {
                //speed += addedSpeed;
            }
        }
       
        if (playerX >= endFollow)
        {
            sound.Stop();
            rb.isKinematic = true;
           
        }
        if (x > playerX)
        {
            x += 50;
            playerSpotted=false;
            rb.isKinematic = true;
        }
       
    }
    void FixedUpdate()
    {
        if (playerSpotted)
        {
            playerX = player.position.x;
            playerY = player.position.y;
            playerZ = player.position.z;

            animator.SetTrigger("Attacking");
            isGrounded = Physics.Raycast(transform.position, Vector3.down, checkRadius, Ground);
            
            moveInput = 1;
            rb.velocity = new Vector3(Mathf.MoveTowards(rb.velocity.x, moveInput * speed, acceleration * accelerationTime),
                            rb.velocity.y, rb.velocity.z);
        
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            GameObject obstacle = collision.gameObject;
          
            obstacle.GetComponent<Collider>().enabled = false;


                StartCoroutine(ResetCollider(obstacle, 1.5f));
        }
    }

    IEnumerator ResetCollider(GameObject obstacle, float delay)
    {
        yield return new WaitForSeconds(delay);
        obstacle.GetComponent<Collider>().enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

    }
}
