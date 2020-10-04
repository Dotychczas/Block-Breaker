using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{ 
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 2f;
    [SerializeField]  AudioClip[] ballSounds;
    [SerializeField] float bouncePower = 0f;
    bool hasStarted = false;
    Vector3 lastVelocity;
    Vector2 paddleToBallVector;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    AudioSource myAudioSource;
    void Start()
        {

            rb = GetComponent<Rigidbody2D>();
            paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
    {
        lastVelocity = rb.velocity;
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMaouseClick();
        }
        
       
        
    }

    private void LaunchOnMaouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rb.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, bouncePower);
        if(hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
       

    }
}
