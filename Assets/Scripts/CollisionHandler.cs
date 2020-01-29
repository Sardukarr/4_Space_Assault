using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject ExplosionFX;
    [SerializeField] float levelLoadDelay=2f;
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }
    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        ExplosionFX.transform.localPosition = transform.localPosition;
        ExplosionFX.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInParent<BetterWaypointFollower>().routeSpeed = 1f;
        ParticleSystem[] bullets = GetComponentsInChildren<ParticleSystem>();
        foreach(var bullet in bullets)
        {
            bullet.Stop();
        }
        Invoke("LoadFirstScene", levelLoadDelay);

    }
}
