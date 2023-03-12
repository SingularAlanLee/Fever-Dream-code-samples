using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManagerScript : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _musicList;
    AudioSource _musicSource;
    playerController _PC;

    void Start()
    {
        _musicSource = GetComponent<AudioSource>();
        _PC = GameObject.Find("player").GetComponent<playerController>(); 
    }

    void Update()
    {
        if(!_PC.gameOver && !_musicSource.isPlaying)
        {
            _musicSource.clip = _musicList[Random.Range(0,_musicList.Length)];
            _musicSource.Play();
        }  
    }
}
