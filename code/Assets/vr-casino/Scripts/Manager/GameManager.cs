using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;


public class GameManager : MonoBehaviour
{
    private GameState _currentState;
    private GameAction _currentAction;
    private Coroutine _computerTurnCoroutine;



    /*[SerializeField]
    private AudioManager _audioManager;*/
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private UIManager_VR _uiManagerVR;
    [SerializeField]
    private Dealer _dealer;

    [SerializeField]
    private HumanPlayer _human;

    [SerializeField]
    private ComputerPlayer _computer;

    public delegate void GameStateEvent(GameState state);
    public event GameStateEvent OnGameStateChanged;

    public delegate void GameActionEvent(GameAction actions);
    public event GameActionEvent OnGameActionChanged;

    public GameState CurrentState {
        get {
            return _currentState;
        }
        set {
            if (_currentState != value) {
                _currentState = value;

                if (OnGameStateChanged != null) {
                    OnGameStateChanged(_currentState);
                }
            }
        }
    }

    private GameAction CurrentAction {
        get {
            return _currentAction;
        }
        set {
            if (_currentAction != value) {
                _currentAction = value;

                if (OnGameActionChanged != null) {
                    OnGameActionChanged(_currentAction);
                }
            }
        }
    }

    private void Start()
    {
              
        Subscriptions();

        OnNewGameEvent();

 
    }

    private void Subscriptions()
    {
        _uiManager.OnDealButtonEvent += OnButtonEvent;
        _uiManager.OnHitButtonEvent += OnButtonEvent;
        _uiManager.OnStandButtonEvent += OnButtonEvent;
        _uiManagerVR.OnNewGameButtonEvent += OnButtonEvent;
        _uiManagerVR.OnExitButtonEvent += OnButtonEvent;

        _uiManager.OnDealButtonEvent += OnCardEvent;
        _uiManager.OnHitButtonEvent += OnCardEvent;

        _uiManager.OnDealButtonEvent += OnDealEvent;
        _uiManager.OnHitButtonEvent += OnHitEvent;
        _uiManager.OnStandButtonEvent += OnStandEvent;
        _uiManagerVR.OnNewGameButtonEvent += OnNewGameEvent;
        _uiManagerVR.OnExitButtonEvent += OnExitEvent;

        OnGameActionChanged += _uiManager.OnUpdateGameplayButtons;

    }

    private void OnButtonEvent()
    {
        //_audioManager.PlayButtonClip();
    }

    private void OnCardEvent()
    {
        //_audioManager.PlayCardClip();
    }

    private void OnDealEvent()
    {
        _human.LockBettingValue();
        _dealer.Deal(_human);
        _dealer.Deal(_computer, false);
        EvaluateHands(GameState.HumanTurn);
    }

    private void OnHitEvent()
    {
        _dealer.GiveCard(_human);

        EvaluateHands(GameState.ComputerTurn);
    }

    private void OnStandEvent()
    {
        _human.IsHitting = false;

        EvaluateHands(GameState.ComputerTurn);
    }

    private void OnNewGameEvent()
    {
        _dealer.Reset(_human, _computer);

        CurrentState = GameState.None;
        CurrentAction = GameAction.Deal;
    }

    private void OnExitEvent()
    {
        if (_computerTurnCoroutine != null) {
            StopCoroutine(_computerTurnCoroutine);
        }
        Application.Quit();
    }

    private IEnumerator OnComputerTurn()
    {
        yield return _computer.TurnWaitForSeconds;

        _computer.UpdateBehaviour(_human.Hand);

        if (_computer.IsHitting) {
            _dealer.GiveCard(_computer);
        }

        EvaluateHands(GameState.HumanTurn);
    }

    private void EvaluateHands(GameState nextState)
    {
        bool moveToNextState = false;

        int computerTotalValue = _computer.Hand.TotalValue;
        int humanTotalValue = _human.Hand.TotalValue;

        if (_computer.IsHitting || _human.IsHitting) {
            if (humanTotalValue == 21 && computerTotalValue == 21) {
                CurrentState = GameState.Draw;
            }
            else if (computerTotalValue > 21 || humanTotalValue == 21 || (humanTotalValue > computerTotalValue && !_computer.IsHitting)) {
                CurrentState = GameState.HumanWon;
            }
            else if (humanTotalValue > 21 || computerTotalValue == 21 || (computerTotalValue > humanTotalValue && !_human.IsHitting)) {
                CurrentState = GameState.ComputerWon;
            }
            else {
                moveToNextState = true;
            }
        }
        else if (!_computer.IsHitting && !_human.IsHitting) {
            if (computerTotalValue > humanTotalValue) {
                CurrentState = GameState.ComputerWon;
            }
            else if (humanTotalValue > computerTotalValue) {
                CurrentState = GameState.HumanWon;
            }
            else {
                CurrentState = GameState.Draw;
            }
        }

        if (moveToNextState) {
            if ((nextState == GameState.HumanTurn && _human.IsHitting) || (nextState == GameState.ComputerTurn && !_computer.IsHitting)) {
                CurrentState = GameState.HumanTurn;
                CurrentAction = GameAction.HitAndStand;
            }
            else {
                CurrentState = GameState.ComputerTurn;
                CurrentAction = GameAction.None;

                _computerTurnCoroutine = StartCoroutine(OnComputerTurn());
            }
        }
        else {
            EndGame();
        }
    }

    private void EndGame()
    {
        _computer.Hand.Show();

        if (CurrentState == GameState.HumanWon) {
            _human.GetComponent<ChipHandler>().GenerateChipsForPlayer(_human.CurrentBet * 2);
            _human.Score++;

            Debug.Log("HumanWon");
        }
        else if (CurrentState == GameState.ComputerWon) {
            _computer.Score++;
            Debug.Log("ComputerWon");
        }

        CurrentAction = GameAction.NewGame;

        _uiManagerVR.UpdateScore(_human.Score, _computer.Score);
    }
}