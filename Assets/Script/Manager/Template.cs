using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

#region Shop Owner

[Serializable]
public class Distributor
{
    public string name;
    public string store_id;
}

[Serializable]
public class Result
{
    public string store_id;
    public string name;
    public string email;
    public string phone_no;
    public string country;
    public string city;
    public string store_name;
    public string logo;
    public string description;
    public Distributor distributor;
} 

[Serializable]
public class ShopOwnerResponce
{
    public string message;
    public bool status;
    public Result result;
}

[Serializable]

#endregion

#region FishAndUser

public class FishAllDetail
{
    public bool fevarit;
    public tagType tag;
    public string fishName;
    public string fishDetail;
    public string funFact;
    public string availability;
    public string fishType;
    public string fishoptimalTemperature;
    public string canfishSurviveIn;
    public string fishMaxSize;
    public CategoryType category;
    public Sprite fishImage;
}

public enum tagType
{
    Saltwater, Freshwater
}
public enum CategoryType
{
    GoldFish, KoiFish, FGoldFish, NormalFish
}

[Serializable]
public class FishSpawnData
{
    public FishAllDetail fishData;
    public int fishID;
    /// <summary>Cost in points required to unlock this fish. Set in Inspector — never overwritten at runtime.</summary>
    public int fishUnlockCost;
    public GameObject _placeFishPrefab;
}

[Serializable]
public class FishFullSaveData
{
    public List<int> unlockedFishIDs = new List<int>();
    public List<int> favoriteFishIDs = new List<int>();
}

[Serializable]
public class UserData
{
    public string userName;

    public int level;
    public float progressPercent;

    public int totalFishCollected;

    public int points;
    public string title;

    public FishFullSaveData fishFullSaveData = new FishFullSaveData();
}
#endregion