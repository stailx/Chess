using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private Camera cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = cameras[0];
    }
    public Camera GetCamera()
    {
        return cameraMain;
    }
    void SetCamera(int number)
    {
        cameraMain = cameras[number];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(number == i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCamera(1);
        }
    }
}
