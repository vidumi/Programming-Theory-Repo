using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumObject : ObjectsToFind
{
    private GameManager gameManager;

    public AudioClip clipToFound;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        NewObject();
    }

    protected override void ObjectFound()
    {
        Destroy(gameObject, 0.1f);
        gameManager.UpdateScore(objectValue);
        gameManager.Info("look for ...");
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.01f);
        gameManager.SoundClip(clipToFound);
    }

    protected override void NewObject()
    {
        gameManager.Info("look for Medium object");
    }
}
