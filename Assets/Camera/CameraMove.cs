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

        
        // reads input for camera pan(mouse and WASD)
        mousePosition = mouseMove.ReadValue<Vector2>();
        Vector2 cameraInputKeyboard = cameraMove.ReadValue<Vector2>();
        switch (currentCamDir)
        {
            case cameraDirection.NORTH:
                
                if (mouseClick.IsPressed())
                {
                    CameraPan(mousePosition.x, mousePosition.y);
                } else if (cameraMove.IsPressed()){
                    CameraPan(cameraInputKeyboard.x, cameraInputKeyboard.y);
                }
                break;
            case cameraDirection.EAST:
                if (mouseClick.IsPressed())
                {
                    CameraPan(mousePosition.y, -mousePosition.x);
                } else if (cameraMove.IsPressed()){
                    CameraPan(cameraInputKeyboard.y, -cameraInputKeyboard.x);
                }
                break;
            case cameraDirection.SOUTH:
                if (mouseClick.IsPressed())
                {
                    CameraPan(-mousePosition.x, -mousePosition.y);
                } else if (cameraMove.IsPressed()){
                    CameraPan(-cameraInputKeyboard.x, -cameraInputKeyboard.y);
                }
                break;
            case cameraDirection.WEST:
                if (mouseClick.IsPressed())
                {
                    CameraPan(-mousePosition.y, mousePosition.x);
                } else if (cameraMove.IsPressed()){
                    CameraPan(-cameraInputKeyboard.y, cameraInputKeyboard.x);
                }
                break;
        }
       

        //rotate camera
        if (cameraRotate.WasPressedThisFrame())
        {
            switchCameraAngle();
        }
        
        
        //dedug drawray
        Debug.DrawRay(northCameraPos.transform.position,northCameraPos.transform.forward*1000,Color.coral,1);
        Debug.DrawRay(southCameraPos.transform.position,southCameraPos.transform.forward*1000,Color.pink,1);
        Debug.DrawRay(eastCameraPos.transform.position,eastCameraPos.transform.forward*1000,Color.green,1);
        Debug.DrawRay(westCameraPos.transform.position,westCameraPos.transform.forward*1000,Color.blue,1);


    }


    public void CameraPan(float posX,float posY){
        this.transform.Translate(new Vector3(posX,0,posY) * CameraMoveSpeed, Space.Self);
    }
    //changes the agile by 90 degrees
    public void switchCameraAngle() {
        switch (currentCamDir)
        {
            case cameraDirection.NORTH:
                mainCamera.transform.position=eastCameraPos.transform.position;
                mainCamera.transform.rotation=eastCameraPos.transform.rotation;
                currentCamDir = cameraDirection.EAST;
                break;
            case cameraDirection.EAST:
                mainCamera.transform.position=southCameraPos.transform.position;
                mainCamera.transform.rotation=southCameraPos.transform.rotation;
                currentCamDir = cameraDirection.SOUTH;
                break;
            case cameraDirection.SOUTH:
                mainCamera.transform.position=westCameraPos.transform.position;
                mainCamera.transform.rotation=westCameraPos.transform.rotation;
                currentCamDir= cameraDirection.WEST;
                break;
            case cameraDirection.WEST:
                mainCamera.transform.position=northCameraPos.transform.position;
                mainCamera.transform.rotation=northCameraPos.transform.rotation;
                currentCamDir=cameraDirection.NORTH;
                break;
        }
    }


}
