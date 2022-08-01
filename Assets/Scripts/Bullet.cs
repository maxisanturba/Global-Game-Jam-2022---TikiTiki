using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float angle;
    public int playerColor; //1 = Blue 2 = Red
    public GameController gameController;
    public Rigidbody rb;
    public GameObject target;
    public GameObject rockBlock;
    public GameObject rockParticles;
    public GameObject ballsParticles;

    public AudioSource audioSource;
    public AudioClip rockClip;
    public AudioClip destroyClip;

    [SerializeField] private Player_FSM playerScript;
    
    private void OnEnable()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();

        switch (playerColor)
        {
            case 1:
                playerScript = GameObject.FindWithTag("PlayerBlue").GetComponent<Player_FSM>();
                target = GameObject.FindWithTag("BlueTarget");
                break;
            case 2:
                playerScript = GameObject.FindWithTag("PlayerRed").GetComponent<Player_FSM>();
                target = GameObject.FindWithTag("RedTarget");
                break;
        }
    }
    private void Start()
    {
        if(playerScript.sliderPower.value <= 0.25f)
            speed = 3.75f;
        else
            speed *= playerScript.sliderPower.value;

        var transform1 = transform;
        transform1.right = (target.transform.position - transform1.position).normalized;
        rb.AddForce(transform1.right * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameController.gameOver) return;
        switch (playerColor)
        {
            case 1:
            {
                if (collision.gameObject.CompareTag("ProyectileRed"))
                {
                    audioSource.PlayOneShot(destroyClip);
                    Instantiate(ballsParticles, transform.position, rockParticles.transform.rotation);
                    Destroy(gameObject);
                }
                if (collision.gameObject.CompareTag("ProyectileBlue"))
                {
                    audioSource.PlayOneShot(rockClip);
                    var position = transform.position;
                    Instantiate(rockBlock, position, rockBlock.transform.rotation);
                    Instantiate(rockParticles, position, rockParticles.transform.rotation);
                    Destroy(gameObject);
                }

                break;
            }
            case 2:
            {
                if (collision.gameObject.CompareTag("ProyectileBlue"))
                {
                    Instantiate(ballsParticles, transform.position, rockParticles.transform.rotation);
                    Destroy(gameObject);
                }
                if (collision.gameObject.CompareTag("ProyectileRed"))
                {
                    audioSource.PlayOneShot(rockClip);
                    var position = transform.position;
                    Instantiate(rockBlock, position, rockBlock.transform.rotation);
                    Instantiate(rockParticles, position, rockParticles.transform.rotation);
                    Destroy(gameObject);
                }

                break;
            }
        }
    }
}
