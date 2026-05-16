using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [SerializeField] Slider slider;

    [SerializeField] TextMeshProUGUI percentText;

    [SerializeField] Button replayButton;

    [SerializeField] Button nextLevelButton;

    private void Start()
    {
        replayButton.onClick.AddListener(OnClickReplay);

        nextLevelButton.onClick.AddListener(OnClickNextLevel);
    }

    private void OnClickNextLevel()
    {
        PlayerPrefsSave.NormalLevel++;
        GameManagerNormal.Instance.ResetAfterComplete();
        GameManagerNormal.Instance.LoadNewLevel(PlayerPrefsSave.NormalLevel);
    }

    private void OnClickReplay()
    {
        GameManagerNormal.Instance.ResetAfterComplete();
        GameManagerNormal.Instance.LoadNewLevel(PlayerPrefsSave.NormalLevel);
    }

    public void SetUpSlider(int correctValue, int maxValue)
    {
        percentText.text = (correctValue * 100 / maxValue).ToString() + " %";

        DOTween.To(() => slider.value, x => slider.value = x, correctValue * 1f / maxValue, 1f).From(0);
    }
}
