using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] Button settingButton;

    [SerializeField] Button penButton;

    [SerializeField] Button artButton;

    [SerializeField] Button retryButton;

    [SerializeField] Button skipButton;

    public CanvasGroup canvasGroup;

    public List<Image> colorImages;

    public List<Button> colorButtons;

    public GameObject colorPanel;

    public Button okButton;

    void Start()
    {
        settingButton.onClick.AddListener(OnClickSetting);

        penButton.onClick.AddListener(OnClickPen);

        artButton.onClick.AddListener(OnClickArt);

        retryButton.onClick.AddListener(OnClickRetry);

        skipButton.onClick.AddListener(OnClickSkip);

        okButton.onClick.AddListener(OnClickOk);

        for (int i = 0; i < colorButtons.Count; i++)
        {
            int x = i;
            colorButtons[x].onClick.AddListener(() =>
            {
                GameManagerNormal.Instance.controller.SelectColor(colorImages[x].color);

                if (trueColor == colorImages[x].color)
                {
                    GameManagerNormal.Instance.correctValue++;
                }
            });
        }
    }

    private void OnClickOk()
    {
        GameManagerNormal.Instance.NextColoring();

        okButton.gameObject.SetActive(false);
    }

    private void OnClickSkip()
    {
        PlayerPrefsSave.NormalLevel++;
        GameManagerNormal.Instance.LoadNewLevel(PlayerPrefsSave.NormalLevel);
    }

    private void OnClickRetry()
    {
        GameManagerNormal.Instance.Reset();
        
    }

    
    private void OnClickArt()
    {
        GameManagerNormal.Instance.GoToArtUI();
    }

    private void OnClickPen()
    {
        GameManagerNormal.Instance.GoToPenUI();
    }

    private void OnClickSetting()
    {
        GameManagerNormal.Instance.GotoSettingUI();
    }

    public void ShowColorPanel(bool on)
    {
        colorPanel.SetActive(on);
    }

    private Color trueColor;

    public void ChangeColorPanel(Color color)
    {
        trueColor = color;

        List<Color> colors = new()
        {
            color,

            new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f),1),

            new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f),1)
        };

        Shuffle(colors);

        for (int i = 0; i < 3; i++)
        {
            colorImages[i].color = colors[i];
        }
    }

    public void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            (ts[r], ts[i]) = (ts[i], ts[r]);
        }
    }
}
