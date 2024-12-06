using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour
{
    public void PositionLane()
    {
        int randomLane = Random.Range(-2, 2);
        transform.position = new Vector3(randomLane, transform.position.y, transform.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
