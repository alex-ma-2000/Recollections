using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private ControllerInputScript controls;
    public float speed;
    private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
        controls = GameObject.Find("ControllerInput").GetComponent<ControllerInputScript>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float joystickX = controls.getRightStickX();
        float joystickY = controls.getRightStickY();
        Vector3 offset = new Vector3(joystickX,joystickY,0);
        //transform.position = transform.position + offset * speed;
        rb2d.MovePosition(transform.position + offset * speed);

        // rb.AddForce(offset * speed);
    }
}
