using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Contract : MonoBehaviour
{


    public ContractStats contractStats;

    ContractPublicInfo contractPublicInfo;

    public List<Plant> plants = new List<Plant>();

    public GameObject plantprefab;


    private void Awake()
    {

        var parent = GetComponent<ContractPublicInfo>();// transform.GetChild(2).GetComponent<ContractPublicInfo>();
        contractPublicInfo = parent;
        if (contractPublicInfo != null)
        {
            //  contractPublicInfo.SetContractPublicInfo();
            // contract not started   contractPublicInfo.ContractUIButton.enabled = false;
        }

        InstantiatePlants();
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
    /// spawning all fifteens plants for each contract
    /// </summary>
    void InstantiatePlants()
    {
        for (int i = 0; i < InitialPlantsCount; i++)
        {
            GameObject plant = Instantiate(plantprefab, gameObject.transform);
            plant.transform.parent = gameObject.transform;
            plants.Add(plant.GetComponent<Plant>());
        }

    }

    public void Log()
    {
        for (int i = 0; i < plants.Count; i++)
        {
            Debug.Log(plants[i].plantStats.isPlantInContract);
        }
    }

    /// <summary>
    /// Creating initial contract
    /// </summary>
    public void CreateContract()
    {

        contractStats = new ContractStats();
        if (contractStats.isContractStarted == false)
        {
            contractStats.ContractDescription = "test";
            contractStats.ContractID = contractPublicInfo.StaticConttractID;
            contractStats.isContractStarted = true;
            // set public info here
            //   gameObject.GetComponent<ContractPublicInfo>().SetPlayerPrefsContractID(contract.contractStats.ContractID, true);
            string serializedJson = JsonUtility.ToJson(contractStats);
            FirebaseReferenceManager.reference.Child("USERS").Child(LogInAndRegister.Instance.UserName).Child("farmdata").Child("contract" + contractStats.ContractID).SetRawJsonValueAsync(serializedJson);
            CreatePlantsForContract();
        }

        LoadContractDataA();
    }

    /// <summary>
    /// Planting plants for the crreated contract
    /// </summary>
    void CreatePlantsForContract()
    {
        for (int i = 0; i < plants.Count; i++)
        {

            plants[i].SetInitialPlants(contractStats.ContractID, i);
        }
    }


    /// <summary>
    /// Retreiving all plants data for each contract
    /// </summary>
    public void RetreivePlantsData()
    {
        if (contractStats == null)
            return;

        for (int i = 0; i < plants.Count; i++)
        {
            plants[i].GetPlantStatsData(contractStats.ContractID, i);
            plants[i].GetPlantsGrwothFactorsData(contractStats.ContractID, i);
        }

    }
    /// <summary>
    /// Deleting the contract with pop up confirmation yes/no
    /// </summary>
    public void DeleteContract()
    {
        if (contractStats != null)
        {
            FirebaseReferenceManager.reference.Child("USERS").Child(LogInAndRegister.Instance.UserName).Child("farmdata").Child("contract" + contractStats.ContractID).RemoveValueAsync();
            UIController.Instance.DeleteContractDialog(false);
            contractStats = null;
            UIController.Instance.DeleteDialogYesButton.onClick.RemoveAllListeners();
        }
        DeletePlants();
    }

    void DeletePlants()
    {
        for (int i = 0; i < plants.Count; i++)
        {
            plants[i].ClearPlantStats();
        }
    }

    /// <summary>
    /// Passing the deletecontract method to the Yes button in the confirmation dialog
    /// </summary>
    /// <param name="Yes"></param>
    public void OpenDeleteContractDialog()
    {
        UIController.Instance.DeleteDialogYesButton.onClick.AddListener(() => DeleteContract());
        UIController.Instance.DeleteContractDialog(true);
    }


    /// <summary>
    /// Loads data  from db for each contract after the log in 
    /// </summary>
    public void LoadContractDataA()
    {

        FirebaseDatabase.DefaultInstance
           .GetReference("USERS").Child(LogInAndRegister.Instance.UserName).Child("farmdata").Child("contract" + contractPublicInfo.StaticConttractID)
           .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;

              Debug.Log(snapshot.GetRawJsonValue());

              contractStats = JsonUtility.FromJson<ContractStats>(snapshot.GetRawJsonValue());
              isDataLoaded = true;
          }
      });

    }

    public bool isContractDataLoaded()
    {
        return isDataLoaded;
    }

    bool isDataLoaded;
    int InitialPlantsCount = 15;

    //CLASSES
    [Serializable]
    public class ContractStats
    {
        public int ContractID;
        public bool isContractStarted;
        public string ContractDescription;
    }

}
