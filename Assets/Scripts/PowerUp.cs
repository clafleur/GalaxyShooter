using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = sheilds
    [SerializeField]
    AudioClip audioClip;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -6.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            if (player != null)
            {
                //enable triple shot
                if (powerupID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostOn();
                }
                else if (powerupID == 2)
                {
                    player.TurnShieldOn();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
