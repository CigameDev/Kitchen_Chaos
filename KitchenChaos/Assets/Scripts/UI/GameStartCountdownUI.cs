using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI coundownText;

    private const string NUMBER_POPUP = "NumberPopup";
    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        // coundownText.text = KitchenGameManager.Instance.GetCountDownToStartTimer().ToString("F2");
        //mathf.ceil lam tron len
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer());
       coundownText.text = countdownNumber.ToString();

        if(previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }    
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
