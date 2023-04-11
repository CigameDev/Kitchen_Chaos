using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coundownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
       // coundownText.text = KitchenGameManager.Instance.GetCountDownToStartTimer().ToString("F2");
       //mathf.ceil lam tron len
       coundownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountDownToStartTimer()).ToString();
    }
    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCoundownToStartActive())
        {
            Show();
        }   
        else
        {
            Hide();
        }    
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }    
    private void Hide()
    {
        gameObject.SetActive(false);
    }    
}
