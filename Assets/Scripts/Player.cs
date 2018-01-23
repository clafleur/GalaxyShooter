﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public bool canTripleShot = false;
    public bool speedBoostEnabled = false;
    public bool hasShields = false;
    public int playerLives = 3;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject playerExplosionPrefab;
    [SerializeField]
    private GameObject shieldGameObject;
    [SerializeField]
    private float fireRate = 0.25f;

    private float nextFire = 0.0f;

    private UIManager uiManager;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, 0, 0);

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (uiManager != null)
        {
            uiManager.UpdateLives(playerLives);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            PlayerShot();
        }
    }

    private void PlayerMovement()
    {
        //horizontal Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (speedBoostEnabled)
        {
            transform.Translate(Vector3.right * speed * 2.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * speed * 2.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        }

        //clamp movement of player on the y axis
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //clamp movement of player on the x axis
        if (transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (hasShields)
        {
            hasShields = false;
            shieldGameObject.SetActive(false);
            return;
        }

        playerLives--;
        uiManager.UpdateLives(playerLives);

        if (playerLives < 1)
        {
            Instantiate(playerExplosionPrefab, transform.position, Quaternion.identity);
            uiManager.ShowTitleScreen();
            gameManager.gameOver = true;

            Destroy(this.gameObject);
        }

    }

    private void PlayerShot()
    {
        if (Time.time > nextFire)
        {
            if (canTripleShot)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.95f, 0), Quaternion.identity);
            }

            nextFire = Time.time + fireRate;
        }
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostOn()
    {
        speedBoostEnabled = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    public void TurnShieldOn()
    {
        hasShields = true;
        shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speedBoostEnabled = false;
    }
}
