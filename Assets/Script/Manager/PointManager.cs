using UnityEngine;
using UnityEngine.UI;
using System;
using Mihir;
using System.Collections.Generic;

public class PointManager : Singleton<PointManager>
{
    public int _point;
    //public static PointManager Instance;

    public ShopOwnerResponce _shopOwnerResponce;
    public List<int> _unLockFishID;
    public List<int> _categoryComplete;
    public List<int> _likeFishID;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (PlayerPrefs.HasKey("Point"))
        {
            _point = PlayerPrefs.GetInt("Point");
        }
        _unLockFishID = LoadIntList("FishID");
        _categoryComplete = LoadIntList("CategoryData");
        _likeFishID = LoadIntList("LikeFishData");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void AddPoint(int Point)
    {
        _point += Point;
        PlayerPrefs.SetInt("Point", _point);
    }

    public void Removepoint(int Point)
    {
        if (_point >= Point)
        {
            _point -= Point;
            PlayerPrefs.SetInt("Point", _point);
        }
        else
        {
            Debug.Log("Point is Not Sufficient");
        }
    }
    public void UnLockFishSave(int Id)
    {
        if (!_unLockFishID.Contains(Id))
        {
            _unLockFishID.Add(Id);
        }
        SaveIntList("FishID", _unLockFishID);
    }

    public void SetCategoryData(int Level)
    {
        if (!_categoryComplete.Contains(Level))
        {
            _categoryComplete.Add(Level);
        }
        SaveIntList("CategoryData", _categoryComplete);
    }

    public void AddLikeData(int fishId)
    {
        if (!_likeFishID.Contains(fishId))
        {
            _likeFishID.Add(fishId);
        }
        SaveIntList("LikeFishData", _likeFishID);
    }

    public void RemoveLikeData(int fishId)
    {
        if (_likeFishID.Contains(fishId))
        {
            _likeFishID.Remove(fishId);
        }
        SaveIntList("LikeFishData", _likeFishID);
    }

    public void SaveIntList(string key, List<int> list)
    {
        string value = string.Join(",", list);
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public List<int> LoadIntList(string key)
    {
        string value = PlayerPrefs.GetString(key, "");

        List<int> list = new List<int>();

        if (!string.IsNullOrEmpty(value))
        {
            string[] values = value.Split(',');

            foreach (string v in values)
            {
                list.Add(int.Parse(v));
            }
        }

        return list;
    }
    

    public void Loadimage(string ImageUrl, Image _image, float maxWidth)
    {
        //Debug.Log("first Load Image............................................................................");
        string url = ImageUrl.Replace(@"\", "");
        //Debug.Log("URL : " + url);
        Davinci.get()
       .load(url)
       .into(_image)
       .withStartAction(() =>
       {
           //statusTxt.text = "Download has been started.";
           //Debug.Log("Download Is Started..................");
           //UIManager.UIInstance.ToggleLoadingPanel(true);
       })
       .withDownloadProgressChangedAction((progress) =>
       {
           //statusTxt.text = "Download progress: " + progress;
       })
       .withDownloadedAction(() =>
       {
           //statusTxt.text = "Download has been completed.";
           //Debug.Log("Download Is Completed..................");
       })
       .withLoadedAction(() =>
       {
           // statusTxt.text = "Image has been loaded.";
           //Debug.Log("Image Is Downloaded.........................");
       })
       .withErrorAction((error) =>
       {
           // statusTxt.text = "Got error : " + error;
           Debug.Log("Error : " + error);
           //UIManager.UIInstance.ToggleLoadingPanel(false);
       })
       .withEndAction(() =>
       {
           //print("Operation has been finished.");
           //UIManager.UIInstance.ToggleLoadingPanel(false);


           SetMaxSize(maxWidth, _image);
       })

       .setFadeTime(0.1f)
       .setCached(true)
       .start();
        //_loadOnce = true;
    }
    public void SetMaxSize(float maxWidth, Image _meImage)
    {
        // Get the aspect ratio of the image
        float aspectRatio = _meImage.sprite.rect.width / _meImage.sprite.rect.height;

        // Calculate the new size based on the maximum width and height
        float newWidth = Mathf.Min(maxWidth, _meImage.sprite.rect.width);

        if (newWidth < maxWidth)
        {
            newWidth = maxWidth;
        }

        float newHeight = newWidth / aspectRatio;
        //Debug.Log(newWidth + " new Width " + _sliderDataList.id);

        //if (newHeight > maxHeight)
        //{
        //    newHeight = maxHeight;
        //    newWidth = newHeight * aspectRatio;
        //    //Debug.Log(newHeight + " new Hight " + _sliderDataList.id);
        //}


        // Set the size
        RectTransform rectTransform = _meImage.rectTransform;
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
