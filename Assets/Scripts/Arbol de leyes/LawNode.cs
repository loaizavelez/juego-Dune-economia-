using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [System.Serializable]
    public class LawNode
    {
     public string lawName;
     public Button lawButton;

     
     public List<LawNode> unlockedLaws;

    public bool isApplied = false;
    }

