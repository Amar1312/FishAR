using UnityEngine;

public class SetFishPosition : MonoBehaviour
{
    public FishRandomMovement _fishMovement;
    public Vector3 _offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _fishMovement.areaCenter = transform.position + _offset;

    }
}
