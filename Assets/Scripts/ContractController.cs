using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ContractController : MonoBehaviour
{


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Getting the db data for each started contract
    /// </summary>
    public void GetDataForAllContracts()
    {
        foreach (var item in Contracts)
        {
           item.LoadContractDataA();
        }
    }

    /// <summary>
    /// Retreiving all contracts plants data only
    /// </summary>
    public void GetDataForAllPlantsLinkedWithContracts()
    {
        for (int i = 0; i < Contracts.Count; i++)
        {
            Contracts[i].RetreivePlantsData();

        }

    }


    //PUBLIC VARIABLES 
    public static ContractController Instance;
    public List<Contract> Contracts = new List<Contract>();

}
