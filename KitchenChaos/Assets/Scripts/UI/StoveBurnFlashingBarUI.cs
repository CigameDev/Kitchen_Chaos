using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    private Animator animator;

    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventAgrs e)
    {
        float burnShowProgessAmount = 0.5f;
        bool show = e.progressNormalized >= burnShowProgessAmount && stoveCounter.IsFried();

        animator.SetBool(IS_FLASHING, show);
    }

    

}
