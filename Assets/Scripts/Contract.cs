using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Contract : MonoBehaviour
{


    public ContractStats contractStats;


    ContractPublicInfo contractPublicInfo;

    private void Awake()
    {

        var parent = GetComponent<ContractPublicInfo>();// transform.GetChild(2).GetComponent<ContractPublicInfo>();
        contractPublicInfo = parent;
        if (contractPublicInfo != null)
        {
            //  contractPublicInfo.SetContractPublicInfo();
            // contract not started   contractPublicInfo.ContractUIButton.enabled = false;


        }
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
    /// Creating contract
    /// </summary>
    public void CreateContract()
    {

        Contract contract = new Contract();
        contract.contractStats = new ContractStats();
        if (contract.contractStats.isContractStarted == false)
        {
            contract.contractStats.ContractDescription = "test";
            contract.contractStats.ContractID = contractPublicInfo.StaticConttractID;
            contract.contractStats.isContractStarted = true;
            // set public info here
            //   gameObject.GetComponent<ContractPublicInfo>().SetPlayerPrefsContractID(contract.contractStats.ContractID, true);
            string serializedJson = JsonUtility.ToJson(contract);
            FirebaseReferenceManager.reference.Child("USERS").Child(LogInAndRegister.Instance.UserName).Child("FARMDATA").Child("CONTRACT" + contract.contractStats.ContractID).SetRawJsonValueAsync(serializedJson);
            LoadContractDataA();
        }
    }



    /// <summary>
    /// Deleting the contract with pop up confirmation yes/no
    /// </summary>
    public void DeleteContract()
    {
        if(contractStats != null)
        {
            FirebaseReferenceManager.reference.Child("USERS").Child(LogInAndRegister.Instance.UserName).Child("FARMDATA").Child("CONTRACT" + contractStats.ContractID).RemoveValueAsync();
            UIController.Instance.DeleteContractDialog(false);
            contractStats = null;
            UIController.Instance.DeleteDialogYesButton.onClick.RemoveAllListeners();
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
           .GetReference("USERS").Child(LogInAndRegister.Instance.UserName).Child("FARMDATA").Child("CONTRACT" + contractPublicInfo.StaticConttractID).Child("contractStats")
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
          }
      });

    }


    //CLASSES
    [Serializable]
    public class ContractStats
    {
        public int ContractID;
        public bool isContractStarted;
        public string ContractDescription;
    }

}
