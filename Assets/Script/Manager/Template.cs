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

public class FishAllDetail
{
    public bool fevarit;
    public tagType tag;
    public string fishName;
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
    public int _fishUnlockPoint;
    public GameObject _placeFishPrefab;
}

#endregion