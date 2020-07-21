using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertMove : MonoBehaviour
{
    private bool pause = false;
    //private CharacterController cc;

    Vector3[] waypoints = new Vector3[2];

    private int waypointIndex = 0;
    private float speed = 5.0f;
    //private bool forward = true;
    // Start is called before the first frame update
    private void Start()
    {
        waypoints[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        waypoints[1] = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (!pause)
        {
            MovePlatform(Time.fixedDeltaTime);
            if (transform.position == waypoints[waypointIndex])
            {
                StartCoroutine(Pause());
                if (waypointIndex == 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex = 1;
                }
            }
        }
    }

    IEnumerator Pause()
    {
        pause = true;
        yield return new WaitForSeconds(2.0f);
        pause = false;
    }

    void MovePlatform(float deltaTime)
    {
        float step = speed * deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], step);
        transform.position = newPosition;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("child");
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("unchild");
            other.transform.parent = null;
        }
    }
}
