using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{

    //TODO: dificulty levels, speed upon a key, information about waypoints
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
    ParticleSystem[] bullets;
    // Start is called before the first frame update
    void Start()
    {
         bullets = GetComponentsInChildren<ParticleSystem>();
        Startfiring(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDead)
        {
            transform.localPosition = CalculatePosition();
            transform.localRotation = CalculateRotation();
        }
        FireAtWill();

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
    void FireAtWill()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
            Startfiring(true);
        else
            Startfiring(false);
    }
    void Startfiring(bool shouldStartFiring)
    {
        if (shouldStartFiring)
        {
            foreach (var bullet in bullets)
            {
                if (bullet.isStopped)
                    bullet.Play();
            }
        }
        else
        {
            foreach (var bullet in bullets)
            {
                if (!bullet.isStopped)
                    bullet.Stop();

            }
        }
    }
}
