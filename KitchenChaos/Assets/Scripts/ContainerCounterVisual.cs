using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// script nay gan cho ContainerCounterVisual
/// thuc hien animator dong mo containerCounterVisual
/// </summary>
public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedKitchen += ContainerCounter_OnPlayerGrabbedKitchen;

    }
    private void ContainerCounter_OnPlayerGrabbedKitchen(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
