using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float cameraVelocity;
    [SerializeField] public float cameraSizeChangeVelocity;
    [SerializeField] public float minimumCameraSize;

    private CustomInput custonInput;
    private Vector2 movementInput;
    private float scrollInput;
    private Camera cam;

    void Awake()
    {
        custonInput = new CustomInput();
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // read AWSD input
        movementInput = custonInput.MapControl.CameraMovementAWSD.ReadValue<Vector2>();

        //read scroll input
        scrollInput = custonInput.MapControl.CameraCloseIn.ReadValue<float>();

        cameraPositionMove();
    }
    
    private void cameraPositionMove()
    {
        cam.transform.localPosition += new Vector3(movementInput.x * cameraVelocity, movementInput.y * cameraVelocity, 0f);

    }

    public void cameraZoom()
    {
        cam.orthographicSize -= (scrollInput * cameraSizeChangeVelocity);
        cam.orthographicSize = cam.orthographicSize > minimumCameraSize? cam.orthographicSize : minimumCameraSize;
    }


    private void OnCameraClosing()
    {
        Debug.Log("CameraClossin");
    }

    private void OnEnable()
    {
        custonInput.MapControl.Enable();
    }

    private void OnDesable()
    {
        custonInput.MapControl.Disable();  
    }


}
