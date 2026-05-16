using DG.Tweening;
using UnityEngine;

public class GameManagerNormal : MonoBehaviour
{
    public static GameManagerNormal Instance;

    [SerializeField] PlayUI playUI;

    [SerializeField] PenUI penUI;

    [SerializeField] ArtUI artUI;

    [SerializeField] SettingUI settingUI;

    [SerializeField] LevelCompleteUI levelCompleteUI;

    public Controller controller;

    [SerializeField] Transform topPoint;

    [SerializeField] Transform bottomColorPoint;

    [SerializeField] Transform bottomLinePoint;

    public Transform startLinePoint;

    public Transform startColorPoint;

    [SerializeField] Transform panel;

    [SerializeField] Transform postPanel;

    [SerializeField] Color backgroundColor;

    public int maxValue;

    public int correctValue;

    public DrawPanel drawPanel;

    private Vector2 paintZoneSize;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;

        LoadLevel(PlayerPrefsSave.NormalLevel);

        SetUpPanel();

        Debug.Log(drawPanel.gameObject.name);

        maxValue = drawPanel.DarwingPanels.Count;
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;  // Disable vertical sync
        Application.targetFrameRate = 60;
    }

    public void GoToPenUI()
    {
        playUI.gameObject.SetActive(false);

        penUI.gameObject.SetActive(true);

        controller.enabled = false;
    }

    public void BackFromPenUI()
    {
        playUI.gameObject.SetActive(true);

        penUI.gameObject.SetActive(false);

        controller.enabled = true;
    }

    public void GoToArtUI()
    {
        playUI.gameObject.SetActive(false);

        artUI.gameObject.SetActive(true);

        controller.enabled = false;
    }

    public void BackFromArtUI()
    {
        playUI.gameObject.SetActive(true);

        artUI.gameObject.SetActive(false);

        controller.enabled = true;
    }

    public void GotoSettingUI()
    {
        settingUI.gameObject.SetActive(true);
    }

    public void BackFromSettingUI()
    {
        settingUI.gameObject.SetActive(false);
    }

    public void GoToLevelCompleteUI()
    {
        levelCompleteUI.gameObject.SetActive(true);

        postPanel.localScale = Vector3.one * Camera.main.orthographicSize / 5;

        ZoneOfPart temp = postPanel.GetComponent<ZoneOfPart>();

        ZoneOfPart temp1 = panel.GetComponent<ZoneOfPart>();

        Vector3 postPanelCenter = temp.center;

        Vector3 panelCenter = temp1.center;

        postPanel.DOMove(postPanel.position + new Vector3((panelCenter - postPanelCenter).x, (panelCenter - postPanelCenter).y, postPanel.position.z), 1f);

        postPanel.GetChild(0).GetComponent<SpriteRenderer>().DOColor(Color.white, 1f).From(Color.clear);

        float ratioX = temp.width / temp1.width;

        float ratioY = temp.height / temp1.height;

        if (ratioX > ratioY)
        {
            panel.DOScale(panel.localScale * ratioY, 1f);
        }
        else
        {
            panel.DOScale(panel.localScale * ratioX, 1f);
        }

        Camera.main.transform.DORotate(new Vector3(0, 0, 5), 1f).From(Vector3.zero);

        Camera.main.transform.DOMoveY(-Camera.main.orthographicSize / 7, 1f);

        Camera.main.DOColor(backgroundColor, 1f).From(Color.white);

        playUI.canvasGroup.DOFade(0, 1f).From(1).OnComplete(() => playUI.gameObject.SetActive(false));

        levelCompleteUI.canvasGroup.DOFade(1, 1f).From(0);

        levelCompleteUI.SetUpSlider(correctValue, maxValue);
    }

    public void ShowOkButton()
    {
        playUI.okButton.gameObject.SetActive(true);
    }

    public void HideOkButton()
    {
        playUI.okButton.gameObject.SetActive(false);
    }

    public void NextColoring()
    {
        controller.AlmostFinished();
    }

    private void SetUpPanel()
    {
        ZoneOfPart temp = panel.GetChild(0).GetComponent<ZoneOfPart>();

        Vector2 panelSize = new(temp.width * 2, temp.height * 2);

        paintZoneSize = new(Camera.main.orthographicSize * 2 * Camera.main.aspect, topPoint.position.y - bottomLinePoint.position.y);

        float ratioX = panelSize.x / paintZoneSize.x;

        float ratioY = panelSize.y / paintZoneSize.y;

        Debug.Log(panelSize.x + "  " + panelSize.y);

        Debug.Log(paintZoneSize.x + "  " + paintZoneSize.y);

        Debug.Log(ratioX + "  " + ratioY);

        if (ratioX > ratioY)
        {
            Camera.main.orthographicSize *= ratioX;
        }
        else
        {
            Camera.main.orthographicSize *= ratioY;
        }

        float camMoveY = temp.center.y - (topPoint.position.y + bottomLinePoint.position.y) / 2;

        // Debug.Log()

        Camera.main.transform.position = new Vector3(temp.center.x, Camera.main.transform.position.y + camMoveY, Camera.main.transform.position.z);
    }

    public void Zoom(int index, bool isLine)
    {
        ZoneOfPart temp = panel.GetChild(index).GetComponent<ZoneOfPart>();

        Vector2 panelSize = new(temp.width * 2, temp.height * 2);

        paintZoneSize = new(Camera.main.orthographicSize * 2 * Camera.main.aspect, topPoint.position.y - (isLine ? bottomLinePoint : bottomColorPoint).position.y);

        float ratioX = panelSize.x / paintZoneSize.x;

        float ratioY = panelSize.y / paintZoneSize.y;

        Debug.Log(panelSize.x + "  " + panelSize.y);

        Debug.Log(paintZoneSize.x + "  " + paintZoneSize.y);

        Debug.Log(ratioX + "  " + ratioY);


        if (ratioX > ratioY)
        {
            DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, Camera.main.orthographicSize * ratioX, 0.5f);
        }
        else
        {
            DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, Camera.main.orthographicSize * ratioY, 0.5f);
        }

        float camMoveY = temp.center.y - (topPoint.position.y + (isLine ? bottomLinePoint : bottomColorPoint).position.y) / 2;

        Camera.main.transform.DOMove(new Vector3(temp.center.x, Camera.main.transform.position.y + camMoveY, Camera.main.transform.position.z), 0.5f);
    }

    public void ShowColorPanel(bool on, int index = 0)
    {
        if (on)
        {
            Debug.Log(panel.GetChild(index).gameObject.name);

            playUI.ChangeColorPanel(panel.GetChild(index).GetComponent<ZoneOfPart>().trueColor);
        }

        playUI.ShowColorPanel(on);
    }

    public void Reset()
    {
        controller.Reset();
    }

    public void LoadLevel(int level)
    {
        panel = ((GameObject)Instantiate(Resources.Load("Level/Panel " + level))).transform;

        drawPanel = panel.GetComponent<DrawPanel>();
    }

    public void ResetAfterComplete()
    {
        controller.gameObject.SetActive(true);

        Camera.main.transform.rotation = Quaternion.identity;

        Camera.main.backgroundColor = Color.white;

        postPanel.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;

        levelCompleteUI.gameObject.SetActive(false);

        playUI.gameObject.SetActive(true);

        playUI.canvasGroup.alpha = 1;
    }

    public void LoadNewLevel(int level)
    {
        Destroy(panel.gameObject);

        LoadLevel(level);

        Zoom(0, true);
        // SetUpPanel();

        Debug.Log(drawPanel.gameObject.name);

        maxValue = drawPanel.DarwingPanels.Count;

        controller.Restart();
    }
}
