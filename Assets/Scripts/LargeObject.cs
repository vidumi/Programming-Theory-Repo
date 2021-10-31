using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeObject : ObjectsToFind
{
    private GameManager gameManager;

    public AudioClip clipToFound;
    public AudioClip audioLargeObject;


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
        StartCoroutine (Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.01f);
        gameManager.ResetTimer();
        gameManager.StopSound();
        gameManager.SoundClip(clipToFound);
    }

    protected override void NewObject()
    {
        gameManager.Info("You have "+ gameManager.timeLeft + " seconds to search for the large object");
        Debug.Log("Trovare Large Object");
        gameManager.SoundClip(audioLargeObject);

        gameManager.IsTimer();
    }
}
