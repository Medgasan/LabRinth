using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ButtonSwitchBehaviour : AbstractSwitcherBehaviour
{

    [SerializeField] private Light pointLight;
    [SerializeField] private GameObject cubeMaterial;

    bool lightUpdated = false;


    void Start()
    {
        pointLight.gameObject.SetActive(false);
        pointLight.color = switchColor;
        cubeMaterial.GetComponent<Renderer>().material.color = switchColor;
    }


    public override void SwitchAction()
    {

        if (linkedDoor.IsOpen)
        {
            pointLight.enabled = false;
            return;
        }

        pointLight.enabled = true;
    }


    void Update()
    {
        if (!lightUpdated) lightUpdate();

    }


    void lightUpdate()
    {
        if (linkedDoor == null) return;
        if (linkedDoor.doorColor != switchColor)
        {
            linkedDoor.SetColor(switchColor);
            lightUpdated = true;
        }
    }


    public override void CanActivate()
    {
        pointLight.gameObject.SetActive(true);
    }


    public override void CannotActivate()
    {
        pointLight.gameObject.SetActive(false);
    }
}
