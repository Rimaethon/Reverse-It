using System;
using System.Collections.Generic;
using System.Linq;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{
    public class EventManager : PersistentSingleton<EventManager>
    {
        #region Fields And Properties
        private readonly Dictionary<GameEvents, List<Delegate>> _eventHandlers = new();
        [SerializeField] private bool showEventNames;
        #endregion

        #region Unity Methods

        [SerializeField] private List<string> eventNames = new();

        private void Start()
        {
            if (!showEventNames) return;
            foreach (var events in _eventHandlers.Keys)
            {
                eventNames.Add(events.ToString());
                foreach (var value in _eventHandlers[events].ToList())
                {
                    Type type = value.Method.DeclaringType;
                    if (type == null) continue;
                    string className = type.Name;
                    eventNames.Add(className+ "." + value.Method.Name);
                }
            }
        }

        protected override void OnApplicationQuit()
        {
            _eventHandlers.Clear();
            base.OnApplicationQuit();
        }

        #endregion

        #region Event Handlers

        public void AddHandler(GameEvents gameEvent, Action handler)
        {
            if (!_eventHandlers.ContainsKey(gameEvent)) _eventHandlers[gameEvent] = new List<Delegate>();

            _eventHandlers[gameEvent].Add(handler);
        }

        public void AddHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (!_eventHandlers.ContainsKey(gameEvent)) _eventHandlers[gameEvent] = new List<Delegate>();
            _eventHandlers[gameEvent].Add(handler);
        }

        public void RemoveHandler(GameEvents gameEvent, Action handler)
        {
            if (_eventHandlers.TryGetValue(gameEvent, out var handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _eventHandlers.Remove(gameEvent);
                }
            }
        }

        public void RemoveHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (_eventHandlers.TryGetValue(gameEvent, out var handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _eventHandlers.Remove(gameEvent);
                }
            }
        }

        #endregion

        #region Event Broadcasting

        public void Broadcast(GameEvents gameEvents)
        {
            ProcessEvent(gameEvents);
        }

        public void Broadcast<T>(GameEvents gameEvent, T arg)
        {
            ProcessEvent(gameEvent, arg);
        }

        private void ProcessEvent(GameEvents gameEvents, params object[] args)
        {
            if (_eventHandlers.TryGetValue(gameEvents, out var eventHandler))
                foreach (var handler in eventHandler)
                {
                    handler.DynamicInvoke(args);
                }
        }

        #endregion
    }
}
