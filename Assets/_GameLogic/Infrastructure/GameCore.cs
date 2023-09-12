using _GameLogic.Infrastructure.States;
using _GameLogic.UI;
using UnityEngine;

namespace _GameLogic.Infrastructure
{
    [DisallowMultipleComponent]
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private UIContainer _uiContainer;
        [SerializeField] private Transform _contentTransform;

        private GameStateHandler _gameStateHandler;

        private void Start()
        {
            _gameStateHandler = new GameStateHandler(_uiContainer, _contentTransform);
            _gameStateHandler.ChangeState<InitializationState>();
        }

        private void Update()
        {
            _gameStateHandler?.ExecuteState();
        }
    }
}