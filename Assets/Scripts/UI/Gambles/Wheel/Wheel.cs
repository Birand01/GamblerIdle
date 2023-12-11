using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wheel : MonoBehaviour
{
    [SerializeField] private float rotationAmount,rotationDuration;
    internal bool isSpinButtonEnable=true;
    float randomAngle;
    [SerializeField] internal List<GameObject> sectors;
    [SerializeField] private Transform sectorsParent;
    private void OnEnable()
    {
        SpinButton.OnSpinWheel += SpinWheel;
       
    }
    private void OnDisable()
    {
        SpinButton.OnSpinWheel -= SpinWheel;

    }
    private void Awake()
    {
        SetSectors();
    }


    private void SetSectors()
    {
        for (int i = 0; i < sectorsParent.childCount; i++)
        {
            sectors.Add(sectorsParent.transform.GetChild(i).gameObject);
        }
    }
    private void SpinWheel(Button spinButton)
    {
        isSpinButtonEnable = false;
        randomAngle = Random.Range(0, 360);
        Debug.Log(randomAngle);
        Debug.Log("Sector is " +GetSector(randomAngle));
        float rotateAngles = (360 * rotationAmount) + randomAngle; 
        this.transform.DOLocalRotate(new Vector3(0, 0, rotateAngles * -1), rotationDuration, RotateMode.WorldAxisAdd)
            .onComplete += OnSpinWheelEventHandler;
    }
    private SectorConfiguration GetSector(float angle)
    {
        int anglePerSector =30;
        return sectors[(int)((angle) / anglePerSector)].gameObject.GetComponent<SectorConfiguration>();
    }
    private void AssignSector(float angle)
    {
        int[] sectorsAngles = new int[sectors.Count];

        // Fill the necessary angles (for example if we want to have 12 sectors we need to fill the angles with 30 degrees step)
        // It's recommended to use the EVEN sectors count (2, 4, 6, 8, 10, 12, etc)
        for (int i = 1; i <= sectors.Count; i++)
        {
            sectorsAngles[i - 1] = 360 / sectors.Count * i;
            Debug.Log("Sector name is "+sectors[i - 1].name +""+"Angle of this sector is " + sectorsAngles[i - 1]);
        }
        if(Mathf.Abs(angle)>0 && Mathf.Abs(angle)<30)
        {

        }
    }
    private void OnSpinWheelEventHandler()
    {
        isSpinButtonEnable = true;
    }
    

}
