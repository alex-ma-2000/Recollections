using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPath : MonoBehaviour
{
    public int lvlID;

    ProgressManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = ProgressManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (lvlID == pm.getCurrentMemory().getID())
        {
            Destroy(gameObject);
        }
    }
}
