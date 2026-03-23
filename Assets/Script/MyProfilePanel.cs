using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MyProfilePanel : MonoBehaviour
{
    public Button _arBtn, _homeBtn, _exploreBtn, _aquariumBtn;
    public TextMeshProUGUI _mainLevelText, _totalPerText, _totalFishCollectText, _pointText;
    public TextMeshProUGUI _levelExplorerText, _fishCollectText, _levelPerText;
    public Scrollbar _collectScrollbar;

    private void OnEnable()
    {
        DisplayData();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _arBtn.onClick.AddListener(ArBtnClick);
        _aquariumBtn.onClick.AddListener(AquariumBtnClick);
        _exploreBtn.onClick.AddListener(ExploreBtnClick);
        _homeBtn.onClick.AddListener(HomeBtnClick);
    }

    void ArBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    void HomeBtnClick()
    {
        HomeSceneManager.Instance._homePanelScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void AquariumBtnClick()
    {
        HomeSceneManager.Instance._myCollectionScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void ExploreBtnClick()
    {
        HomeSceneManager.Instance._allFishScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void DisplayData()
    {
        PointManager _pointManager = PointManager.Instance;
        _pointText.text = _pointManager._point.ToString();
        int totalFish = HomeSceneManager.Instance._myCollectionScript._allFishComponent.Count;
        int unLockFish = _pointManager._unLockFishID.Count;

        float Data = (float)unLockFish / totalFish;
        float per = Data * 100;
        string result = per.ToString("F1");
        _totalPerText.text = result + " %";

        _totalFishCollectText.text = "Total " + unLockFish.ToString() + " / " + totalFish.ToString() + " Fish Collected";
        SetLevelData();
    }
    void SetLevelData()
    {
        int colloctData = PointManager.Instance._unLockFishID.Count;
        int num = 0;
        int Level = 0;

        if (colloctData <= 5)
        {
            num = colloctData;
            Level = 1;
        }
        else if (colloctData <= 10)
        {
            num = colloctData - 5;
            Level = 2;
        }
        else if (colloctData <= 15)
        {
            num = colloctData - 10;
            Level = 3;
        }
        else if (colloctData <= 20)
        {
            num = colloctData - 15;
            Level = 4;
        }
        else if (colloctData <= 25)
        {
            num = colloctData - 20;
            Level = 5;
        }
        else if (colloctData <= 30)
        {
            num = colloctData - 25;
            Level = 6;
        }

        _levelExplorerText.text = "Level " + Level.ToString() + " Explorer";
        _mainLevelText.text = "Level " + Level.ToString();

        _fishCollectText.text = num.ToString() + " / " + "5" + " Fish Collected";

        float Data = (float)num / 5;
        _collectScrollbar.size = Data;
        float per = Data * 100;
        string result = per.ToString("F1");

        _levelPerText.text = "Level " + Level.ToString() + " at " + result + " %";
    }
}
