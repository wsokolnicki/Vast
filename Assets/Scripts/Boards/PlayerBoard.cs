using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public abstract class PlayerBoard : MonoBehaviour
{
    [Header("Cache", order = 0)]
    [SerializeField] GameObject canvas;
    [SerializeField] Transform cameraPosition;
    [SerializeField] Transform boardPosition;
    protected Camera mainCamera;

    protected bool zoomedIn = false;
    protected bool infoClicked = false;

    [Header("BOARD INFO CACHE", order = 2)]
    [SerializeField] GameObject colliders;

    protected GameObject currentlyOpenPanel;

    protected abstract GameObject ReturnPanel(string name);

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CameraZoomInAndOut();
        if (!zoomedIn)
            return;
        else
        {
            BoardInfo();
        }
    }

    void CameraZoomInAndOut()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    CameraManager.SetCurrentView(cameraPosition);
                    TurnSelectedChilderON_OFF(true);
                }

                else if (hit.collider.gameObject == GameObject.Find("Floor"))
                {
                    CameraManager.SetCurrentView(boardPosition);
                    TurnSelectedChilderON_OFF(false);
                }
            }
        }
    }
    void TurnSelectedChilderON_OFF(bool x)
    {
        zoomedIn = x;
        canvas.SetActive(x);
        colliders.SetActive(x);
    }

    //RMC on image on board initiates PanelInfo() and EnlargeImages()
    protected abstract void BoardInfo();

    protected void PanelInfo(string name)
    {
        GameObject panel = ReturnPanel(name);
        currentlyOpenPanel = panel;
        panel.SetActive(true);
        infoClicked = true;
        StartCoroutine(EnlargeImages(panel.transform.GetChild(0), panel.transform.GetChild(1), name));
    }

    protected void TurnPanelOff(GameObject panel)
    {
        panel.SetActive(false);
    }

    IEnumerator EnlargeImages(Transform image_1, Transform image_2, string animationTrigger)
    {
        RectTransform image1 = image_1.GetComponent<RectTransform>();
        RectTransform image2 = image_2.GetComponent<RectTransform>();

        Vector2 image1StartSize = image1.sizeDelta;
        Vector3 image1StartPosition = image1.localPosition;
        Vector2 image2StartSize = image2.sizeDelta;
        Vector3 image2StartPosition = image2.localPosition;

        image_1.parent.parent.GetComponent<Animator>().SetTrigger(animationTrigger);

        yield return new WaitUntil(() => !infoClicked);

        image_1.parent.parent.GetComponent<Animator>().SetTrigger("back");

        image1.sizeDelta = image1StartSize;
        image1.localPosition = image1StartPosition;

        image2.sizeDelta = image2StartSize;
        image2.localPosition = image2StartPosition;
    }
}

#pragma warning restore 0649