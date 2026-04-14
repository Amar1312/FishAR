using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MyCollectionPanel : MonoBehaviour
{
    public Button _arBtn, _homeBtn, _exploreBtn, _profileBtn;
    public Button _allBtn, _saltwaterBtn, _freshwaterBtn;
    public List<GameObject> _selectBtnImage;
    public List<CollectionFishComponent> _allFishComponent;
    public List<CollectionFishComponent> _collectedFish;

    public Scrollbar _collectScrollbar;
    public TextMeshProUGUI _collectText;
    public TextMeshProUGUI _totalText;
    public TextMeshProUGUI _levelText;
    public TextMeshProUGUI _levelPerText;
    

    private void OnEnable()
    {
   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _arBtn.onClick.AddListener(ArBtnClick);
        _exploreBtn.onClick.AddListener(ExploreBtnClick);
        _profileBtn.onClick.AddListener(ProfileBtnClick);
        _homeBtn.onClick.AddListener(HomeBtnClick);

        _allBtn.onClick.AddListener(AllBtnClick);
        _saltwaterBtn.onClick.AddListener(SaltwaterBtnClick);
        _freshwaterBtn.onClick.AddListener(FreshwaterBtnClick);


        CheckCollectedFish();


    }

    void ArBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    void ExploreBtnClick()
    {
        HomeSceneManager.Instance._allFishScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void ProfileBtnClick()
    {
        HomeSceneManager.Instance._profileScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void HomeBtnClick()
    {
        HomeSceneManager.Instance._homePanelScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    void AllBtnClick()
    {
        for (int i = 0; i < _collectedFish.Count; i++)
        {
            _collectedFish[i].gameObject.SetActive(true);
        }
        SelectBtnImageSet(0);
    }
    void SaltwaterBtnClick()
    {
        OnTagFish(tagType.Saltwater);
        SelectBtnImageSet(1);
    }

    void FreshwaterBtnClick()
    {
        OnTagFish(tagType.Freshwater);
        SelectBtnImageSet(2);
    }
    void OnTagFish(tagType tag)
    {
        for (int i = 0; i < _collectedFish.Count; i++)
        {
            tagType type = _collectedFish[i]._fishDetail.tag;
            if (type == tag)
            {
                _collectedFish[i].gameObject.SetActive(true);
            }
            else
            {
                _collectedFish[i].gameObject.SetActive(false);
            }

        }
    }

    void SelectBtnImageSet(int Index)
    {
        for (int i = 0; i < _selectBtnImage.Count; i++)
        {
            _selectBtnImage[i].SetActive(false);
        }
        _selectBtnImage[Index].SetActive(true);
    }

    void CheckCollectedFish()
    {
        UserData user = UserSaveManager.Load();

        _collectedFish.Clear();
        for (int i = 0; i < _allFishComponent.Count; i++)
        {
            if (user.fishFullSaveData.unlockedFishIDs.Contains(_allFishComponent[i]._fishId))
            {
                _collectedFish.Add(_allFishComponent[i]);
                _allFishComponent[i].gameObject.SetActive(true);
            }
            else
            {
                _allFishComponent[i].gameObject.SetActive(false);
            }
        }

        float data = _allFishComponent.Count > 0
            ? (float)_collectedFish.Count / _allFishComponent.Count
            : 0f;
        float per = data * 100f;
        string result = per.ToString("F1");
        _totalText.text = result + " %";

        // Fixed: was int.Parse which crashes on decimal strings like "62.5"
        user.progressPercent = float.Parse(result, System.Globalization.CultureInfo.InvariantCulture);

        UserSaveManager.Save(user);
       // DataContainerManager.Instance.SaveAllData();

        SetLevelData();
    }

    void SetLevelData()
    {
        int collectedCount = _collectedFish.Count;
        const int groupSize = 5;

        // Guard against zero collected fish to avoid negative level/num
        int level = collectedCount == 0 ? 1 : (collectedCount - 1) / groupSize + 1;
        int num   = collectedCount == 0 ? 0 : (collectedCount - 1) % groupSize + 1;

        _levelText.text = "Level " + level + " Explorer";
        _collectText.text = num + " / " + groupSize + " Fish Collected";

        float data = (float)num / groupSize;
        _collectScrollbar.size = data;
        _levelPerText.text = "Level " + level + " at " + (data * 100f).ToString("F1") + " %";

        UserData user = UserSaveManager.Load();
        user.level = level;
        UserSaveManager.Save(user);

        //DataContainerManager.Instance.SaveAllData();

        AllBtnClick();
    }
}
