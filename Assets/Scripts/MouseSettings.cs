using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSettings : MonoBehaviour {
    
    [SerializeField]
    float sensitivityX = 15f;
    [SerializeField]
    float sensitivityY = 15f;
    [SerializeField]
    float minimumX = -360f;
    [SerializeField]
    float maximumX = 360f;
    [SerializeField]
    float minimumY = -360f;
    [SerializeField]
    float maximumY = 360f;


    private void LateUpdate()
    {
        if (MouseLook.mLsensitivityX != sensitivityX)
            MouseLook.mLsensitivityX = sensitivityX;

        if (MouseLook.mLsensitivityY != sensitivityY)
            MouseLook.mLsensitivityY = sensitivityY;

        if (MouseLook.mLminimumX != minimumX)
            MouseLook.mLminimumX = minimumX;

        if (MouseLook.mLminimumY != minimumY)
            MouseLook.mLminimumY = minimumY;

        if (MouseLook.mLmaximumX != maximumX)
            MouseLook.mLmaximumX = maximumX;

        if (MouseLook.mLmaximumY != maximumY)
            MouseLook.mLmaximumY = maximumY;
    }
}
