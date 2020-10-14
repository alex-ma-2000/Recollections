using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftLine : MonoBehaviour
{
    private Line line;
    public EdgeCollider2D edgeCol;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<Line>();
        edgeCol = gameObject.GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NeuronCircled(Vector2 pos, Vector2 dir)
    {
        Collider2D coll = Physics2D.OverlapCircle(pos, 15f, LayerMask.NameToLayer("Neurons"));
        if (coll.gameObject.CompareTag("neuron"))
        {
            if (!coll.gameObject.GetComponent<Neuron>().activated)
            {
                coll.gameObject.GetComponent<Neuron>().ActivateNeuron();
            }
        }
    }



}
