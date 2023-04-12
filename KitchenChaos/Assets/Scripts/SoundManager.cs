using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYE_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYE_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;//sender chinh la cuttingCounter
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        //phat nhac tai vi tri deliveryCounter
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position );
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip , Vector3 position,float volumeMultiplier =1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }    
    public void PlayFootStepSound(Vector3 position,float volume)
    {
        int length = audioClipRefsSO.footstep.Length;
        PlaySound(audioClipRefsSO.footstep[Random.Range(0, length)], position,volume);
    }  
    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning,Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYE_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }    

    public float GetVolume()
    {
        return volume;
    }    
    
}
