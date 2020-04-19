using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float powerUpFloat = 3.0f;
    [SerializeField]
    private int powerUpId;
    void Start()
    {
        
    }

    void Update()
    {
        PowerUpMovement();
    }

    void PowerUpMovement()
    {
        transform.Translate(Vector3.down * powerUpFloat * Time.deltaTime);
        if(transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                switch (powerUpId)
                {
                    case 0: player.TripleShotActive(); break;
                    case 1: player.SpeedBoostActive(); break;
                    case 2: player.ShieldActive(); break;
                    default: Debug.Log("Default Value"); break;

                }
            }
            Destroy(this.gameObject);
        }    
    }
}
