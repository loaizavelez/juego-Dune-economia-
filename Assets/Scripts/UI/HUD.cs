using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject resourcesPanel; // arrastra tu ResourcesPanel aquí en el inspector
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab; // tecla para mostrar/ocultar

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            // alternar visibilidad
            resourcesPanel.SetActive(!resourcesPanel.activeSelf);
        }
    }
}
