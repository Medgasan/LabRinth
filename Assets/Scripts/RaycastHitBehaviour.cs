using UnityEngine;

public class RaycastHitBehaviour : MonoBehaviour
{
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private string rayLayerTag;
    [SerializeField] private Player player;

    private void FixedUpdate()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, rayDistance))
        {
            if (raycastHit.collider != null && raycastHit.collider.CompareTag(rayLayerTag))
            {
                player.InteractableObject(raycastHit.collider);
                //Debug.Log("Colisionando con: " + raycastHit.collider.tag);
            }

            if (raycastHit.collider == null || !raycastHit.collider.CompareTag(rayLayerTag))
            {
                player.InteractableObject(null);
            }

            return;
        }
        
        player.InteractableObject(null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
    
}
