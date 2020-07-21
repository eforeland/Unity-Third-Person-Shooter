using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private int value = 1;

    void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.FirstAid(value);
            Destroy(this.gameObject);
            Messenger.Broadcast(GameEvent.JEWEL_COLLECTED);
        }
    }
}
