using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket_ship : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float thrustforce = 100f;
    [SerializeField] float mainthrust = 10f;
    [SerializeField] AudioClip main,death,nextlevel;
    [SerializeField] ParticleSystem mainP, deathP, nextlevelP;
    int n ;
    [SerializeField] float LevelloadDelay = 1f;

    enum State  {alive,death,wait};
    State state = State.alive;
    Rigidbody rigidbody;
    AudioSource audio;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        //firstlevel();

    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.alive)
        {
            ProcessInput();
        }
       
    }

    void ProcessInput()
    {
        Thrust();
        Rotate();

    }
    void OnCollisionEnter(Collision collision)
    {
        if(state != State.alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "friendly":
                break;
            case "Finish":
                newlevel();
                break;
            default:
                startagain();
                break;
        }

    }

    private void startagain()
    {
        state = State.death;
        audio.Stop();
        print("dead");
        Invoke("dead", LevelloadDelay);
        audio.PlayOneShot(death);
        deathP.Play();
    }

    private void newlevel()
    {
        state = State.wait;
        audio.Stop();
        print("Hit Finish");
        Invoke("LoadNextscene", LevelloadDelay);
        audio.PlayOneShot(nextlevel);
        nextlevelP.Play();
        
    }

    private void dead()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextscene()
    {
        
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextsceneindex = currentsceneIndex+1;
        if (nextsceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextsceneindex = 0;
        }
        SceneManager.LoadScene(nextsceneindex);

    
       

    }
    

    private void Rotate()
    {
        
        float rotationspeed = thrustforce * Time.deltaTime;
        rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationspeed);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationspeed);

        }
        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainthrust);
            if (!audio.isPlaying)
            {
                audio.PlayOneShot(main);
                mainP.Play();//to play the audio
            }


        }
        else
        {
            audio.Stop();
            mainP.Stop();

        }
    }
}
