using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject Player;

    public int startSize; // Starting size of the camera
    public int maxSize; // Max size of the camera
    public float startVelocity; // Velocity at which the camera starts zooming
    public float neuronDistance; // Distance to a neuron to start camera zoom

    private float playerSpeed;
    private bool on = true;

    ProgressManager pm;

    private void Start()
    {
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = startSize;
        pm = ProgressManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (Transform neuron in GameObject.Find("Memory_" + (pm.getCurrentMemory().getID() + 1)).GetComponentInChildren<Transform>().GetComponentsInChildren<Transform>())
        {
            if (Vector3.Distance(Player.transform.position, neuron.position) < neuronDistance)
            {
                on = false;
                GetComponent<CinemachineVirtualCamera>().Follow = neuron;
                if (GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 15)
                {
                    GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += 1;
                }
            }
            else
            {
                if (GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > startSize)
                {
                    GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= 1;
                }
                else
                {
                    on = true;
                    GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
                }
            }
        } */
        if (on)
        {
            playerSpeed = Player.GetComponent<Rigidbody2D>().velocity.magnitude;
            float sizeInc = Mathf.Sqrt(playerSpeed - startVelocity);
            if (playerSpeed > startVelocity && GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < maxSize)
            {
                GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = sizeInc + startSize;
            }
            else if (playerSpeed <= startVelocity)
            {
                if (GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > startSize)
                {
                    GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= sizeInc;
                }
                else
                {
                    GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = startSize;
                }
            }
        }
    }

    public void turnOnOff(bool o)
    {
        on = o;
    }

    public void ZoomToNeuron(GameObject neuron)
    {
        on = true;
    }
}
