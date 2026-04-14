using System.Collections.Generic;
using UnityEngine;
using Mihir;

public class DataContainerManager : Singleton<DataContainerManager>
{
    public List<FishSpawnData> _fishData;

    void Start()
    {
        //FishSaveManager.Load(_fishData);
    }

    /// <summary>Returns the fish entry matching the given ID, or null if not found.</summary>
    //public FishSpawnData GetFishByID(int id)
    //{
    //    return _fishData.Find(f => f.fishID == id);
    //}

    /// <summary>Persists all fish unlock and favourite states.</summary>
    //    public void SaveAllData()
    //    {
    //        //FishSaveManager.Save(_fishData);
    //        Debug.Log("All Fish Data Saved");
    //    }

}
