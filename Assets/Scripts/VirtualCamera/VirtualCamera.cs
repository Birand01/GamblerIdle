using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;
using System.Linq;
using System;
using Unity.VisualScripting;

public enum CameraType
{
    mainCamera,
    diceGameCamera
}
public class VirtualCamera : MonoBehaviour
{
    List<VirtualCameraController> virtualCameraControllerList;
    internal VirtualCameraController lastActiveCamera;


    private void Awake()
    {
        virtualCameraControllerList = GetComponentsInChildren<VirtualCameraController>().ToList();
        virtualCameraControllerList.ForEach(x => x.gameObject.GetComponent<CinemachineVirtualCamera>().Priority=0);
        SwitchCamera(CameraType.mainCamera);
    }
    private void OnEnable()
    {
        ExitDiceGameButton.OnSwitchMainCamera += SwitchCamera;
        DiceGameInteraction.OnSwitchDiceGameCamera += SwitchCamera;
    }
    private void OnDisable()
    {
        DiceGameInteraction.OnSwitchDiceGameCamera -= SwitchCamera;
        ExitDiceGameButton.OnSwitchMainCamera -= SwitchCamera;

    }
    private void SwitchCamera(CameraType cameraType)
    {
        if (lastActiveCamera != null)
        {
            lastActiveCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }
        VirtualCameraController desiredCamera = virtualCameraControllerList.Find(x => x.cameraType == cameraType);
        if (desiredCamera != null)
        {
            desiredCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
            lastActiveCamera = desiredCamera;
        }
        else
        {
            Debug.LogWarning("The desired camera was not found");
        }
    }

   
   
}












