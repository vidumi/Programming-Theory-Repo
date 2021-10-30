using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectsToFind : MonoBehaviour
{
    public int objectValue = 2;
    private ParticleSystem explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            explosion = GameObject.FindObjectOfType<ParticleSystem>();
            explosion.Play();
            ObjectFound();
        }
    }

    protected abstract void ObjectFound();

    protected abstract void NewObject();

}
