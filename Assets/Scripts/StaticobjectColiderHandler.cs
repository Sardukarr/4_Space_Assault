using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticobjectColiderHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject SmallExplosionFX;
    private void OnParticleCollision(GameObject other)
    {
       // Instantiate(SmallExplosionFX, other, Quaternion.identity);

    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(SmallExplosionFX, other.transform.position, Quaternion.identity);
    }
}
