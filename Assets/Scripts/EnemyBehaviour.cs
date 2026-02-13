using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour
{

    [Header("Patrol")]
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] float umbralDeLlegada = 1f;
    [Header("Olivion")]
    [SerializeField] float olivionTime = 10f;
    [SerializeField] float maxDistanceToComprobate = 5f;
    [Header("Damage")]
    [SerializeField] Transform gunTarget;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] float laserDamage= 10f;
    [SerializeField] float laserFrecuency = 1f;
    [SerializeField] Vector3 laserStartOffset = new Vector3(0.68f, 0, 0);


    private Transform target = null;
    private Transform LastTarget = null;
    private NavMeshAgent agent;
    private int currentPatrolPointIndex = 0;
    private Vista vista;
    private float currentOlivion = 0f;
    private bool isShoting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vista = GetComponent<Vista>();
        //laserLine = GetComponentInChildren<LineRenderer>();
    }


    void Update()
    {

        target = vista.GetPlayerInVista();

        if (target != null)
        {
            SeekAndDestroyEnemy();
        }
        else
        {
            if (LastTarget != null)
                Olivion();
            else
                Patrol();
        }

    }



    public void Olivion()
    {
        currentOlivion += Time.deltaTime;
        if ((currentOlivion > olivionTime) || Vector3.Distance(transform.position,LastTarget.position)< maxDistanceToComprobate)
        {
            currentOlivion = 0f;
            LastTarget = null;
        }
    }


    private void SeekAndDestroyEnemy()
    {
        LastTarget = target;
        agent.speed = 7f;
        agent.SetDestination(LastTarget.position);
        if (Vector3.Distance(transform.position, LastTarget.position) < maxDistanceToComprobate)
        {
            agent.SetDestination(transform.position);
            StartCoroutine("ShotLaser");
        }
    }


    private void Patrol()
    {
        agent.speed = 3.5f;
        Transform patrolPoint = patrolPointsParent.GetChild(currentPatrolPointIndex);
        agent.SetDestination(patrolPoint.position);
        if (Vector3.Distance(patrolPoint.position, transform.position) < umbralDeLlegada)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex >= patrolPointsParent.childCount)
            {
                currentPatrolPointIndex = 0;
            }
        }
    }


    IEnumerator ShotLaser()
    {
        if (isShoting) yield break;
        Player player = target.GetComponent<Player>();
        Vector3 distance = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, distance, out RaycastHit hitInfo))
        {
            laserLine.SetPosition(0, gunTarget.position);
            laserLine.SetPosition(1, hitInfo.point);
        }
        isShoting = true;
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserFrecuency);

        //player.Hit(laserDamage);

        Debug.Log("Exterminate!!!!!");
        laserLine.enabled = false;
        yield return new WaitForSeconds(laserFrecuency);
        isShoting = false;
        yield break;
    }
}
