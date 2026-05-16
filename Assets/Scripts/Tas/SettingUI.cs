using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Button backButton;

    [SerializeField] Button musicButton;

    [SerializeField] Button soundButton;

    [SerializeField] Button vibrateButton;

    private void Start()
    {
        backButton.onClick.AddListener(OnClickBack);

        musicButton.onClick.AddListener(OnClickMusic);

        soundButton.onClick.AddListener(OnClickSound);

        vibrateButton.onClick.AddListener(OnClickVibrate);
    }

    private void OnClickVibrate()
    {
        PlayerPrefsSave.OnVibrate = !PlayerPrefsSave.OnVibrate;
    }

    private void OnClickSound()
    {
        PlayerPrefsSave.OnSound = !PlayerPrefsSave.OnSound;
    }

    private void OnClickMusic()
    {
        PlayerPrefsSave.OnMusic = !PlayerPrefsSave.OnMusic;
    }

    private void OnClickBack()
    {
        GameManagerNormal.Instance.BackFromSettingUI();
    }
}
