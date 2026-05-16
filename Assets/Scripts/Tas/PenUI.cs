using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenUI : MonoBehaviour
{
    [SerializeField] Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        GameManagerNormal.Instance.BackFromPenUI();
    }
}
