using System;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] ParticleSystem weather;
    public float lineOfSight;
    public float speed;
    public float startFollow;
    public float endFollow;
    private AudioSource sound;
    private bool isInRange;

    void Start(){
        sound = GetComponent<AudioSource>();
        weather.Play();
        if (!sound.isPlaying) sound.Play();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        float playerX = player.position.x;
        

        if (distanceFromPlayer < lineOfSight && playerX >= startFollow && playerX <= endFollow)
        {
            isInRange = true;
            StartWeather();
        }else if(distanceFromPlayer > endFollow)
            {
            weather.Stop();
            sound.Stop();
        }
    }

    private void StartWeather()
    {
        float playerX = player.position.x;
        if (isInRange)
        {
            float newX = Mathf.MoveTowards(transform.position.x, playerX, speed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
           

    }

    private  void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight) ;
    }
}
