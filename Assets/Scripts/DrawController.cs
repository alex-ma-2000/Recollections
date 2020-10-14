﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawController : MonoBehaviour {
    public float radius;
    public GameObject linePrefab;
    public GameObject softLinePrefab;
    private Line activeLine;
    private Line softLine;
    private Vector3 cursorPos;
    private float lerpFract;
    private float angle;
    public GameObject controller;
    private ControllerInputScript controls;
    private PlayerController player;
    private bool paused;
    private bool circleDrawn;

    private Light glow;
    public float lightRange;
    public float lerpLight;


    // Use this for initialization
    void Start () {
		cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lerpFract = 0.3f;
        angle = Vector3.Angle(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position, transform.position - transform.parent.position);
        controls = controller.GetComponent<ControllerInputScript>();
        player = transform.parent.GetComponent<PlayerController>();

        GameObject softLineObject = Instantiate(softLinePrefab);
        softLine = softLineObject.GetComponent<Line>();
        softLine.UpdateLine(transform.position);

        glow = (Light)GetComponent("Light");
        glow.enabled = false;
        glow.range = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool stunned = transform.parent.GetComponent<PlayerController>().stunned;
        if (stunned || paused)
        {
            AkSoundEngine.PostEvent("Set_State_Surfing_Off", gameObject);
        }
        if (!paused && !stunned) {
            //if (controls.getADown()) {
            //    transform.parent.gameObject.GetComponent<PlayerController>().destroyEntireTrail();
            //    Camera.main.GetComponent<Shake>().shakeDuration = 0.5f;

            //}

            if (softLine != null)
            {
                softLine.UpdateLine(transform.position);
                List<Vector2> points = softLine.GetPoints();
                if (points.Count > 0)
                {
                    for (int i = 0; i < points.Count - 10; i++)
                    {
                        if (Vector2.Distance(points[points.Count - 1], points[i]) < 0.5f && !circleDrawn)
                        {
                            circleDrawn = true;
                            softLine.GetComponent<SoftLine>().NeuronCircled(transform.position, transform.up);
                            Debug.Log("Circle Drawn!");
                            circleDrawn = false;
                        }
                    }
                }
            }

            if (controls.getRightTriggerDown()) //generating a few false positives at first
            {
                //Debug.Log("Drawing??");
                player.destroyEntireTrail();
                GameObject line = Instantiate(linePrefab);
                activeLine = line.GetComponent<Line>();
                AkSoundEngine.PostEvent("Set_State_Surfing_On", gameObject);

                LightOn();
            }
            if (controls.getRightTriggerUp())
            {
                activeLine = null;
                AkSoundEngine.PostEvent("Set_State_Surfing_Off", gameObject);

                LightOff();
            }
            if (activeLine != null)
            {
                activeLine.UpdateLine(transform.position);
            }
            float joystickX = controls.getLeftStickX();
            float joystickY = controls.getLeftStickY();
            if(joystickX == 0)
            {
                joystickX = -1;
            }
            Vector3 joystickVec = new Vector3(joystickX, joystickY, 0);
            Vector3 ballVec = transform.position - transform.parent.position;
            angle = Vector2.Angle(joystickVec, ballVec);
            if (angle >= 30)
            {
                lerpFract = 0.2f;
            }
            else
            {
                lerpFract = 0.4f;
            }
            cursorPos = Vector3.Lerp(transform.position - transform.parent.position, joystickVec, lerpFract);
            cursorPos.Normalize();
            cursorPos = cursorPos * radius;
            transform.localPosition = cursorPos;
        } 
    }

    public float GetLerpFract()
    {
        return lerpFract;
    }

    public void Pause(bool pause)
    {
        paused = pause;
    }

    void LightOn()
    {
        glow.enabled = true;
        glow.range = Mathf.Lerp(glow.range, lightRange, lerpLight);
    }

    void LightOff()
    {
        glow.enabled = false;
        glow.range = Mathf.Lerp(glow.range, 0, lerpLight);
    }
}
