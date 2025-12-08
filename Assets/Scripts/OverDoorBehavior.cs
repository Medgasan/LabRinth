using UnityEngine;

public class OverDoorBehavior : MonoBehaviour
{

    [SerializeField] AudioSource audioSource; 

    void Start()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }
}
