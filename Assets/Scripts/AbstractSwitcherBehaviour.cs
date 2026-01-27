using UnityEngine;

public abstract class AbstractSwitcherBehaviour : MonoBehaviour
{ 
    [SerializeField] internal AbstractDoorBehaviour linkedDoor;
    [SerializeField] internal Color switchColor;


    public void Action()
    {
        Debug.Log("Switcher actionated!");
        if (linkedDoor == null) {
            Debug.LogWarning("No door linked to the switcher!");
            return;
        }

        linkedDoor.Open(!linkedDoor.IsOpen);

        SwitchAction();

    }


    public abstract void SwitchAction();

    public abstract void CanActivate();

    public abstract void CannotActivate();

}
