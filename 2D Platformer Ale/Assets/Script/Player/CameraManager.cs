using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] int startCameraIndex;
    [SerializeField] CinemachineVirtualCamera[] cameras;
    [SerializeField] GameObject[] cameraZones;
    private void Awake()
    {
        //cameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetCamera(startCameraIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == index)
            {
                cameras[i].Priority = 10;
            }
            else
            {
                cameras[i].Priority = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < cameraZones.Length; i++)
        {
            if (collision.gameObject == cameraZones[i])
            {
                SetCamera(i);
            }
        }
        
    }
}
