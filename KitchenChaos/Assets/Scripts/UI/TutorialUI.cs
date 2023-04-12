using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    private void Start()
    {
        UpdateVisual();

        KitchenGameManager.Instance.OnStateChanged += KitchenManager_OnStateChanged;

        Show();
        //ban dau vao game thi show luon tutorial(state = waitingToStart)
        //Khi thay doi state lan dau tien(waitingToStart=> countdownToStart) thi an tutorial di
    }

    private void KitchenManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCoundownToStartActive())
        {
            Hide();
        }    
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        //keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
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
