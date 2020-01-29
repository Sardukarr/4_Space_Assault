using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    [Header("Genral")]
    [Tooltip("in ms^-1")][SerializeField] float Speed = 12f;
    [Tooltip("in ms")] [SerializeField] float xRange = 5.5f;
    [Tooltip("in ms")] [SerializeField] float yRange = 4f;

    [Header("ScreenPosition")]
    [SerializeField] float posiotionPitchFactor = -5f;
    [SerializeField] float posiotionYawFactor = -5f;
    [Header("ControlThrow")]
    [SerializeField] float ControlPitchFactor = -20f;
    [SerializeField] float ControlRollFactor = -24f;
    float xThrow, yThrow;
    bool isPlayerDead = false;
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
        {
            transform.localPosition = CalculatePosition();
            transform.localRotation = CalculateRotation();
        }


    }

    private void OnPlayerDeath()
    {
        isPlayerDead = true;
        print("gotcha");
    }
    private Quaternion CalculateRotation()
    {
        float pitchDueToPosition = posiotionPitchFactor * transform.localPosition.y;
        float pitchDueToControlThrow = yThrow * ControlPitchFactor;
        float pitch= pitchDueToPosition + pitchDueToControlThrow;
        float yaw= posiotionYawFactor * transform.localPosition.x;
        float roll = xThrow * ControlRollFactor;
        return Quaternion.Euler(pitch, yaw, roll);
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
