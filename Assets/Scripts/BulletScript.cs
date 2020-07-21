using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // is this object our enemy?
        if (other.gameObject.tag == "Zombie")
        {
            ReactiveTarget target = other.gameObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
            }
        }
    }
}
