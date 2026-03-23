using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomePanel : MonoBehaviour
{
    public Button _worldBtn, _exploreFishBtn, _qrBtn;
    public Image _logoImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _worldBtn.onClick.AddListener(WorldBtnClick);
        _exploreFishBtn.onClick.AddListener(ExploreFishBtnClick);
        _qrBtn.onClick.AddListener(QrBtnClick);
        CheckQRScan();
    }

    public void WorldBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExploreFishBtnClick()
    {
        HomeSceneManager.Instance._allFishScript.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void QrBtnClick()
    {
        SceneManager.LoadScene(2);
    }

    void CheckQRScan()
    {
        PointManager _pointManager = PointManager.Instance;

        if (string.IsNullOrWhiteSpace(_pointManager._shopOwnerResponce.result.logo))
        {
            return;
        }

        _pointManager.Loadimage(_pointManager._shopOwnerResponce.result.logo, _logoImage, 850f);
    }
}
