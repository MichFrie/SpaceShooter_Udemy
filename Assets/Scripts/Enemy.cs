using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float enemySpeed = 4.0f;

    private Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
        if(transform.position.y < -6.0f)
        {
            float randomX = Random.Range(-9.4f, 9.4f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        
        if(other.gameObject.tag == "Player")
        {
           Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Laser")
        {
            
            Destroy(other.gameObject);
            
            if(player != null)
            {
                player.CountScore(10);
            }

            Destroy(this.gameObject);      
        }
    }
}
