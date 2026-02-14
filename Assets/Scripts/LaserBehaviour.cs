using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] float timeToDamage = 1.0f;
    [SerializeField] float timeToStart = 0.5f;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] int damage = 6;

    private float damageTimer = 0.0f;
    private bool canStart = false;
    
    void Start()
    {

        // lanza un raycast desde la posición del objeto hacia adelante
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            // si el raycast golpea algo, establece el punto final de la línea en el punto de impacto
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, hitInfo.point);
        }
        else
        {
            // si no golpea nada, establece un punto final predeterminado (por ejemplo, 100 unidades adelante)
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, transform.position + transform.forward * 100f);
        }

    }


    private void Update()
    {
        UpdateTimers();

    }


    private void UpdateTimers()
    {
        damageTimer += Time.deltaTime;
        if (!canStart && damageTimer >= timeToStart)
        {
            canStart = true;
            damageTimer = 0.0f;
            return;
        }

        if (damageTimer >= timeToDamage)
        {
            laserLine.enabled = !laserLine.enabled;
            damageTimer = 0.0f;
        }
    }




    private void FixedUpdate()
    {

        if (!laserLine.isVisible) return;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.collider != null)
            {
                Collider collider = hitInfo.collider;
                if (collider.CompareTag("Player"))
                {
                    ScoreManager.Instance.AddTrap();
                    PlayerController player = collider.GetComponent<PlayerController>();
                    player.TakeDamage(damage);
                    //collider.gameObject.SendMessage("TakenDamage", this.gameObject.GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
