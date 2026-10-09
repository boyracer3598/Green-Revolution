using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float CameraMoveSpeed = 0.1F;
    Vector2 mousePosition;
    Vector3 mousePositionVec3;
    
    InputAction mouseMove;
    InputAction mouseClick;
    InputAction cameraZoom;
    InputAction cameraMove;
    InputAction cameraRotate;
    public Camera mainCamera;
    public float zoomSpeed = 0.2f;
    public float minZoom, maxZoom;

    public GameObject northCameraPos;
    public GameObject southCameraPos;
    public GameObject eastCameraPos;
    public GameObject westCameraPos;
    
   
    public enum cameraDirection
    {
        EAST, WEST, NORTH, SOUTH
    }
    // current Direction of camera
    public cameraDirection currentCamDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setup input
        mouseMove = InputSystem.actions["MouseMove"];
        mouseClick = InputSystem.actions["MouseClick"];
        cameraZoom = InputSystem.actions["Zoom"];
        cameraMove = InputSystem.actions["cameraMove"];
        cameraRotate = InputSystem.actions["cameraRotate"];
        currentCamDir = cameraDirection.NORTH;
        mainCamera = GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {

        //zoom camera
        mainCamera.orthographicSize = Mathf.Clamp((mainCamera.orthographicSize + cameraZoom.ReadValue<float>() * zoomSpeed), minZoom, maxZoom);

        //pan camera with mouse
        mousePosition = mouseMove.ReadValue<Vector2>();
        mousePositionVec3 = new Vector3(mousePosition.x, 0, mousePosition.y);
        if (mouseClick.IsPressed())
        {
            this.transform.Translate(mousePositionVec3*CameraMoveSpeed,Space.Self);
        }

        // move camera with keyboard
        if (cameraMove.IsPressed())
        {
            Vector2 cameraMoveV2= cameraMove.ReadValue<Vector2>();
            this.transform.Translate(new Vector3(cameraMoveV2.x,0,cameraMoveV2.y)*CameraMoveSpeed, Space.Self);
        }

        //rotate camera
        if (cameraRotate.WasPressedThisFrame())
        {
            switchCameraAngle();
        }


    }
    //chnages the agnle by 90 degress
    public void switchCameraAngle() {
        switch (currentCamDir)
        {
            case cameraDirection.NORTH:
                Debug.Log("cam is east");
                currentCamDir = cameraDirection.EAST;
                break;
            case cameraDirection.EAST:
                Debug.Log("cam is south");
                currentCamDir = cameraDirection.SOUTH;
                break;
            case cameraDirection.SOUTH:
                Debug.Log("cam is west");
                currentCamDir= cameraDirection.WEST;
                break;
            case cameraDirection.WEST:
                Debug.Log("cam is north");
                currentCamDir=cameraDirection.NORTH;
                break;
        }
    }


}
