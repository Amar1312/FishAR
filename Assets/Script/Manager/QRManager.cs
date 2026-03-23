using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
using Mihir;

public class QRManager : Singleton<QRManager>
{
    public string _qrPointData;
    public int _qrPoint;
    public TextMeshProUGUI _pointText;
    public QRAddPoint _qrAddPanel;
    public string _shopOwnwerQrText;

    public ReadQRCode _readQrCode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pointText.text = PointManager.Instance._point.ToString();
    }

    public void QRReadText(string qrData)
    {
        if (string.IsNullOrWhiteSpace(qrData))
        {
            return;
        }

        if(qrData == _shopOwnwerQrText)
        {
            ShopOwnerAPiCall(qrData);
            _readQrCode.StopScanQR();
        }

        if (qrData == _qrPointData)
        {
            CheckQRDailyCall();
        }
        else
        {
            _qrAddPanel.OpenMessagePanel("Wrong QR Code");
        }
    }

    void CheckQRDailyCall()
    {
        string lastDate = PlayerPrefs.GetString("LastQRDailyCall");

        string todayDate = DateTime.Now.ToString("yyyyMMdd");

        if (lastDate != todayDate)
        {
            DailyMethod();
            PlayerPrefs.SetString("LastQRDailyCall", todayDate);
        }
        else
        {
            _qrAddPanel.OpenMessagePanel("You Have already Get Point");
        }
    }

    void DailyMethod()
    {
        Debug.Log("Daily QR Method Called");
        AddPointQR(_qrPoint);
        _qrAddPanel.OpenMessagePanel("You Have " + _qrPoint.ToString() + " Point");
    }

    public void AddPointQR(int point)
    {
        PointManager.Instance.AddPoint(point);
        _pointText.text = PointManager.Instance._point.ToString();
    }


    public void ShopOwnerAPiCall(string QRText)
    {
        APIManager.Instance.ShopOwnerIn(QRText, ShopOwnerResponce1);
    }

    void ShopOwnerResponce1(ShopOwnerResponce responce)
    {
        if (responce.status)
        {
            PointManager.Instance._shopOwnerResponce = responce;
            SceneManager.LoadScene(0);
        }
    }
}
