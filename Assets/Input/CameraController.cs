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
    private GameObject nodeObjectSelected0;
    private GameObject nodeObjectSelected1;

    public GameObject selectionIndicatorPrefab; 
    private GameObject[] selectionIndicator;
    private GameObject lastSelectedNode;

    void Awake()
    {
        custonInput = new CustomInput();
        cam = GetComponent<Camera>();
        lastSelectedNode = null;

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
                // If the selected object isa Node
                nodeObjectSelected = hit.collider.gameObject;
                if (nodeObjectSelected.GetComponent<Node>() != null)
                {   
                    if (nodeObjectSelected == nodeObjectSelected0)
                    {
                        DestroySelectionIndicator(0);
                        nodeObjectSelected0 = null;
                    }
                    else
                    {
                        if (nodeObjectSelected == nodeObjectSelected1)
                        {
                            DestroySelectionIndicator(1);
                            nodeObjectSelected1 = null;
                        }
                        else
                        {
                            if (nodeObjectSelected0 == null)
                            {
                                nodeObjectSelected0 = nodeObjectSelected;
                                Node nodeSelected = nodeObjectSelected0.GetComponent<Node>();
                                ShowSelectionIndicator(nodeObjectSelected0, 0);
                                lastSelectedNode = nodeObjectSelected0;
                            }
                            else
                            {
                                if (nodeObjectSelected1 == null)
                                {
                                    nodeObjectSelected1 = nodeObjectSelected;
                                    Node nodeSelected = nodeObjectSelected1.GetComponent<Node>();
                                    ShowSelectionIndicator(nodeObjectSelected1, 0);
                                    lastSelectedNode = nodeObjectSelected1;
                                }
                                else
                                {
                                    if(lastSelectedNode == nodeObjectSelected1)
                                    {
                                        DestroySelectionIndicator(0);
                                        nodeObjectSelected0 = nodeObjectSelected;
                                        ShowSelectionIndicator(nodeObjectSelected0,0);
                                        lastSelectedNode = nodeObjectSelected0;
                                    }
                                    else
                                    {
                                        DestroySelectionIndicator(1);
                                        nodeObjectSelected1 = nodeObjectSelected;
                                        ShowSelectionIndicator(nodeObjectSelected1, 1);
                                        lastSelectedNode = nodeObjectSelected1;
                                    }
                                }
                            }
                        }
                    }
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

    void ShowSelectionIndicator(GameObject selectedObject, int selection)
    {
        // Si no existe un objeto visual de selección, instanciarlo
        if (selectionIndicator[selection] == null)
        {
            selectionIndicator[selection] = Instantiate(selectionIndicatorPrefab, selectedObject.transform.position, Quaternion.identity);
        }

        // Posicionar el objeto visual de selección sobre el objeto seleccionado
        selectionIndicator[selection].transform.position = selectedObject.transform.position;

        selectionIndicator[0].GetComponent<SpriteRenderer>().color = Color.blue;
        selectionIndicator[1].GetComponent<SpriteRenderer>().color = Color.red;

        // Activar el objeto visual de selección para que sea visible
        selectionIndicator[selection].SetActive(true);
    }

    void DestroySelectionIndicator(int d)
    {
        Destroy(selectionIndicator[d]);
        selectionIndicator[d] = null;
    }

}
