using UnityEngine;

namespace UIParty
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UITuto _tutoWindow;
        [SerializeField] private UILevel _levelWindow;
        [SerializeField] private UIWin _winWindow;
        [SerializeField] private UILose _loseWindow;
        [SerializeField] private UITransitionScreen _transitionWindow;
        [SerializeField] UIFeedback _feedbackWindow;

        private Canvas _m_uiCanvas;
        public Canvas UICanvas => _m_uiCanvas;
        public UIFeedback feedbackWindow => _feedbackWindow;

        public void Init()
        {
            _tutoWindow.Init();
            _levelWindow.Init(Root.DataManager.levelData);
            _winWindow.Init();
            _loseWindow.Init();
            _feedbackWindow.Init();

            _transitionWindow.OnFullSplashScreen += FullSplashScreen;
            _transitionWindow.OnClosedSplashScreen += ClosedSplashScreen;

            _transitionWindow.Init();
            _transitionWindow.OnClosedSplashScreen?.Invoke();

            _m_uiCanvas = GetComponent<Canvas>();
        }

        public void OpenMenu()
        {
            _levelWindow.Open();
            _tutoWindow.Open();

            if (!Root.GameManager.gameStart)
                _tutoWindow.Open();
        }

        public void GameStarted()
        {
            _tutoWindow.Close();
        }

        public void OpenWinWindow()
        {
            _levelWindow.Close();
            _winWindow.Open();
        }

        public void OpenLoseWindow()
        {
            _levelWindow.Close();
            _loseWindow.Open();
        }

        public void FullSplashScreenOpeen()
        {
            _transitionWindow.Open();
        }

        private void FullSplashScreen()
        {
            Root.TransitionScreenFull();
        }

        private void ClosedSplashScreen()
        {
            Root.TransitionScreenClosed();
        }
    }
}

