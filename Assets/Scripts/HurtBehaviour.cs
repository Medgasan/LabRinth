using UnityEngine;

public class HurtBehaviour : MonoBehaviour
{


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Player"))
        {
            collider.gameObject.SendMessage("TakenDamage", GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
        }
    }
}
