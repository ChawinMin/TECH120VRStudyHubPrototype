
using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        // esc
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
