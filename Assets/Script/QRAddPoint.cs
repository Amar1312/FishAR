using UnityEngine;
using TMPro;

public class QRAddPoint : MonoBehaviour
{
    public TextMeshProUGUI _messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OpenMessagePanel(string message)
    {
        _messageText.text = message;
        gameObject.SetActive(true);
        Invoke(nameof(OffPanel), 2f);
    }

    void OffPanel()
    {
        gameObject.SetActive(false);
    }
}
