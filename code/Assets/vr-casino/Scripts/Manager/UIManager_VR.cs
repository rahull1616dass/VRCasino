using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Events;
using System;

namespace Valve.VR.InteractionSystem
{
    public class UIManager_VR : MonoBehaviour
{
    private Image _audioImage;
    private bool _isAudioDisabled;
        //private Sprite _audioOnSprite;

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private HoverButton _dealButton;
    [SerializeField]
    private HoverButton _hitButton;
    [SerializeField]
    private HoverButton _standButton;
    [SerializeField]
    private HoverButton _newGameButton;
    [SerializeField]
    private HoverButton _exitButton;
    [SerializeField]
    private HoverButton _audioButton;
    [SerializeField]
     private AudioSource gameAudio;

        //private InputActionReference _debugButtonDeal, _debugButtonNewGame, _debugButtonHit;
    /*[SerializeField]
    private Sprite _audioOffSprite;*/

    public delegate void ButtonEvent();
    public event ButtonEvent OnDealButtonEvent = delegate { };
    public event ButtonEvent OnHitButtonEvent = delegate { };
    public event ButtonEvent OnStandButtonEvent = delegate { };
    public event ButtonEvent OnNewGameButtonEvent = delegate { };
    public event ButtonEvent OnExitButtonEvent = delegate { };

    public delegate bool ButtonEventWithBoolean();
    public event ButtonEventWithBoolean OnAudioButtonEvent = delegate() { return false; };

    private void Start()
    {
         _dealButton.onButtonDown.AddListener((Hand arg0) => OnDealButtonEvent());
        _hitButton.onButtonDown.AddListener((Hand arg0) => OnHitButtonEvent());
        _standButton.onButtonDown.AddListener((Hand arg0) => OnStandButtonEvent());
        _newGameButton.onButtonDown.AddListener((Hand arg0) => OnNewGameButtonEvent());
        _exitButton.onButtonDown.AddListener((Hand arg0) => OnExitButtonEvent());

        _audioButton.onButtonDown.AddListener((Hand arg0) => OnAudioButtonClick());
        _audioImage = _audioButton.GetComponent<Image>();
        //_debugButtonDeal.action.performed += DealButton;
        //_debugButtonNewGame.action.performed += NewGameButton;
        //_debugButtonHit.action.performed += HitGameButton;

        _isAudioDisabled = false;
        gameAudio.Play();

            //_audioOnSprite = _audioImage.sprite;

            //_scoreText =  gameObject.GetComponent<TextMeshProUGUI>();

        }

    public void OnButtonDown(Hand fromHand)
    {
        ColorSelf(Color.cyan);
    }

    public void OnButtonUp(Hand fromHand)
    {
        ColorSelf(Color.white);
    }

    private void ColorSelf(Color newColor)
    {
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
        for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
        {
            renderers[rendererIndex].material.color = newColor;
        }
    }

    public void OnUpdateGameplayButtons(GameAction action)
    {
        _dealButton.enabled = (action == GameAction.Deal);
        _hitButton.enabled = (action == GameAction.HitAndStand);
        _standButton.enabled = (action == GameAction.HitAndStand);
        _newGameButton.enabled = (action == GameAction.NewGame);
    }

    public void UpdateScore(int humanScore, int computerScore)
    {
        _scoreText.text = string.Format("Player: {0}\nComputer: {1}", humanScore, computerScore);
    }

    private void OnAudioButtonClick()
    {
        //bool _isAudioDisabled = OnAudioButtonEvent();
            if (_isAudioDisabled)
            {
                gameAudio.Play();
                _isAudioDisabled = false;
            }
            else
            {
                gameAudio.Pause();
                _isAudioDisabled = true;
                //_audioImage.sprite = _audioOnSprite;
            }
        }

    public void DealButton(InputAction.CallbackContext context)
    {
        Debug.Log("Caalling");
        OnDealButtonEvent();
    }

    public void NewGameButton(InputAction.CallbackContext context)
    {
        Debug.Log("NewGame");
        OnNewGameButtonEvent();
    }

    public void HitGameButton(InputAction.CallbackContext context)
    {
        Debug.Log("Hit");
        OnHitButtonEvent();
    }
}
}
