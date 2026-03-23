using UnityEngine;

public class PondWater : MonoBehaviour
{
    public GameObject _parentObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(_parentObject.transform.position + " pond Water Poisition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
