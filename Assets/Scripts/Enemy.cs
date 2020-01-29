using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enamyDeathFX;
    [SerializeField] Transform parent;
    [SerializeField] int ScoreValue = 25;

    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<BoxCollider>().isTrigger=false;
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject explosion = Instantiate(enamyDeathFX, transform.position, Quaternion.identity);
        explosion.transform.parent = parent;
        scoreBoard.ScoreHit(ScoreValue);
        Destroy(gameObject,0.2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(enamyDeathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
