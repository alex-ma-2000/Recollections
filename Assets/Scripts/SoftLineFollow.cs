using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoftLineFollow : MonoBehaviour
{
    public GameObject linePrefab;
    private EdgeCollider2D edgeColl;
    private Line softLine;
    private List<Vector2> points;
    private bool circleDrawn;
    // public LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        GameObject line = Instantiate(linePrefab);
        softLine = line.GetComponent<Line>();
        softLine.UpdateLine(transform.position);
        points = softLine.GetPoints();
        circleDrawn = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (softLine != null)
        //{
        //    softLine.UpdateLine(transform.position);
        //    List<Vector2> points = softLine.GetPoints();
        //    if(points.Count > 0) { 
        //    for (int i = 0; i < points.Count - 10; i++)
        //        {
        //            if (Vector2.Distance(points[points.Count - 1], points[i]) < 0.5f && !circleDrawn)
        //            {
        //                circleDrawn = true;
        //                softLine.GetComponent<SoftLine>().NeuronCircled(transform.position, transform.up);
        //                Debug.Log("Circle Drawn!");
        //                circleDrawn = false;
        //            }
        //        }
        //    }
        //}
    }


}
