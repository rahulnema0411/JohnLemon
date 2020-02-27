using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    float horizontal_movement;
    float vertical_movement;
    float turn_speed = 20f;
    public float speed = 1f;

    Vector3 movement;
    Vector3 desired_Forward;

    Quaternion rotation = Quaternion.identity;

    bool isWalking = false;
    bool is_horizontallyWalk;
    bool is_VerticallyWalk;
    bool isPaused = false;

    Animator anim;
    Rigidbody rigidbody;
    AudioSource audio;
    public GameObject pause;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal_movement, 0, vertical_movement);
        movement.Normalize();

        isWalking = !Mathf.Approximately(horizontal_movement, 0f) || !Mathf.Approximately(vertical_movement, 0f);

        anim.SetBool("IsWalking", isWalking);
        if(isWalking)
        {
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }

        desired_Forward = Vector3.RotateTowards(transform.forward, movement, turn_speed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desired_Forward);


    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void OnAnimatorMove()
    {
        rigidbody.MovePosition(rigidbody.position + movement * anim.deltaPosition.magnitude*speed);
        rigidbody.MoveRotation(rotation);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        isPaused = true;
        
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("resume");
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Quit..");
        Application.Quit();
    }
}
