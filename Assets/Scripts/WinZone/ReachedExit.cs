using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedExit : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.GameOver(true);
        }
    }
}
