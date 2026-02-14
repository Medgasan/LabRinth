using UnityEngine;

public class HurtBehaviour : MonoBehaviour
{
    [SerializeField] int damage = 6;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            ScoreManager.Instance.AddTrap();
            PlayerController player = collider.GetComponent<PlayerController>();
            player.TakeDamage(damage);

        }
    }
}
