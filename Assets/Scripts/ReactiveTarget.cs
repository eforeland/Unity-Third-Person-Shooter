using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {
    private bool isAlive = true;

    public void ReactToHit()
    {
        if (isAlive)
        {
            WanderingAI enemyAI = GetComponent<WanderingAI>();
            if (enemyAI != null)
            {
                enemyAI.ChangeState(EnemyStates.dead);
            }

            Animator enemyAnimator = GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Die");
            }

            isAlive = false;
            StartCoroutine(Die());
        }
    }
     
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_DEAD, this.gameObject);
        Destroy(this.gameObject);
    }

   
}
