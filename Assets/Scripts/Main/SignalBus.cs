using System;
using System.Collections.Generic;

namespace Assets.Scripts.Main
{
    public static class SignalBus
    {
        // Define a generic delegate for signals without parameters
        public delegate void SignalHandler();

        // Define a generic delegate for signals with one parameter
        public delegate void SignalHandler<T1>(T1 param1);

        // Define a generic delegate for signals with two parameters
        public delegate void SignalHandler<T1, T2>(T1 param1, T2 param2);

        // Define a generic delegate for signals with three parameters
        public delegate void SignalHandler<T1, T2, T3>(T1 param1, T2 param2, T3 param3);

        // Dictionary to store events by name
        private static Dictionary<string, Delegate> events = new Dictionary<string, Delegate>();


        // Create or retrieve a generic event by name 
        private static SignalHandler GetEvent(string eventName)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = null; // Initialize as null
            }

            return events[eventName] as SignalHandler;
        }

        // Create or retrieve a generic event by name for one parameter
        private static SignalHandler<T1> GetEvent<T1>(string eventName)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = null; // Initialize as null
            }

            return events[eventName] as SignalHandler<T1>;
        }

        // Create or retrieve a generic event by name for two parameters
        private static SignalHandler<T1, T2> GetEvent<T1, T2>(string eventName)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = null; // Initialize as null
            }

            return events[eventName] as SignalHandler<T1, T2>;
        }

        // Create or retrieve a generic event by name for three parameters
        private static SignalHandler<T1, T2, T3> GetEvent<T1, T2, T3>(string eventName)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = null; // Initialize as null
            }

            return events[eventName] as SignalHandler<T1, T2, T3>;
        }

        // Broadcast a signal with one parameter
        public static void BroadcastSignal(string eventName)
        {
            var signalEvent = GetEvent(eventName);
            signalEvent?.DynamicInvoke();
        }


        // Broadcast a signal with one parameter
        public static void BroadcastSignal<T1>(string eventName, T1 param1)
        {
            var signalEvent = GetEvent<T1>(eventName);
            signalEvent?.DynamicInvoke(param1);
        }

        // Broadcast a signal with two parameters
        public static void BroadcastSignal<T1, T2>(string eventName, T1 param1, T2 param2)
        {
            var signalEvent = GetEvent<T1, T2>(eventName);
            signalEvent?.DynamicInvoke(param1, param2);
        }

        // Broadcast a signal with three parameters
        public static void BroadcastSignal<T1, T2, T3>(string eventName, T1 param1, T2 param2, T3 param3)
        {
            var signalEvent = GetEvent<T1, T2, T3>(eventName);
            signalEvent?.DynamicInvoke(param1, param2, param3);
        }

        // Subscribe to a signal with a callback without parameter
        public static void Subscribe(string eventName, SignalHandler listener)
        {
            var signalEvent = GetEvent(eventName);

            if (signalEvent == null)
            {
                signalEvent = listener; // Create a new event with the provided listener
                events[eventName] = signalEvent;
            }
            else
            {
                signalEvent = (SignalHandler)Delegate.Combine(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Subscribe to a signal with a callback for one parameter
        public static void Subscribe<T1>(string eventName, SignalHandler<T1> listener)
        {
            var signalEvent = GetEvent<T1>(eventName);

            if (signalEvent == null)
            {
                signalEvent = listener; // Create a new event with the provided listener
                events[eventName] = signalEvent;
            }
            else
            {
                signalEvent = (SignalHandler<T1>)Delegate.Combine(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Subscribe to a signal with a callback for two parameters
        public static void Subscribe<T1, T2>(string eventName, SignalHandler<T1, T2> listener)
        {
            var signalEvent = GetEvent<T1, T2>(eventName);

            if (signalEvent == null)
            {
                signalEvent = listener; // Create a new event with the provided listener
                events[eventName] = signalEvent;
            }
            else
            {
                signalEvent = (SignalHandler<T1, T2>)Delegate.Combine(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Subscribe to a signal with a callback for three parameters
        public static void Subscribe<T1, T2, T3>(string eventName, SignalHandler<T1, T2, T3> listener)
        {
            var signalEvent = GetEvent<T1, T2, T3>(eventName);

            if (signalEvent == null)
            {
                signalEvent = listener; // Create a new event with the provided listener
                events[eventName] = signalEvent;
            }
            else
            {
                signalEvent = (SignalHandler<T1, T2, T3>)Delegate.Combine(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Unsubscribe from a signal for without parameter
        public static void Unsubscribe(string eventName, SignalHandler listener)
        {
            var signalEvent = GetEvent(eventName);

            if (signalEvent != null)
            {
                signalEvent = (SignalHandler)Delegate.Remove(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Unsubscribe from a signal for one parameter
        public static void Unsubscribe<T1>(string eventName, SignalHandler<T1> listener)
        {
            var signalEvent = GetEvent<T1>(eventName);

            if (signalEvent != null)
            {
                signalEvent = (SignalHandler<T1>)Delegate.Remove(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Unsubscribe from a signal for two parameters
        public static void Unsubscribe<T1, T2>(string eventName, SignalHandler<T1, T2> listener)
        {
            var signalEvent = GetEvent<T1, T2>(eventName);

            if (signalEvent != null)
            {
                signalEvent = (SignalHandler<T1, T2>)Delegate.Remove(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }

        // Unsubscribe from a signal for three parameters
        public static void Unsubscribe<T1, T2, T3>(string eventName, SignalHandler<T1, T2, T3> listener)
        {
            var signalEvent = GetEvent<T1, T2, T3>(eventName);

            if (signalEvent != null)
            {
                signalEvent = (SignalHandler<T1, T2, T3>)Delegate.Remove(signalEvent, listener);
                events[eventName] = signalEvent;
            }
        }
    }
}
