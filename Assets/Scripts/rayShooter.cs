using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayShooter : MonoBehaviour
{
    int open = 0;

    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject bulletSpawn;
    private float speed = 30.0f;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.UI_POPUP_OPENED, OnPopupOpen);
        Messenger<int>.AddListener(GameEvent.UI_POPUP_CLOSED, OnPopupClosed);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.UI_POPUP_OPENED, OnPopupOpen);
        Messenger<int>.RemoveListener(GameEvent.UI_POPUP_CLOSED, OnPopupClosed);
    }

    void OnPopupOpen(int numOpen)
    {
        open = numOpen;
    }

    void OnPopupClosed(int numOpen)
    {
        open = numOpen;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (open == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // --- rigidbody canon shooter code------
                
                // spawn bullet prefab at bulletSpawn(in front of player)
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = bulletSpawn.transform.position;

                // get the bullet roation
                Vector3 bulletRotation = bullet.transform.rotation.eulerAngles;
                // set y rotation
                bulletRotation.y = transform.eulerAngles.y;
                // assign rotation back to bullet   
                bullet.transform.rotation = Quaternion.Euler(bulletRotation);

                Rigidbody rbody = bullet.GetComponent<Rigidbody>();
                rbody.AddForce(bulletSpawn.transform.forward * speed, ForceMode.Impulse);

                StartCoroutine(DestroyProjectile(bullet));
            }
        }
    }

    private IEnumerator DestroyProjectile(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }

}
