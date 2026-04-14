using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MyProfilePanel : MonoBehaviour
{
    public Button _arBtn, _homeBtn, _exploreBtn, _aquariumBtn;
    public TextMeshProUGUI _mainLevelText, _totalPerText, _totalFishCollectText, _pointText;
    public TextMeshProUGUI _levelExplorerText, _fishCollectText, _levelPerText;
    public TextMeshProUGUI _userName;
    public Scrollbar _collectScrollbar;

    private UserData user;

    private void OnEnable()
    {
        // Reload fresh user data every time the panel is shown
        user = UserSaveManager.Load();
        DisplayData();
    }

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
        // Write data TO the UI — never read from UI text fields
        _userName.text = user.userName;
        _pointText.text = user.points.ToString();

        int totalFish = HomeSceneManager.Instance._myCollectionScript._allFishComponent.Count;
        int unLockFish = user.fishFullSaveData.unlockedFishIDs.Count;

        float data = totalFish > 0 ? (float)unLockFish / totalFish : 0f;
        float per = data * 100f;
        _totalPerText.text = per.ToString("F1") + " %";
        _totalFishCollectText.text = "Total " + unLockFish + " / " + totalFish + " Fish Collected";

        SetLevelData();
    }

    void SetLevelData()
    {
        int collectedCount = user.fishFullSaveData.unlockedFishIDs.Count;
        const int groupSize = 5;

        // Guard against zero collected fish to avoid negative level/num
        int level = collectedCount == 0 ? 1 : (collectedCount - 1) / groupSize + 1;
        int num   = collectedCount == 0 ? 0 : (collectedCount - 1) % groupSize + 1;

        _levelExplorerText.text = "Level " + level + " Explorer";
        _mainLevelText.text = "Level " + level;
        _fishCollectText.text = num + " / " + groupSize + " Fish Collected";

        float data = (float)num / groupSize;
        _collectScrollbar.size = data;
        _levelPerText.text = "Level " + level + " at " + (data * 100f).ToString("F1") + " %";
    }
}

