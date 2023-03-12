using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    bool _grounded = true;
    public bool gameOver = false;
    public float thrust = 10f, speed = 10f;
    float _horInput;
    Rigidbody rb;
    [SerializeField]
    AudioClip _bounceSound;
    AudioSource _audioSource;

    //player will try to ascend vertically as far as they can

    void Start()
    {
        _audioSource = GameObject.Find("audioManager").GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.Log("Audio manager not found");
        }
        else
        {
            _audioSource.clip = _bounceSound;
        }

        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3 (1.03f,-1.76f,0f);
    }

    void Update()
    {
        if(!gameOver)
        {
            playerMovement();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag){
            case "ground":
                _grounded = true;
                break;
            case "obstacle":
                gameOver = true;
                break;
        }
    }

    void playerMovement()
    {
        _horInput = Input.GetAxis("Horizontal");

        if(Input.GetButton("Horizontal")){
            transform.Translate(new Vector3(_horInput,0,0) * Time.deltaTime * speed);
        }
        if(Input.GetButtonDown("Jump") && _grounded){
            rb.AddForce(0,thrust,0, ForceMode.Impulse);
            _audioSource.PlayOneShot(_bounceSound);
            _grounded = false;
        }
    }
}
