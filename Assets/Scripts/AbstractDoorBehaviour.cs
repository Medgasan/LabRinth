using UnityEngine;

public abstract class AbstractDoorBehaviour : MonoBehaviour
{
    internal bool IsOpen { get; private set; } = false;
    internal Color doorColor = Color.white;
    public void Open(bool open)
    {
        IsOpen = open;
        if (IsOpen)
        {
            OpenedAction();
            return;
        }

        ClosedAction();

    }

    public abstract void OpenedAction();

    public abstract void ClosedAction();

    public abstract void SetColor(Color color);

}

