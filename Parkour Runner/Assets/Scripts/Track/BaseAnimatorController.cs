using UnityEngine;
using ParkourRunner.Scripts.Player.InvectorMods;
using AEngine;

public class BaseAnimatorController : MonoBehaviour
{
    public enum AnimationKinds
    {
        Default,
        PlatformUp,
        Bridge
    }

    [SerializeField] protected AnimationKinds _animationKind;

    protected Transform _player;
    protected AudioManager _audio;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _player = ParkourThirdPersonController.instance.transform;
        _audio = AudioManager.Instance;
    }

    protected void PlaySound()
    {
        switch (_animationKind)
        {
            case AnimationKinds.Default:
                break;

            case AnimationKinds.PlatformUp:
                _audio.PlaySound(Sounds.PlatformUp);
                break;

            case AnimationKinds.Bridge:
                _audio.PlaySound(Sounds.Bridge);
                break;
        }
    }
}
