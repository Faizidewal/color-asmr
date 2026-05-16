using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerOld : MonoBehaviour
{
    public static AudioManagerOld instance;

    public AudioSource musicSource;

    public AudioSource winAudio;

    public AudioSource buttonAudio;



    [HideInInspector]
    public int soundState, musicState;

    private void Awake()
    {

        if (FindObjectsOfType(typeof(AudioManagerOld)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }


        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnSoundOff()
    {
        winAudio.volume = 0.0f;
        buttonAudio.volume = 0.0f;

        soundState = 0;
    }

    public void TurnSoundOn()
    {
        winAudio.volume = 1.0f;

        buttonAudio.volume = 1.0f;


        soundState = 1;
    }

    public void TurnMusicOff()
    {
        musicSource.volume = 0.0f;
        musicState = 0;
    }

    public void TurnMusicOn()
    {
        musicSource.volume = 0.3f;
        musicState = 1;
    }
}
