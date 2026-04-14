using UnityEngine;
[CreateAssetMenu(fileName = "NewDecree", menuName = "Politics/Decree")]
public class DecreeData : ScriptableObject
{

    [SerializeField] private string decreeName;
    [SerializeField] private string description;
    [SerializeField] private int productionChange;
    [SerializeField] private int communitySupportChange;
    [SerializeField] private string favoredFaction;
    [SerializeField] private int stabilityChange;


    public bool IsApplied { get; set; }


    public string DecreeName => decreeName;
    public string Description => description;
    public int ProductionChange => productionChange;
    public int CommunitySupportChange => communitySupportChange;
    public string FavoredFaction => favoredFaction;

    public int StabilityChange => stabilityChange;
}
