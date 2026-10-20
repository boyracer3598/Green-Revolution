using UnityEngine;
using UnityEngine.InputSystem;

public class DisplayBuildingDetails : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    InputAction MouseLeftClick;
    BuildingObject HitBuidling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MouseLeftClick = InputSystem.actions["build"];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,LayerMask.GetMask("Building"))&MouseLeftClick.WasPressedThisFrame())
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.gameObject);
        }
    }
}
