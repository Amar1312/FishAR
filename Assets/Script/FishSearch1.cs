using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FishSearch1 : MonoBehaviour
{
    public Button _allSpeciesBtn, _koiFish, _fGoldFish, _goldFish, _normalFish, _likeBtn,_container;
    public List<GameObject> _selectBtnImage;


    public static List<FishComponent> namesList = new List<FishComponent>();


    private void OnEnable()
    {

        AllSpeciesBtnClick();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _allSpeciesBtn.onClick.AddListener(AllSpeciesBtnClick);
        _koiFish.onClick.AddListener(koiFshClick);
        _fGoldFish.onClick.AddListener(FGoldFishClick);
        _goldFish.onClick.AddListener(GoldFishClick);
        _normalFish.onClick.AddListener(NormalFish);
        _container.onClick.AddListener(ContainerClick);
        _likeBtn.onClick.AddListener(LikeBtnClick);
        namesList = UIManager.Instance._allPointFish;


    }

    void AllSpeciesBtnClick()
    {
        OnAllFish();
        OnSelectImage(0);
    }
    void OnAllFish()
    {
        for (int i = 0; i < UIManager.Instance._allPointFish.Count; i++)
        {
            UIManager.Instance._allPointFish[i].gameObject.SetActive(true);
        }
    }

    void koiFshClick()
    {
        Debug.Log("KoiFish Clicked");
        OnTagFish("KoiFish");
        OnSelectImage(1);
    }
    void ContainerClick()
    {
        Debug.Log("Container Clicked");
        OnTagFish("Container");
        OnSelectImage(5);
    }
    void FGoldFishClick()
    {
        Debug.Log("FGoldFish Clicked");
        OnTagFish("FGoldFish");
        OnSelectImage(2);
    }

    void GoldFishClick()
    {
        Debug.Log("FGoldFish Clicked");
        Debug.Log("GoldFish Clicked");
        OnTagFish("GoldFish");
        OnSelectImage(3);
    }
    void NormalFish()
    {
        Debug.Log("NormalFish Clicked");
        OnTagFish("NormalFish");
        OnSelectImage(4);
    }




    void OnTagFish(string tag)
    {
        for (int i = 0; i < UIManager.Instance._allPointFish.Count; i++)
        {
            Debug.Log("Fish count: " + UIManager.Instance._allPointFish.Count);

            FishAllDetail detail = UIManager.Instance._allPointFish[i]._fishDetail;
            Debug.Log("Fish Name: " + detail.fishName);
            CategoryType type = detail.category;
            string typeString = type.ToString();
            if (typeString == tag)
            {
                UIManager.Instance._allPointFish[i].gameObject.SetActive(true);
            }
            else
            {
                UIManager.Instance._allPointFish[i].gameObject.SetActive(false);
            }

        }
    }

    void LikeBtnClick()
    {
        Debug.Log("LikeBtn Clicked");
        OnSelectImage(6);
        for (int i = 0; i < UIManager.Instance._allPointFish.Count; i++)
        {

            FishAllDetail detail = UIManager.Instance._allPointFish[i]._fishDetail;
            Debug.Log("Fish Name: " + detail.fishName);

            bool like = detail.fevarit;
                if (like)
                {
                    UIManager.Instance._allPointFish[i].gameObject.SetActive(true);
                }
                else
                {
                    UIManager.Instance._allPointFish[i].gameObject.SetActive(false);
                }
            
        }
    }

    void OnSelectImage(int Index)
    {
        for (int i = 0; i < _selectBtnImage.Count; i++)
        {
            _selectBtnImage[i].SetActive(false);
        }
        _selectBtnImage[Index].SetActive(true);
    }
}
