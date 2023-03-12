using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManagerScript : MonoBehaviour
{
    public GameObject[] obstacles;
    GameObject _generatedObstacle, obstacle;
    playerController _playerController;
    obstacleScript obsScript;
    int _direction, _objIndex;
    public Vector3 rightRot, leftRot;
    //1 = left
    //2 = right
    //3 = top

    void Start()
    {
        rightRot = new Vector3(transform.eulerAngles.x, -90f, transform.eulerAngles.z);
        leftRot = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
        _playerController = GameObject.Find("player").GetComponent<playerController>();
        if(_playerController == null)
        {
            Debug.Log("could not find player");
        }
        InvokeRepeating("SpawnObstacles", 2.0f, 3.0f);
    }

    void SpawnObstacles()
    {
        if(!_playerController.gameOver)
        {
            _objIndex = Random.Range(0,obstacles.Length);
            _generatedObstacle = obstacles[_objIndex];
            _direction = Random.Range(1,3);

            //generate random to choose between left and right sides of screen
            if(_objIndex == 0 || _objIndex == 1)
            {
                if (_direction == 2)
                {
                    obstacle = Instantiate(_generatedObstacle,new Vector3 (12.0f, Random.Range(-1.76f, 0.5f), 0), _generatedObstacle.transform.rotation);
                }
                else if (_direction == 1)
                {
                    obstacle = Instantiate(_generatedObstacle,new Vector3 (-11.0f, Random.Range(-1.76f, 0.5f), 0), _generatedObstacle.transform.rotation);
                }
                obsScript = obstacle.GetComponent<obstacleScript>();
                obsScript.direction = _direction;
            }

            //if object drops from top
            else if(_objIndex == 2 || _objIndex == 3)
            {
                obstacle = Instantiate(_generatedObstacle,new Vector3 (Random.Range(-8.79f, 9.73f), 6f, 0), _generatedObstacle.transform.rotation);
                obsScript = obstacle.GetComponent<obstacleScript>();
                obsScript.direction = 3;
            }
        }
    }
}
