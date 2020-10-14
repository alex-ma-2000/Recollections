using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public int id;
    [SerializeField]
    public GameObject[] neurons;
    public bool activated;
    private bool cutscenePlayed;
    ProgressManager pm;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


    // Start is called before the first frame update
    void Start()
    {
        pm = ProgressManager.instance;
        cutscenePlayed = false;

        minX = transform.position.x;
        maxX = transform.position.x + 0.2f;

        minY = transform.position.y;
        maxY = transform.position.y + 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 0.2f, maxX - minX) + minX, transform.position.y, transform.position.z);
        //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, maxY - minY) + minY, transform.position.z);
    }

    void ActivateMemory()
    {
        bool all_neurons = true;

        // Check if every neron has been activated
        for (int i = 0; i < neurons.Length; i++) {
            if (neurons[i].GetComponent<Neuron>().activated == false) {
                all_neurons = false;
            }
        }

        if (all_neurons == true) {
            activated = true;
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!cutscenePlayed) {
            ActivateMemory();
            pm.updateFlag(id);
            StartCoroutine(pm.playCutscene(id));
            cutscenePlayed = true;
        }
    }

    public int getID()
    {
        return id;
    }
}
