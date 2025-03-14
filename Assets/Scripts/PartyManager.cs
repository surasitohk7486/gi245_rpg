using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<Characters> members = new List<Characters>();
    public List<Characters> Members { get { return members; } } 

    [SerializeField]
    private List<Characters> selectChars = new List<Characters>();
    public List<Characters> SelectChars { get { return selectChars; } }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }
}
