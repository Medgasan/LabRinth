using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] float radius = 5f;

    [SerializeField] float maxTime = 0.1f;

    float currentTime = 0;
    PlayerController player;



    void Update()
    {
        if ((Time.time - currentTime) > (1f / maxTime))
        {
            player = null;
            currentTime = Time.time;
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        Vector3 direction = collider.transform.position - transform.position;
                        Vector3 desplazamiento = new Vector3(0, 2, 0);
                        if (Physics.Raycast(transform.position + desplazamiento, direction, out RaycastHit raycastHit, radius))
                        {
                            if (raycastHit.collider.Equals(collider))
                            {
                                player = collider.GetComponent<PlayerController>();
                            }
                        }

                    }
                        
                }
            }
        }
    }

    public PlayerController GetPlayerInVista() { return player; }
}
