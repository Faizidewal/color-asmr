using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEditor.MPE;

public class Controller : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Vector3> PointsToBeFollow;
    public List<GameObject> SequenceOfLines;
    public List<GameObject> DarwingPanels;
    [Space(10)]
    public LineRenderer lineRenderer;
    [Space(10)]
    public GameObject SampleLineRender;
    public GameObject colorselection;
    public GameObject PaintBrush;
    public GameObject EndImage;
    public GameObject StartImage;
    public GameObject EndimageDisplay;
    public GameObject StartImageDisplay;
    public Transform visual;
    [Space(10)]
    public bool Coloring;
    public bool line;
    public bool Reached;
    [Space(10)]
    public int count;
    public int PaintLineCount;
    public int DrawingpaCount;
    [Space(10)]
    public float Speed;
    public static Controller Instance;
    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Restart();


    }

    Vector3 lastPositon;

    Vector3 currentPosition;

    bool shouldGetPosition = false;

    Tween backPen;

    bool visualRotation = false;

    void Update()
    {
        currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));

        var deltaPositon = Vector3.zero;
        if (shouldGetPosition)
        {
            deltaPositon = currentPosition - lastPositon;
        }
        lastPositon = currentPosition;

        if (Input.GetMouseButtonDown(0))
        {
            shouldGetPosition = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            shouldGetPosition = false;
        }

        if (line)
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (count < PointsToBeFollow.Count)
                {
                    movement();

                    backPen.Kill();
                }
                else
                {
                    Reached = true;
                }
            }
        }
        if (Coloring)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PaintBrush.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                float currentPercentage = DarwingPanels[DrawingpaCount].transform.GetChild(1).GetComponent<P3dChannelCounter>().RatioA;

                transform.Translate(deltaPositon);

                if (currentPercentage >= 0.8f)
                {
                    GameManagerNormal.Instance.ShowOkButton();
                }


                // if (count < PointsToBeFollow.Count)
                // {
                //     movement();
                // }
                // else
                // {
                //     if (DrawingpaCount < DarwingPanels.Count-1)
                //     {
                //         DarwingPanels[DrawingpaCount].transform.GetChild(1).GetComponent<P3dPaintableTexture>().enabled = false;
                //         DrawingpaCount++;
                //         Coloring = false;
                //         transform.DOMove(new Vector3(0.56f, 1.22f, -1.25f), 0.45f);
                //         StartCoroutine(putAlineWithEffect());
                //     }
                //     else
                //     {
                //         Coloring = false;
                //         Ui.Instance.LevelCompleted();
                //         if (AudioManager.instance)
                //         {
                //          AudioManager.instance.winAudio.Play();
                //         }
                //         if(Particaleffect.instance)
                //         {
                //             Particaleffect.instance.playpop();
                //         }
                //         if(Controller.Instance)
                //         {
                //             Controller.Instance.transform.gameObject.SetActive(false);
                //         }
                //     }
                // }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(13);
            PaintBrush.SetActive(false);

            if (line && Reached)
            {
                EndimageDisplay.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.5f).From(Color.black);
                UndoingPaintingAbility();
                if (PaintLineCount < SequenceOfLines.Count - 1)
                {
                    PaintLineCount++;

                    PutADrawLine();
                }
                else
                {
                    putAlineWithEffect();
                }
            }
            else if (line && !Reached)
            {
                if (isInLine && count != 0)
                {
                    PointsToBeFollow.Insert(count, transform.position);

                    isInLine = false;

                    visualRotation = !visualRotation;

                    visual.DOLocalRotate(new Vector3(0, 0, visualRotation ? 40 : 45), 0.5f);
                }

                backPen = transform.DOMove(PointsToBeFollow[count] + new Vector3(0.25f, -0.25f, 0), 0.5f);
            }

            if (Coloring)
            {
                visualRotation = !visualRotation;

                visual.DOLocalRotate(new Vector3(0, 0, visualRotation ? 40 : 45), 0.5f);
            }
        }
    }

    public void AlmostFinished()
    {
        DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>().enabled = true;

        DarwingPanels[DrawingpaCount].transform.GetChild(1).gameObject.SetActive(false);

        if (DrawingpaCount < DarwingPanels.Count - 1)
        {
            DrawingpaCount++;
            Coloring = false;
            putAlineWithEffect();
        }
        else
        {
            Coloring = false;

            GameManagerNormal.Instance.GoToLevelCompleteUI();
            if (AudioManagerOld.instance)
            {
                AudioManagerOld.instance.winAudio.Play();
            }
            if (Particaleffect.instance)
            {
                Particaleffect.instance.playpop();
            }
            if (Instance)
            {
                Instance.gameObject.SetActive(false);
            }
        }


    }

    bool isInLine = false;

    bool shouldShowEnd = true;

    public void movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointsToBeFollow[count], Speed * Time.deltaTime * Camera.main.orthographicSize / 5);

        if (transform.position == PointsToBeFollow[count])
        {
            if (count < PointsToBeFollow.Count && PaintBrush.activeSelf)
            {
                count++;
            }

            if (shouldShowEnd)
            {
                shouldShowEnd = false;

                EndimageDisplay.transform.position = PointsToBeFollow[^1];

                StartImageDisplay.GetComponent<SpriteRenderer>().DOColor(Color.clear, 0.4f).OnComplete(() =>
                {
                    if (Reached)
                    {
                        return;
                    }

                    EndimageDisplay.GetComponent<SpriteRenderer>().DOColor(Color.black, 0.4f).From(Color.clear);
                });
            }

            PaintBrush.SetActive(true);

            isInLine = true;
        }
    }

    public void UndoingPaintingAbility()
    {
        line = false;
        count = 0;

        GameObject Temp = SequenceOfLines[PaintLineCount];
        Temp.transform.GetChild(0).gameObject.SetActive(false);
        // Temp.transform.GetChild(1).GetComponent<P3dPaintableTexture>().enabled = false;
        Temp.transform.GetChild(1).gameObject.SetActive(false);
        Temp.GetComponent<SpriteRenderer>().enabled = true;
    }

    P3dPaintSphere PS;

    public void PutADrawLine()
    {
        GameObject Temp = SequenceOfLines[PaintLineCount];
        Temp.SetActive(true);

        SpriteRenderer tempSpriteRenderer = Temp.GetComponent<SpriteRenderer>();

        Sprite A = tempSpriteRenderer.sprite;
        Texture Present = tempSpriteRenderer.sprite.texture;
        tempSpriteRenderer.enabled = false;
        Temp.transform.GetChild(0).gameObject.SetActive(true);
        PointsToBeFollow = Temp.transform.GetChild(0).GetComponent<linemakeing>().points;

        visual.GetChild(2).GetComponent<SpriteRenderer>().color = Color.black;

        PS.blendMode.Texture = Present;
        PS.blendMode.Color = Color.black;
        GameObject temp2 = Temp.transform.GetChild(1).gameObject;
        temp2.transform.localScale = A.bounds.size;
        temp2.transform.localPosition = new Vector3(0, 0, 0);
        temp2.SetActive(true);
        temp2.GetComponent<MeshRenderer>().material.SetTexture("Albedo (RGB) Alpha (A)", Present);

        temp2.GetComponent<P3dPaintableTexture>().Clear(null, Color.clear);

        line = true;
        Reached = false;

        StartImageDisplay.transform.position = PointsToBeFollow[0];

        StartImageDisplay.GetComponent<SpriteRenderer>().DOColor(Color.black, 0.5f).From(Color.clear);

        shouldShowEnd = true;

        if (PaintLineCount == 0 && !isReset)
        {
            isReset = true;

            transform.DOMove(PointsToBeFollow[0] + new Vector3(0.25f, -0.25f, 0), 0.5f).From(new Vector3(GameManagerNormal.Instance.startLinePoint.position.x, GameManagerNormal.Instance.startLinePoint.position.y, transform.position.z));
        }
        else
        {
            transform.DOMove(PointsToBeFollow[0] + new Vector3(0.25f, -0.25f, 0), 0.5f);

            GameManagerNormal.Instance.Zoom(PaintLineCount, true);
        }
    }
    private void putAlineWithEffect()
    {
        transform.DOMoveInTargetLocalSpace(GameManagerNormal.Instance.startColorPoint, new Vector3(0, 0, transform.position.z - GameManagerNormal.Instance.startColorPoint.position.z), 0.5f);
        // Mesh.GetComponent<MeshRenderer>().materials[1].color = Color.black;

        DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>().enabled = true;
        DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>().color = Color.white;
        // StartCoroutine(blink());
        spriteRenderer.gameObject.SetActive(true);
        spriteRenderer.DOColor(new Color(1, 1, 1, 0), 0.7f).From(Color.white).SetLoops(-1, LoopType.Yoyo);
        DarwingPanels[DrawingpaCount].GetComponent<SpriteMask>().enabled = true;
        // colorselection.SetActive(true);

        GameManagerNormal.Instance.ShowColorPanel(true, SequenceOfLines.Count + DrawingpaCount);
        GameManagerNormal.Instance.drawPanel.decor.SetActive(true);


        GameManagerNormal.Instance.Zoom(SequenceOfLines.Count + DrawingpaCount, false);
    }

    public void SelectColor(Color color)
    {
        if (AudioManagerOld.instance)
        {
            AudioManagerOld.instance.buttonAudio.Play();
        }
        count = 0;
        PS.Color = color;
        PS.Scale = new Vector3(2f, 2f, 2f);
        PS.blendMode.Color = color;

        visual.GetChild(2).GetComponent<SpriteRenderer>().color = color;

        SpriteRenderer tempSpriteRenderer = DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>();

        PS.blendMode.Texture = tempSpriteRenderer.sprite.texture;
        GameObject temp = DarwingPanels[DrawingpaCount].transform.GetChild(0).gameObject;
        temp.SetActive(true);
        tempSpriteRenderer.enabled = false;
        tempSpriteRenderer.color = color;



        Transform tempTransform = DarwingPanels[DrawingpaCount].transform.GetChild(1);

        tempTransform.gameObject.SetActive(true);
        tempTransform.localScale = tempSpriteRenderer.sprite.bounds.size;
        tempTransform.transform.localPosition = new Vector3(0, 0, 0);

        // DarwingPanels[DrawingpaCount].transform.GetChild(1).GetComponent<P3dPaintableTexture>().Clear(null, Color.clear);

        temp.GetComponent<LineRenderer>().enabled = false;
        // colorselection.SetActive(false);

        GameManagerNormal.Instance.ShowColorPanel(false);
        PointsToBeFollow = temp.GetComponent<linemakeing>().points;
        Coloring = true;

        spriteRenderer.gameObject.SetActive(false);
        DarwingPanels[DrawingpaCount].GetComponent<SpriteMask>().enabled = false;

        transform.DOMove(PointsToBeFollow[0], 0.5f);
    }

    bool isReset = false;

    public void Reset()
    {
        if (line)
        {
            GameObject Temp;
            if (count == 0 && PaintLineCount > 0)
            {
                Temp = SequenceOfLines[PaintLineCount];

                Temp.transform.GetChild(0).gameObject.SetActive(false);
                Temp.transform.GetChild(1).gameObject.SetActive(false);
                PaintLineCount--;
            }
            else
            {
                count = 0;
            }

            EndimageDisplay.GetComponent<SpriteRenderer>().color = Color.clear;

            PutADrawLine();

            Temp = SequenceOfLines[PaintLineCount];

            Temp.transform.GetChild(0).gameObject.SetActive(true);
            Temp.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (Coloring)
        {
            GameManagerNormal.Instance.HideOkButton();

            PaintBrush.SetActive(false);

            StartCoroutine(ShutDownColoring());

            Coloring = false;
            putAlineWithEffect();
        }
        else
        {
            PaintBrush.SetActive(false);

            if (DrawingpaCount > 0)
            {
                DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>().enabled = false;
                DarwingPanels[DrawingpaCount].GetComponent<SpriteMask>().enabled = false;

                StartCoroutine(ShutDownColoring());

                DrawingpaCount--;

                putAlineWithEffect();
            }
            else
            {
                PS.Scale = new Vector3(0.6f, 0.6f, 0.6f);
                spriteRenderer.gameObject.SetActive(false);

                DarwingPanels[DrawingpaCount].GetComponent<SpriteRenderer>().enabled = false;
                DarwingPanels[DrawingpaCount].GetComponent<SpriteMask>().enabled = false;

                StartCoroutine(ShutDownColoring());

                GameObject Temp;

                EndimageDisplay.GetComponent<SpriteRenderer>().color = Color.clear;

                GameManagerNormal.Instance.ShowColorPanel(false);

                PutADrawLine();

                Temp = SequenceOfLines[PaintLineCount];

                Temp.transform.GetChild(0).gameObject.SetActive(true);
                Temp.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator ShutDownColoring()
    {
        Debug.Log(11);
        yield return null;

        Debug.Log(10);

        DarwingPanels[DrawingpaCount].transform.GetChild(1).GetComponent<P3dPaintableTexture>().Clear(null, Color.clear);

        yield return null;

        Debug.Log(9);

        DarwingPanels[DrawingpaCount].transform.GetChild(0).gameObject.SetActive(false);
        DarwingPanels[DrawingpaCount].transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Restart()
    {
        SequenceOfLines = GameManagerNormal.Instance.drawPanel.SequenceOfLines;

        DarwingPanels = GameManagerNormal.Instance.drawPanel.DarwingPanels;

        EndimageDisplay = Instantiate(EndImage, transform.position, Quaternion.identity);

        StartImageDisplay = Instantiate(StartImage, transform.position, Quaternion.identity);

        PS = PaintBrush.GetComponent<P3dPaintSphere>();

        count = 0;
        PaintLineCount = 0;
        DrawingpaCount = 0;
        line = true;
        // colorselection.SetActive(false);
        GameManagerNormal.Instance.ShowColorPanel(false);
        PutADrawLine();

        PaintBrush.SetActive(false);

        visual.GetChild(2).GetComponent<SpriteRenderer>().color = Color.black;
    }
}
