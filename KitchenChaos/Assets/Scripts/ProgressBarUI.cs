using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        barImage.fillAmount = 0f;
        HideBar();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventAgrs e)
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
