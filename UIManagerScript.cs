using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    int _playerScore = 0, _highScore = 0;
    bool _gameStarted = false;
    public Text _scoreText, _highScoreText;
    playerController _PC;
    [SerializeField]
    GameObject _spawnMan, _title, _instructions, _gameOverText, _restartInsctructions;
    
    void Start()
    {
        _PC = GameObject.Find("player").GetComponent<playerController>();
        if(_PC == null)
        {
            Debug.Log("Player controller not found");
        }

        if(_restartInsctructions == null)
        {
            Debug.Log("Couldn't find restart instructions");
        }

        if(_title == null)
        {
            Debug.Log("Title was not found");
        }

        if(_spawnMan == null)
        {
            Debug.Log("Spawn Manager was not found");
        }
        else
        {
            _spawnMan.SetActive(false);
        }

        if(_instructions == null)
        {
            Debug.Log("Instructions game object was not found");
        }

        if(_gameOverText == null)
        {
            Debug.Log("Game over text not found");
        }

        _title.SetActive(true);
        _instructions.SetActive(true);
        _gameOverText.SetActive(false);
        _restartInsctructions.SetActive(false);
        _highScore = PlayerPrefs.GetInt("highScore",0);
        _scoreText.text = "Score: " + _playerScore;
        _highScoreText.text = "High Score: " + _highScore;
    }

    void Update()
    {
        //debug high score
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.SetInt("highScore",0);
        }

        if(!_gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            _gameStarted = true;
            _title.SetActive(false);
            _instructions.SetActive(false);
            _spawnMan.SetActive(true);
        }

        if(_PC.gameOver)
        {
            _gameOverText.SetActive(true);
            _gameOverText.transform.Rotate(0f,0f,0.3f, Space.Self);
            _restartInsctructions.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void updateScore()
    {
        _playerScore++;
        _scoreText.text = "Score: " + _playerScore;
        if(_playerScore > _highScore)
        {
            _highScore = _playerScore;
            PlayerPrefs.SetInt("highScore",_highScore);
            _highScoreText.text = "High Score: " + _highScore;
        }
    }
}
