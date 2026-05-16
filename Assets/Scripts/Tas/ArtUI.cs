using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtUI : MonoBehaviour
{
    [SerializeField] Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnClickBack);
    }

    private void OnClickBack()
    {
        GameManagerNormal.Instance.BackFromArtUI();
    }
}
