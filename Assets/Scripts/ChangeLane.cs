using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour
{
    public void PositionLane()
    {
        int randomLane;
        do
        {
            randomLane = Random.Range(-2, 3); // Gera valores de -2, -1, 0, 1, 2
        } while (randomLane == -1 || randomLane == 1); // Re-sorteia se for -1 ou 1

        transform.position = new Vector3(randomLane, transform.position.y, transform.position.z);
    }
}
