using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class controller : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Diference = (cam.ScreenToWorldPoint(Input.mousePosition)) - cam.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            cam.transform.position = Origin - Diference;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            cam.transform.position = ResetCamera;
            cam.orthographicSize = 6;
        }
        ResetCamera = new Vector3(this.transform.position.x, this.transform.position.y, -10);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize++;
        }
    }
}
