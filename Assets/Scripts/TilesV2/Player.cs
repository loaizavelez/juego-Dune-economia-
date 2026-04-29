using UnityEngine;

public class Player : MonoBehaviour
{
    public Controller player;
    public int water;
    public void GetWater(int gain)
    {
        water += gain;
    }

    public void PayWater(int loss)
    {
        water -= loss;
    }
}
