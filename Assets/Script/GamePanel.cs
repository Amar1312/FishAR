using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePanel : MonoBehaviour
{
    public Button _backBtn;
    public GameObject _scrollObj;
    public ArTapToPlaceObject _tapToPlace;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backBtn.onClick.AddListener(BackBtnClick);
    }

    void BackBtnClick()
    {
       PointManager.scene = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void TapToPlace(GameObject placeObj, FishComponent fishButtonScript)
    {
        GameObject ObjIns = Instantiate(placeObj);
        ObjIns.SetActive(false);
        ObjIns.GetComponent<FishPlaceModel>()._fishButtonScript = fishButtonScript;
        _tapToPlace.objectToPlace = ObjIns;
        _tapToPlace._parentObject = ObjIns;
        _tapToPlace.HideObject = false;
        _tapToPlace.gameObject.SetActive(true);
        _scrollObj.SetActive(false);
    }
}
