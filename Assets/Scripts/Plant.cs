using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField]
    PlantStats plantStats;

    int initialPlants = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetInitialDataForPlant(int contractID)
    {
        for (int i = 0; i < initialPlants; i++)
        {
            plantStats = new PlantStats();

            plantStats.isDroneAssigned = false;
            plantStats.isPlantPlanted = true;
            plantStats.isPlantInContract = true;
            plantStats.FieldID = generateID();
            plantStats.ContractID = contractID;
            plantStats.PlantID = i;
            string serializedJson = JsonUtility.ToJson(plantStats);
            FirebaseReferenceManager.reference.Child("USERS").Child(LogInAndRegister.Instance.UserName).Child("FARMDATA").Child("CONTRACT" + contractID).Child("Plants").Child("Plant"+i).SetRawJsonValueAsync(serializedJson);
        }
     
    }

    public string generateID()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// </summary>
    public void LoadPlantData(int StaticConttractID, int plantID)
    {

        FirebaseDatabase.DefaultInstance
           .GetReference("USERS").Child(LogInAndRegister.Instance.UserName).Child("FARMDATA").Child("CONTRACT" + StaticConttractID).Child("Plants").Child("Plant"+ plantID)
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

                   plantStats = JsonUtility.FromJson<PlantStats>(snapshot.GetRawJsonValue());
               }
           });

    }

    public void ClearPlantStats()
    {
        plantStats = null;
    }



    [Serializable]
    public class PlantStats
    {
        public bool isDroneAssigned;
        public bool isPlantPlanted;
        public bool isPlantInContract;
        public int PlantID;
        public string  FieldID;
        public int ContractID;
        public int GrowthDays;
        public int Tultip;
        public int SoilMoisture;
        public int SoilDensity;
        public int SoilOrganicMaterial;
        public int Fertilizer;
        public int Weed;
        public int Disease;
        public int Toxicity;
        public int Acidity;




    }
}
