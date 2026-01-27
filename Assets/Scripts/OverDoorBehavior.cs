using UnityEngine;

public class OverDoorBehavior : MonoBehaviour
{

    [SerializeField] AudioSource audioSource; 

    void Start()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
    }
}
