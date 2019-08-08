using System;
using UnityEngine;
using ParkourRunner.Scripts.Player.InvectorMods;

public class TutorialMessageTrigger : MonoBehaviour
{
    public static event Action<string> OnSendMessage;

    [SerializeField] private LocalizationComponent _localizationText;
    [SerializeField] private string _text;    
    
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ParkourThirdPersonController>();

        if (target != null)
            OnSendMessage.SafeInvoke(_localizationText == null ? _text : _localizationText.Text);
    }
}