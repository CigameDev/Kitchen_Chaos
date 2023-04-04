using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;//co the la cuttingCounter,StoveCounter
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress =  hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("Game Object "+hasProgressGameObject.name +"Khong co hasProgress ");
        }    
        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;
        HideBar();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventAgrs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized==0 ||e.progressNormalized ==1)
        {
            HideBar();
        }   
        else
        {
            ShowBar();
        }    
    }
    private void ShowBar()
    {
        this.gameObject.SetActive(true);    
    }   
    private void HideBar()
    {
        this.gameObject.SetActive(false);
    }    
}
