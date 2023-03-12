using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
{
    public float speed;
    public int direction;
    //1 = left
    //2 = right

    playerController _playerController;
    UIManagerScript _UIMan;
    spawnManagerScript _spawnMan;
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _soundEffect;
    
    void Start()
    {
        _playerController = GameObject.Find("player").GetComponent<playerController>();
        //check if player controller has been found
        if(_playerController == null)
        {
            Debug.Log("could not find player");
        }
        _UIMan = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
        //check that UI manager has been found
        if(_UIMan == null)
        {
            Debug.Log("could not find UI manager");
        }

        _spawnMan = GameObject.Find("spawnManager").GetComponent<spawnManagerScript>();
        //check that spawn manager was found
        if(_spawnMan == null)
        {
            Debug.Log("could not find spawn manager");
        }

        _audioSource = GameObject.Find("audioManager").GetComponent<AudioSource>();
        //check if audio source was found, if so, assign sound effect
        if(_audioSource == null)
        {
            Debug.Log("Audio source not found");
        }
        else
        {
            _audioSource.clip = _soundEffect;
        }

        speed = Random.Range(1,10);
        _audioSource.PlayOneShot(_soundEffect);
        findDirection();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.Rotate(0f,0f,1f, Space.Self);
        
        getRidOfObstacle();
    }

    void findDirection()
    {
        if(direction == 2){
            transform.eulerAngles = _spawnMan.rightRot;
        }
        else if (direction == 1){
            transform.eulerAngles = _spawnMan.leftRot;
        }
    }

    void getRidOfObstacle()
    {
        //if right
        if(transform.position.x < -11.4f)
        {
            OutOfBounds();
        }

        //if left
        if(transform.position.x > 12f)
        {
            OutOfBounds();
        }

        //if going down
        if(transform.position.y < -3f)
        {
            OutOfBounds();
        }
    }

    //call when object is out of bounds
    void OutOfBounds()
    {
        Destroy(gameObject);
            if(!_playerController.gameOver)
            {
                _UIMan.updateScore();
            }
    }
}
