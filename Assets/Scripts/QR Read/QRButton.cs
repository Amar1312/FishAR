using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Click state of QRButton
/// </summary>
public class QRButton : MonoBehaviour
{
    public static bool clicked = false;

    void Start()
    {
        //InvokeRepeating(nameof(Click), 0.5f, 0.3f);
    }

    void LateUpdate ()
    {
        //clicked = false;
    }
    public void Click()
    {
        clicked = true;
    }

    public void BackBtnClick()
    {
        SceneManager.LoadScene(0);
    }
}