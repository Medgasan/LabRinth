using UnityEngine;

public class LaserDoorBehaviour : AbstractDoorBehaviour
{

    [SerializeField] Color laserColor = Color.blueViolet;
    //[SerializeField] laserScript[] lasers;
    [SerializeField] BoxCollider forceField;
    [SerializeField] AudioSource ummFx;
    [SerializeField] AudioSource onoffFx;


    private laserScript[] lasers;

    public override void ClosedAction()
    {
        Debug.Log("Laser Door ClosedAction called");
        foreach (var laser in lasers)
        {
            laserScript laserGameObject = laser.GetComponent<laserScript>();
            laserGameObject.gameObject.SetActive(true);
        }
        forceField.gameObject.SetActive(true);
        ummFx.gameObject.SetActive(true);
        onoffFx.Play();

    }

    public override void OpenedAction()
    {
        Debug.Log("Laser Door OpenedAction called");
        foreach (var laser in lasers)
        {
            laserScript laserGameObject = laser.GetComponent<laserScript>();
            laserGameObject.gameObject.SetActive(false);
        }
        forceField.gameObject.SetActive(false);
        ummFx.gameObject.SetActive(false);
        onoffFx.Play();
    }

    void Start()
    {
        lasers = GetComponentsInChildren<laserScript>();
        this.SetColor(laserColor);
    }

    public override void SetColor(Color color)
    {
        laserColor = color;
        foreach (var laser in lasers)
        {
            laser.setColor(laserColor);
        }
    }

}
