using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public GameObject player;

    public float fadeDuration = 1f;
    public float lastSight = 1f;
    float timer = 0f;

    bool hasArrived;
    bool hasSpotted;
    bool hasPlayed;

    public CanvasGroup canvasGroup;
    public CanvasGroup caughtCanvasGroup;

    public AudioSource Win;
    public AudioSource Lose;

    // Update is called once per frame
    void Update()
    {
        if(hasArrived)
        {
            EndGame(canvasGroup, false , Win);
        }
        else if(hasSpotted)
        {
            EndGame(caughtCanvasGroup, true , Lose);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            hasArrived = true;
        }
    }

    void EndGame(CanvasGroup canvas , bool restart , AudioSource audio)
    {
        if(!hasPlayed)
        {
            audio.Play();
            hasPlayed = true;
        }
        timer += Time.deltaTime;
        canvas.alpha = timer / fadeDuration;

        if(timer > fadeDuration + lastSight)
        {
            if (!restart)
            {
                SceneManager.LoadScene("MainMenu");
            }

            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void isCaught()
    {
        hasSpotted = true;
    }
}
