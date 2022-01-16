using UnityEngine;

public class Credits : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Quit()
    {
        Application.Quit();
    }

}
