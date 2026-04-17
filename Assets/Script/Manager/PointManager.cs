using UnityEngine;
using UnityEngine.UI;
using System;
using Mihir;
using System.Collections.Generic;

public class PointManager : Singleton<PointManager>
{
    public int _point;

    public ShopOwnerResponce _shopOwnerResponce;

    public List<int> _unLockFishID = new List<int>();
    public List<int> _categoryComplete = new List<int>();
    public List<int> _likeFishID = new List<int>();
    public static int scene,_timeopenApp;
    

    void Start()
    {
        // Always load — UserSaveManager.Load creates default data on first run
        UserData user = UserSaveManager.Load();
        _point = user.points;
        _unLockFishID = new List<int>(user.fishFullSaveData.unlockedFishIDs);
        _likeFishID = new List<int>(user.fishFullSaveData.favoriteFishIDs);
        _categoryComplete = LoadIntList("CategoryData");
        if(PlayerPrefs.HasKey("OpenAppTime"))
        {
              _timeopenApp = PlayerPrefs.GetInt("OpenAppTime") + 1;
               PlayerPrefs.SetInt("OpenAppTime", _timeopenApp);
        }
        else
        {
             PlayerPrefs.SetInt("OpenAppTime", 1);
            Debug.Log("First time opening app, setting OpenAppTime to 1" + PlayerPrefs.GetInt("OpenAppTime"));
        }

            Invoke("VideoImageM", 1f);
    }
 

    /// <summary>Adds the given amount to the player's points and persists.</summary>
    public void AddPoint(int point)
    {
        _point += point;

        UserData user = UserSaveManager.Load();
        user.points = _point;
        UserSaveManager.Save(user);

       // DataContainerManager.Instance.SaveAllData();
        Debug.Log("Points after add: " + _point);
    }

    /// <summary>Subtracts the given amount from the player's points and persists.</summary>
    public void Removepoint(int point)
    {
        if (_point >= point)
        {
            _point -= point;

            UserData user = UserSaveManager.Load();
            user.points = _point;
            UserSaveManager.Save(user);

            Debug.Log("Points after remove: " + _point);
        }
        else
        {
            Debug.Log("Points not sufficient");
        }
    }

    /// <summary>Records a fish as unlocked and persists.</summary>
    public void UnLockFishSave(int id)
    {
        if (!_unLockFishID.Contains(id))
            _unLockFishID.Add(id);

        UserData user = UserSaveManager.Load();
        if (!user.fishFullSaveData.unlockedFishIDs.Contains(id))
        {
            user.fishFullSaveData.unlockedFishIDs.Add(id);
            UserSaveManager.Save(user);
        }

        //DataContainerManager.Instance.SaveAllData();
    }

    /// <summary>Records a completed category level and persists.</summary>
    public void SetCategoryData(int level)
    {
        if (!_categoryComplete.Contains(level))
            _categoryComplete.Add(level);

        SaveIntList("CategoryData", _categoryComplete);
    }

    /// <summary>Adds a fish to the favourites list and persists.</summary>
    public void AddLikeData(int fishId)
    {
        if (!_likeFishID.Contains(fishId))
            _likeFishID.Add(fishId);

        UserData user = UserSaveManager.Load();
        if (!user.fishFullSaveData.favoriteFishIDs.Contains(fishId))
        {
            user.fishFullSaveData.favoriteFishIDs.Add(fishId);
            UserSaveManager.Save(user);
        }

       // DataContainerManager.Instance.SaveAllData();
    }

    /// <summary>Removes a fish from the favourites list and persists.</summary>
    public void RemoveLikeData(int fishId)
    {
        _likeFishID.Remove(fishId);

        UserData user = UserSaveManager.Load();
        // Fixed: was inverted (!Contains) so removal never happened
        if (user.fishFullSaveData.favoriteFishIDs.Contains(fishId))
        {
            user.fishFullSaveData.favoriteFishIDs.Remove(fishId);
            UserSaveManager.Save(user);
        }

        //DataContainerManager.Instance.SaveAllData();
    }

    public void SaveIntList(string key, List<int> list)
    {
        PlayerPrefs.SetString(key, string.Join(",", list));
        PlayerPrefs.Save();
    }

    public List<int> LoadIntList(string key)
    {
        string value = PlayerPrefs.GetString(key, "");
        List<int> list = new List<int>();

        if (!string.IsNullOrEmpty(value))
        {
            foreach (string v in value.Split(','))
            {
                if (int.TryParse(v, out int parsed))
                    list.Add(parsed);
            }
        }

        return list;
    }

    public void Loadimage(string imageUrl, Image image, float maxWidth)
    {
        string url = imageUrl.Replace(@"\", "");
        Davinci.get()
            .load(url)
            .into(image)
            .withStartAction(() => { })
            .withDownloadProgressChangedAction((progress) => { })
            .withDownloadedAction(() => { })
            .withLoadedAction(() => { })
            .withErrorAction((error) => { Debug.Log("Image load error: " + error); })
            .withEndAction(() => { SetMaxSize(maxWidth, image); })
            .setFadeTime(0.1f)
            .setCached(true)
            .start();
    }

    public void SetMaxSize(float maxWidth, Image image)
    {
        float aspectRatio = image.sprite.rect.width / image.sprite.rect.height;
        float newWidth = maxWidth;
        float newHeight = newWidth / aspectRatio;
        image.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
