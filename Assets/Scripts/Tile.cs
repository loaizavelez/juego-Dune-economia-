using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Image image;
    private bool isCaptured = false;

    public void Capture()
    {
        if (isCaptured) return;

        isCaptured = true;
        image.color = Color.green;
    }
}