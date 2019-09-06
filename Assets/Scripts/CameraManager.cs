using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class CameraManager : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 1;
    static Transform currentView;

    private void LateUpdate()
    {
        if(currentView != null)
            CameraMovement();
    }

    private void CameraMovement()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, currentView.eulerAngles, Time.deltaTime * transitionSpeed);
        if (transform.position == currentView.position)
            currentView = null;
    }

    public static void SetCurrentView(Transform cameraView)
    {
        currentView = cameraView;
    }    
}

#pragma warning restore 0649
