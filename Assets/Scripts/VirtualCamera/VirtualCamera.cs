using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineTransposer transposer;
   
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        transposer=cam.GetCinemachineComponent<CinemachineTransposer>();
    }


    //private void DiceGameCamPosition()
    //{
    //    transposer.m_XDamping = 0;
    //    transposer.m_YawDamping = 0;
    //    transposer.m_ZDamping = 0;
    //    transposer.m_FollowOffset.x = diceGameCameraPosition.localPosition.x;
    //    transposer.m_FollowOffset.y = diceGameCameraPosition.localPosition.y;
    //    transposer.m_FollowOffset.z = diceGameCameraPosition.localPosition.z;

    //}

}
