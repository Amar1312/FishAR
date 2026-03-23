using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AllFishPanel : MonoBehaviour
{
    public Button _arBtn, _homeBtn, _aquariumBtn, _profileBtn;
    public TextMeshProUGUI _pointText;

    private void OnEnable()
    {
        _pointText.text = PointManager.Instance._point.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _arBtn.onClick.AddListener(ArBtnClick);
        _aquariumBtn.onClick.AddListener(AquariumBtnClick);
        _profileBtn.onClick.AddListener(ProfileBtnClick);
        _homeBtn.onClick.AddListener(HomeBtnClick);
    }

    void ArBtnClick()
    {
        SceneManager.LoadScene(1);
    }

    void AquariumBtnClick()
    {
        HomeSceneManager.Instance._myCollectionScript.gameObject.SetActive(true);
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
}
