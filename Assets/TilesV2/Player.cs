using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Controller player;
    public int water;
    public int spice;
    public int metals;

    [SerializeField] TMP_Text w;//Water
    [SerializeField] TMP_Text s;//Spice
    [SerializeField] TMP_Text m;//Metals


    private void Start()
    {
        w.text = $"Water: {water}";
        s.text = $"Spice: {spice}";
        m.text = $"Metals: {metals}";
    }

    public void GetWater(int gain)
    {
        water += gain;
        w.text = $"Water: {water}";
    }

    public void GetSpice(int gain)
    {
        spice += gain;
        s.text = $"Spice: {spice}";
    }

    public void GetMetals(int gain)
    {
        metals += gain;
        m.text = $"Metals: {metals}";
    }

    public void PayWater(int loss)
    {
        water -= loss;
        w.text = $"Water: {water}";
    }
}
