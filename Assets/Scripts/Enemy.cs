using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject enemyExplosionPrefab;
    [SerializeField]
    private AudioClip audioClip;

    private UIManager uiManager;
    private GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMovement();

        if (gameManager.gameOver && transform.position.y <= -7.0f)
        {
            Destroy(this.gameObject);
        }

    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
            uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
