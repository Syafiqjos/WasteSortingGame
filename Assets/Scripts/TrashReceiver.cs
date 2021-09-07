using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashReceiver : MonoBehaviour
{
    public UnityEvent OnTrashDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            Destroy(collision.gameObject);
            OnTrashDestroy.Invoke();
        }
    }
}
