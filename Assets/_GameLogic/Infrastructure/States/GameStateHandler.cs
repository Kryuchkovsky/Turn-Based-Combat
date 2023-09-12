using System;
using System.Collections.Generic;
using _GameLogic.Gameplay;
using _GameLogic.Gameplay.Battlefield;
using _GameLogic.Gameplay.Units;
using _GameLogic.UI;
using UnityEngine;

namespace _GameLogic.Infrastructure.States
{
    public class GameStateHandler
    {
        private Dictionary<Type, BaseGameState> _states;
        private BaseGameState _currentState;

        public ArenaGenerator ArenaGenerator { get; }
        public UnitCreator UnitCreator { get; }
        public FightHandler FightHandler { get; }
        public UIContainer UIContainer { get; }
        public Transform ContentTransform { get; }

        public GameStateHandler(UIContainer uiContainer, Transform contentTransform)
        {
            _states = new Dictionary<Type, BaseGameState>
            {
                { typeof(InitializationState), new InitializationState(this) },
                { typeof(FightState), new FightState(this) },
                { typeof(RestartingState), new RestartingState(this) },
            };
            UIContainer = uiContainer;
            ContentTransform = contentTransform;
            ArenaGenerator = new ArenaGenerator(contentTransform);
            UnitCreator = new UnitCreator(contentTransform);
            FightHandler = new FightHandler();
        }

        public void ChangeState<T>() where T : BaseGameState
        {
            var type = typeof(T);

            if (_states.ContainsKey(type))
            {
                _currentState?.OnExit();
                _currentState = _states[type];
                _currentState?.OnEnter();
            }
            else throw new Exception($"The state: {type} doesn't exist!");
        }

        public void ExecuteState()
        {
            _currentState?.OnExecute();
        }
    }
}