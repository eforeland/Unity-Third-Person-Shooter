using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockThrow : MonoBehaviour
{
    public float speed = 1.0f;
    [SerializeField] private GameObject rockPrefab;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        // get the bullet roation
        Vector3 rockRotation = transform.rotation.eulerAngles;
        // set y rotation
        rockRotation.y = transform.eulerAngles.y;
        // assign rotation back to bullet   
        transform.rotation = Quaternion.Euler(rockRotation);

        Rigidbody rbody = rockPrefab.GetComponent<Rigidbody>();
        rbody.AddForce(transform.forward * speed, ForceMode.Impulse);

    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.Hit();
        }
        Destroy(this.gameObject);

    }
}
