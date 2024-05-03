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
    private GameObject nodeObjectSelected;
    public GameObject nodeObjectSelected0;
    public GameObject nodeObjectSelected1;

    public GameObject selectionIndicatorPrefab; 
    private GameObject[] selectionIndicator;
    private GameObject lastSelectedNode;

    void Awake()
    {
        custonInput = new CustomInput();
        cam = GetComponent<Camera>();
        lastSelectedNode = null;
        nodeObjectSelected0 = null;
        nodeObjectSelected1 = null;
        selectionIndicator = new GameObject[2];
    }

    private void Update()
    {
        // Click on object
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                nodeObjectSelected = hit.collider.gameObject;

                if (nodeObjectSelected.GetComponent<Node>() != null)
                {
                    // If the selected object is a not already selected Node
                    if (nodeObjectSelected != nodeObjectSelected0 & nodeObjectSelected != nodeObjectSelected1)
                    {

                        if (nodeObjectSelected0 == null)
                        {
                            nodeObjectSelected0 = nodeObjectSelected;
                            Debug.Log(nodeObjectSelected0);
                            ShowSelectionIndicator(nodeObjectSelected0, 0);
                        }
                        else
                        {
                            nodeObjectSelected1 = nodeObjectSelected;
                            ShowSelectionIndicator(nodeObjectSelected1, 1);
                        }

                    }
                    else
                    {
                        if (nodeObjectSelected == nodeObjectSelected0)
                        {
                            nodeObjectSelected0 = null;
                            HideSelectionIndicator(0);
                        }
                        else
                        {
                            nodeObjectSelected1 = null;
                            HideSelectionIndicator(1);
                        }
                    }
                    lastSelectedNode = nodeObjectSelected;
                }
            }
            
        }

        // read AWSD input
        movementInput = custonInput.MapControl.CameraMovementAWSD.ReadValue<Vector2>();

        //read scroll input
        scrollInput = custonInput.MapControl.CameraCloseIn.ReadValue<float>();


        cameraPositionMove();
    }
    
    private void cameraPositionMove()
    {
        cam.transform.localPosition += new Vector3(movementInput.x * cameraVelocity, movementInput.y * cameraVelocity, 0f) * cam.orthographicSize/150;

    }

    public void cameraZoom()
    {
        cam.orthographicSize -= (scrollInput * cameraSizeChangeVelocity);
        cam.orthographicSize = cam.orthographicSize > minimumCameraSize? cam.orthographicSize : minimumCameraSize;


        // Scale selection indicators with zoom
        if (selectionIndicator[0] != null)
        {
            selectionIndicator[0].transform.localScale = new Vector3(cam.orthographicSize, cam.orthographicSize, 1) /10;
        }
        if (selectionIndicator[1] != null)
        {
            selectionIndicator[1].transform.localScale = new Vector3(cam.orthographicSize, cam.orthographicSize, 1) / 10;
        }
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

    void ShowSelectionIndicator(GameObject selectedObject, int selection)
    {
        // Si no existe un objeto visual de selección, instanciarlo
        if (selectionIndicator[selection] == null)
        {
            selectionIndicator[selection] = Instantiate(selectionIndicatorPrefab, selectedObject.transform.position, Quaternion.identity);
            Debug.Log(selectionIndicator[selection]);
            selectionIndicator[selection].GetComponent<SpriteRenderer>().color = selection == 0? Color.blue: Color.red;
            Debug.Log(selection == 0 ? Color.blue : Color.red);
        }

        // Posicionar el objeto visual de selección sobre el objeto seleccionado
        selectionIndicator[selection].transform.position = selectedObject.transform.position;

        // Activar el objeto visual de selección para que sea visible
        selectionIndicator[selection].SetActive(true);
    }
    void HideSelectionIndicator(int selection) {
        Destroy(selectionIndicator[selection]);
        selectionIndicator[selection] = null;
    }
}
