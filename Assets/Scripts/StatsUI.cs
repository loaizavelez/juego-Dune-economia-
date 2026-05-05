using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private Slider spiceSlider;

    // Update is called once per frame
    void Update()
    {
        spiceSlider.value = stats.Spice;
    }
}
