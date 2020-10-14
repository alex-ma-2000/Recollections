
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject controller;
    private ControllerInputScript controls;
    private bool paused = false;
    ProgressManager pm;

    public GameObject player;

    public Vector3 resetPoint;

    //  GameObject panel;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        ControllerInputScript controls = controller.GetComponent<ControllerInputScript>();
    }

    void Start()
    {
        pm = ProgressManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
        else if (controller.GetComponent<ControllerInputScript>().getStartReleased())
        {
            if (!paused) {
                pm.pause();
                Debug.Log("Game paused");
                paused = true;
            } 
            else if (paused)
            {
                pm.unpause();
                Debug.Log("Game unpaused");
                paused = false;
            }
        }
        else if (controller.GetComponent<ControllerInputScript>().getRightBumperDown())
        {
            player.transform.position = resetPoint;
        }

        if (Time.timeScale == 0 && controller.GetComponent<ControllerInputScript>().getRightBumperDown())
        {
            pm.unpause();

            Debug.Log("Game unpaused");
        } 
           //  SceneManager.LoadScene(SceneManager.GetActiveScene().name); // put this on some "restart" button

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

//    public GameObject GetController()
//    {
//        return controller;
//    }

}
