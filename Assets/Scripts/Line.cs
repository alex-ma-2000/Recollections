using System.Linq;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Mathf;

public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    private EdgeCollider2D edgeCol;
    public float maxFadeTime = 0.05f;
    private float lineFadeTimer;
    public int maxPoints = 50;

    List<Vector2> points;

    void Start()
    {
        lineFadeTimer = maxFadeTime;
        edgeCol = gameObject.GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        lineFadeTimer -= Time.deltaTime;
        if ((lineFadeTimer < 0.0f && points.Count > 0) || points.Count > maxPoints)
        {
            points.RemoveAt(0);
            lineFadeTimer = maxFadeTime;
            //lineRenderer.SetPositions(points);
            lineRenderer.positionCount--;
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i]);
            }
            edgeCol.points = points.ToArray();

        }
    }

    public void UpdateLine(Vector2 pos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(pos);
            return;
        }

        if (points.Count > 0)
        {
            if (Vector2.Distance(points.Last(), pos) > .1f)
                SetPoint(pos);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
            edgeCol.points = points.ToArray();

    }

    public List<Vector2> GetPoints()
    {
        return points;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 direction = coll.gameObject.GetComponent<Rigidbody2D>().velocity;
        GetComponent<AreaEffector2D>().forceAngle = Vector2.Angle(direction, new Vector2(1,0));
    }



}