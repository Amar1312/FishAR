using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Text;

public class APIManager : MonoBehaviour
{

    public static APIManager Instance;
    public GameObject _loadingPanel;

    private const string APIBASEURL = "https://1c12-2402-a00-192-16ae-91e-ceaf-f91a-1036.ngrok-free.app/mixed_place/api";
    private const string SHOPOWNER = APIBASEURL + "/shop-owner";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void APiCall()
    {
        QRManager.Instance.ShopOwnerAPiCall("MP-BSJ12TNQ");
    }


    #region Check_InternetConnection 

    private void CheckInternet(Action<bool> action)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            action(true);
        }
        else
        {
            action(false);
        }
    }
    #endregion

    #region Shop owner 
    public void ShopOwnerIn(string _qrText, Action<ShopOwnerResponce> response)
    {
        CheckInternet(status =>
        {
            if (status)
                StartCoroutine(ShopOwnerIEnum(_qrText, response));
            else
                Debug.Log("Error No Internet Connection");
        });
    }

    private IEnumerator ShopOwnerIEnum(string qrText, Action<ShopOwnerResponce> response)
    {
        _loadingPanel.SetActive(true);

        ShopOwnerClass _rawdata = new ShopOwnerClass(qrText);
        string rawstring = JsonUtility.ToJson(_rawdata);

        UnityWebRequest request = new UnityWebRequest(SHOPOWNER, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(rawstring);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("api-key", "MIXEDPLACE");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("platform", "WEB");

        var callback = new ShopOwnerResponce();
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.responseCode == 200 || request.responseCode == 201)
        {
            Debug.Log("Shop owner Response: " + request.downloadHandler.text);
            callback = JsonUtility.FromJson<ShopOwnerResponce>(request.downloadHandler.text);
            callback.status = true;
        }
        else if (request.responseCode == 401)
        {
            Debug.Log("Shop owner unAuthorized");
            callback.status = false;
            callback.message = "Unauthorized";
        }
        else
        {
            Debug.Log(request.responseCode);
            callback = JsonUtility.FromJson<ShopOwnerResponce>(request.downloadHandler.text);
            callback.status = false;
            if (callback.message == "")
                callback.message = "Something went worng";
            Debug.Log("Shop owner Error: " + request.downloadHandler.text);
        }

        _loadingPanel.SetActive(false);

        response.Invoke(callback);

        request.Dispose();
    }

    #endregion
}


#region DummyClass
public class ShopOwnerClass
{
    public string store_id;
    public ShopOwnerClass(string Store_id)
    {
        store_id = Store_id;
    }
}

#endregion