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

    void ShopOwnerResponce1(ShopOwnerResponce responce)
    {
        if (responce.status)
        {
            PointManager.Instance._shopOwnerResponce = responce;
            PointManager.Instance.Loadimage(PointManager.Instance._shopOwnerResponce.result.logo, _logoImage, 850f);

            //SceneManager.LoadScene(0);
        }
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

        if (PlayerPrefs.HasKey("QRC") && string.IsNullOrWhiteSpace(_pointManager._shopOwnerResponce.result.logo))
        {
            string _qrCode = PlayerPrefs.GetString("QRC");

            if (_qrCode != null || _qrCode == "")
            {
                APIManager.Instance.ShopOwnerIn(_qrCode, ShopOwnerResponce1);
            }
        }
        else
        {

            if (string.IsNullOrWhiteSpace(_pointManager._shopOwnerResponce.result.logo))
            {
                return;
            }

            _pointManager.Loadimage(_pointManager._shopOwnerResponce.result.logo, _logoImage, 850f);
        }

       


    }
}
