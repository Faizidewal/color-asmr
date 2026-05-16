using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public GameObject levelPanel, settingPanel;
    // Start is called before the first frame update
    void Start()
    {
        // AdsControl.Instance.ShowBannerAd();
        QualitySettings.vSyncCount = 0;  // Disable vertical sync
        Application.targetFrameRate = 60;
    }

   

    public void Play()
    {
        // if (PlayerPrefs.GetInt("level") >= SceneManager.sceneCountInBuildSettings)
        // {
        //     SceneManager.LoadScene(PlayerPrefs.GetInt("THISLEVEL"));
        // }
        // else
        // {
        // SceneManager.LoadScene(PlayerPrefs.GetInt("level", 1));
        // }

        SceneManager.LoadScene(1);
    }

    public void ShowLevelSelector()
    {
        AudioManagerOld.instance.buttonAudio.Play();
        levelPanel.SetActive(true);
    }

    public void ShowSetting()
    {
        AudioManagerOld.instance.buttonAudio.Play();
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        AudioManagerOld.instance.buttonAudio.Play();
        settingPanel.SetActive(false);
    }

    public void CloseLevelSelector()
    {
        AudioManagerOld.instance.buttonAudio.Play();
        levelPanel.SetActive(false);
    }
    public void PlayScene1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void PlayScene2()
    {
        SceneManager.LoadScene("2");
    }
    public void PlayScene3()
    {
        SceneManager.LoadScene("3");
    }
    public void PlayScene4()
    {
        SceneManager.LoadScene("4");
    }
    public void PlayScene5()
    {
        SceneManager.LoadScene("5");
    }
    public void PlayScene6()
    {
        SceneManager.LoadScene("6");
    }
    public void PlayScene7()
    {
        SceneManager.LoadScene("7");
    }
    public void PlayScene8()
    {
        SceneManager.LoadScene("8");
    }
    public void PlayScene9()
    {
        SceneManager.LoadScene("9");
    }
    public void PlayScene10()
    {
        SceneManager.LoadScene("10");
    }
    public void PlayScene11()
    {
        SceneManager.LoadScene("11");
    }
    public void PlayScene12()
    {
        SceneManager.LoadScene("12");
    }
    public void PlayScene13()
    {
        SceneManager.LoadScene("13");
    }
    public void PlayScene14()
    {
        SceneManager.LoadScene("14");
    }
    public void PlayScene15()
    {
        SceneManager.LoadScene("15");
    }
    public void PlayScene16()
    {
        SceneManager.LoadScene("16");
    }
    public void PlayScene17()
    {
        SceneManager.LoadScene("17");
    }
    public void PlayScene18()
    {
        SceneManager.LoadScene("18");
    }
}
