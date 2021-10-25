using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToFind : MonoBehaviour
{

    private GameManager gameManager;
    public int objectValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateScore(objectValue);
            Destroy(gameObject);
            Debug.Log("value; " + objectValue);
        }
    }

}
