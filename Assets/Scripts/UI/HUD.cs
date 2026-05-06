using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject hudPanel; // arrastra tu Canvas aquí en el inspector
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab; // tecla para mostrar/ocultar

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            bool visible = hudPanel.activeSelf;
            hudPanel.SetActive(!visible);
        }
    }
}
