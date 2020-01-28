using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [Tooltip("in ms^-1")][SerializeField] float Speed = 12f;
    [Tooltip("in ms")] [SerializeField] float xRange = 5.5f;
    [Tooltip("in ms")] [SerializeField] float yRange = 4f;

    [SerializeField] float posiotionPitchFactor = -5f;
    [SerializeField] float posiotionYawFactor = -5f;
    [SerializeField] float ControlPitchFactor = -20f;
    [SerializeField] float ControlRollFactor = -24f;
    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = CalculatePosition();
        CalculateRotation();



    }

    private void CalculateRotation()
    {
        float pitchDueToPosition = posiotionPitchFactor * transform.localPosition.y;
        float pitchDueToControlThrow = yThrow * ControlPitchFactor;
        float pitch= pitchDueToPosition + pitchDueToControlThrow;
        float yaw= posiotionYawFactor * transform.localPosition.x;
        float roll = xThrow * ControlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    Vector3 CalculatePosition()
    {
         xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
         yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;
        float rawX = transform.localPosition.x + xOffset;
        float rawY = transform.localPosition.y + yOffset;
        return new Vector3(Mathf.Clamp(rawX, -xRange, xRange), Mathf.Clamp(rawY, -yRange, yRange), transform.localPosition.z);
    }
}
