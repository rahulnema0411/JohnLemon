using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding endGame;

    bool isPlayerinRange;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerinRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            isPlayerinRange = false;
        }
    }

    void Update()
    {
        if(isPlayerinRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position ,  direction);

            RaycastHit hit;
            if(Physics.Raycast(ray , out hit))
                {
                    if(hit.collider.transform == player)
                    {
                        endGame.isCaught();
                    }
                }
        }
    }
}
