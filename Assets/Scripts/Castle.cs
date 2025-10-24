using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(99999);

            if (gameManager == null)
            {
                gameManager = FindAnyObjectByType<GameManager>();
            }

            if( gameManager != null )
            {
                gameManager.UpdateHP(-1);
            }
        }
    }
}
