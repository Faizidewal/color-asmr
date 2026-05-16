using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class ScaleCanvas : MonoBehaviour
{
    private CanvasScaler canvasScaler;

    void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        CheckCanvas();
    }

    void CheckCanvas()
    {
        float ratio = (float)1920f / 1080f;
        float canvasRatio = (float)Screen.height / Screen.width;
        if (canvasRatio >= ratio)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
    }
}
