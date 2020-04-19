using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    private float speedMultiplier = 2;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private GameObject shieldVisualizer;

    private UiManager uimanager;

    private bool tripleShotIsActive = false;
    private bool speedBoostIsActive = false;
    private bool shieldIsActive = false;

    Spawn_Manager spawn_Manager;

    [SerializeField]
    private int score;
void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        uimanager = GameObject.Find("Canvas").GetComponent<UiManager>();
        
        if(spawn_Manager == null)
        {
            Debug.LogError("Spawnmanager not created");
        }
        if(uimanager == null)
        {
            Debug.LogError("UI-Manager not created");
        }
    }

    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && tripleShotIsActive == false)
        {
            canFire = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
        }else if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && tripleShotIsActive == true)
        {
            canFire = Time.time + fireRate;
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

       transform.Translate(direction * speed * Time.deltaTime);
        
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        else if (transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
        }
        else if (transform.position.y <= -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
    }
    public void Damage()
    {
        if(shieldIsActive == true)
        {
            shieldIsActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            lives--;
            uimanager.UpdateLives(lives);
            if(lives < 1)
            {
                spawn_Manager.OnPlayerDeath();
                Destroy(this.gameObject);
                uimanager.DisplayGameOverMessage();
            }
        }

    }

    public void TripleShotActive()
    {
        tripleShotIsActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        tripleShotIsActive = false;
    }

    public void SpeedBoostActive()
    {
        speed = 10;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        speed = 5;
    }

    public void ShieldActive()
    {
        shieldIsActive = true;
        StartCoroutine(ShieldPowerDownRoutine());
        shieldVisualizer.SetActive(true);
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        shieldIsActive = false;
    }

    public void CountScore(int points)
    {
         score = score + points;
        uimanager.UpdateScore(score);     
    }
}
