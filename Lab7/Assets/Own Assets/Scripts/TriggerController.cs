using UnityEngine;
using UnityEngine.UI;

public class TriggerController : MonoBehaviour
{
    public GameObject door;

    Animator animator;
    bool doorOpen;

    // Use this for initialisation
    void Start()
    {
        doorOpen = false;
        animator = door.GetComponent<Animator>();
    }

    // When the player enters the hidden capsule, play an opening animation for the corresponding door. The variable doorOpen will help us examine later if the door was opened.
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            doorOpen = true;
            animator.SetTrigger("Open");
            other.GetComponent<ScoreController>().AddPoints(1);
        }
    }

    // When the player leaves the hidden zone, close the door. We base on the boolean doorOpen and change its state, as well.
    void OnTriggerExit(Collider other)
    {
        if (doorOpen)
        {
            doorOpen = false;
            animator.SetTrigger("Close");
        }
    }
}
