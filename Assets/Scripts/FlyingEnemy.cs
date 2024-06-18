using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public Vector3 flyingPos;
    private Transform player;
    public bool caught=false;

    void Start()
    {
        flyingPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            transform.position = new Vector3(Mathf.MoveTowards(this.transform.position.x, player.position.x, speed * Time.deltaTime),
                Mathf.MoveTowards(this.transform.position.y, player.position.y, speed * Time.deltaTime),31f);
        }
        if (caught)
        {
            transform.position = new Vector3(this.transform.position.x + speed * Time.deltaTime,
                                               this.transform.position.y + speed * Time.deltaTime,
                                                this.transform.position.z);
            player.position = transform.position;
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            caught = true;
            
        }
     }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
