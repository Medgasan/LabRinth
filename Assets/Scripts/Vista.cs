using UnityEngine;

public class Vista : MonoBehaviour
{
    [SerializeField] float radius = 5f;

    [SerializeField] float maxTime = 1f;

    float currentTime = 0;
    Transform player;



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
                        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, radius))
                        {
                            if (raycastHit.collider == collider)
                            {
                                player = collider.transform;
                                //Debug.Log("Dr.Who Detected!!");
                            }
                        }

                    }
                        
                }
            }
        }
    }

    public Transform GetPlayerInVista() { return player; }
}
