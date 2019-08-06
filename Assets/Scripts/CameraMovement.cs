using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement _cam;
    Vector3 boardPosition;
    Vector3 target;
    Vector3 camAngle;
    bool go = false;
    float cameraSpeed = 3.5f;

    private void Awake()
    {
        _cam = this;
    }

    void Update()
    {
        if (go)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, cameraSpeed);
            transform.LookAt(boardPosition, Vector3.up);
            go = (transform.position == target) ? false : true;
            if (!go)
                transform.eulerAngles = camAngle;
        }
    }

    public void SetBoardValues(Vector3 bPosition, Vector3 point, Vector3 cameraAngle)
    {
        boardPosition = bPosition;
        target = point;
        camAngle = cameraAngle;

        go = true;
    }
}
