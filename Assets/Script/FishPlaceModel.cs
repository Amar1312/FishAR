using UnityEngine;

public class FishPlaceModel : MonoBehaviour
{
    public FishComponent _fishButtonScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AddPoint();
    }


    public void AddPoint()
    {
        _fishButtonScript.AddPoint();
    }

}
