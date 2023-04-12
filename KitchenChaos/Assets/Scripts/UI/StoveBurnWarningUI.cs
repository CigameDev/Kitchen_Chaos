using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventAgrs e)
    {
        float burnShowProgessAmount = 0.5f;
        bool show = e.progressNormalized >= burnShowProgessAmount && stoveCounter.IsFried();
        //phai o trang thai chay IsFried()==true;

        if(show)
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
