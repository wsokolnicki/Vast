using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoard : MonoBehaviour
{
    [Header("Cache", order = 0)]
    [SerializeField] GameObject canvas;
    Camera mainCamera;

    [Header("Values", order = 1)]
    [SerializeField] float cameraHeight = 38f;
    Vector3 cameraStartPosition;
    Vector3 cameraStartRotation;
    float cameraAngleY;
    bool zoomedIn = false;
    bool infoClicked = false;

    [Header("BOARD INFO CACHE", order = 2)]
    [SerializeField] GameObject colliders;
    [Header("_Bomb", order = 3)]
    [SerializeField] GameObject bombCollider;
    [SerializeField] GameObject bombPanel;
    [Header("_Bow", order = 4)]
    [SerializeField] GameObject bowCollider;
    [SerializeField] GameObject bowPanel;
    [Header("_Map", order = 5)]
    [SerializeField] GameObject mapCollider;
    [SerializeField] GameObject mapPanel;
    [Header("_Shield", order = 6)]
    [SerializeField] GameObject shieldCollider;
    [SerializeField] GameObject shieldPanel;
    [Header("_TheKnight", order = 7)]
    [SerializeField] GameObject knightCollider;
    [SerializeField] GameObject knightPanel;
    [Header("_Grit", order = 8)]
    [SerializeField] GameObject gritCollider;
    [SerializeField] GameObject gritPanel;

    GameObject currentlyOpenPanel;

    GameObject ReturnPanel(string name)
    {
        switch(name)
        {
            case "bomb":
                return bombPanel;
            case "bow":
                return bowPanel;
            case "map":
                return mapPanel;
            case "shield":
                return shieldPanel;
            case "knight":
                return knightPanel;
            case "grit":
                return gritPanel;
            default:
                return null;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        cameraStartPosition = mainCamera.transform.position;
        cameraStartRotation = mainCamera.transform.eulerAngles;
        cameraAngleY = transform.parent.eulerAngles.y;
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
                    Vector3 target = new Vector3(transform.position.x, cameraHeight, transform.position.z);
                    Vector3 cameraRotation = transform.eulerAngles;
                    CameraMovement._cam.SetBoardValues(transform.position, target, cameraRotation);
                    TurnSelectedChilderON_OFF(true);
                }
                else if (hit.collider.gameObject == bombCollider || 
                    hit.collider.gameObject == bowCollider || infoClicked)
                    return;
                else
                {
                    mainCamera.transform.position = cameraStartPosition;
                    mainCamera.transform.eulerAngles = cameraStartRotation;
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

    void BoardInfo()
    {
        if (Input.GetMouseButtonDown(1) && !infoClicked)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == bombCollider)
                    PanelInfo("bomb");
                else if (hit.collider.gameObject == bowCollider)
                    PanelInfo("bow");
                else if (hit.collider.gameObject == mapCollider)
                    PanelInfo("map");
                else if (hit.collider.gameObject == shieldCollider)
                    PanelInfo("shield");
                else if (hit.collider.gameObject == knightCollider)
                    PanelInfo("knight");
                else if (hit.collider.gameObject == gritCollider)
                    PanelInfo("grit");
            }
        }
        else if(Input.GetMouseButton(0) && infoClicked)
        {
            infoClicked = false;
            TurnPanelOff(currentlyOpenPanel);
            currentlyOpenPanel = null;
        }
    }

    void PanelInfo(string name)
    {
        GameObject panel = ReturnPanel(name);
        currentlyOpenPanel = panel;
        panel.SetActive(true);
        infoClicked = true;
        StartCoroutine(EnlargeImages(panel.transform.GetChild(0), panel.transform.GetChild(1), name));
    }
    void TurnPanelOff(GameObject panel)
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
