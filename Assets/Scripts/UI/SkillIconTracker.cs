using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIconTracker : MonoBehaviour
{

    [SerializeField]
    private PlayerController _playerController;

    public CanvasGroup DashIcon;

    void Awake()
    {
        _playerController.OnDashAvailable += OnDashAvailable;
        _playerController.OnDashUnavailable += OnDashUnavailable;
    }

    private void OnDashAvailable() {
        //TODO: a good place to add some juicy animation for the dash icon activating
        DashIcon.alpha = 1.0f;
    }

    private void OnDashUnavailable() {
        //TODO: another good place to add some animations, maybe a shrink and dimming?
        DashIcon.alpha = 0.5f;
    }

}
