using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] GameObject ramp;
    [SerializeField] GameObject gate;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (ramp.activeInHierarchy)
            {
                ramp.SetActive(false);
            }
            else 
            {
                gate.SetActive(false);
            }
            Destroy(other.gameObject);
        }
    }
}
