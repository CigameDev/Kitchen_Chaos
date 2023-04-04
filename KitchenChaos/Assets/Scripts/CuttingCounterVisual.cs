using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script nay gan cho con dao(cuttingcountervisual)
/// de xu ly goi animator khi thuc hien hanh dong cut
/// </summary>
public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;

    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

}
