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
    [SerializeField] bool zoomedIn = false;

    [Header("BOARD INFO CACHE", order = 2)]
    [SerializeField] GameObject colliders;
    [Header("_Bomb", order = 3)]
    [SerializeField] GameObject bombCollider;
    [SerializeField] GameObject bombPanel;
    [Header("_Bow", order = 4)]
    [SerializeField] GameObject bowCollider;
    [SerializeField] GameObject bowPanel;


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
                    hit.collider.gameObject == bowCollider)
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
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == bombCollider)
            {
                bombPanel.SetActive(true);
                EnlargeImages(bombPanel.transform.GetChild(0), bombPanel.transform.GetChild(1));
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == bowCollider)
            {
                bowPanel.SetActive(true);
            }
        }
    }

    void EnlargeImages(Transform image_1, Transform image_2)
    {
        RectTransform image1 = image_1.GetComponent<RectTransform>();
        RectTransform image2 = image_2.GetComponent<RectTransform>();

        image1.sizeDelta = image1.sizeDelta * 3;
        image1.localPosition = new Vector2(0, image1.sizeDelta.y / 2);
       
        image2.sizeDelta = image2.sizeDelta * 3;
        image2.localPosition = new Vector2(0, -image2.sizeDelta.y /2);
    }
}
