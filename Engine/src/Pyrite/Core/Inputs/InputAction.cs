using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.Core.Inputs
{
    public class InputAction
    {
        private List<Action<CallbackContext>> _listeners = [];

        public bool Consumed { get; protected set; } = false;
        public void Consume() => Consumed = true;
        public void Free() => Consumed = false;

        public void Register(Action<CallbackContext> action)
        {
            _listeners.Add(action);
        }

        public bool Unregister(Action<CallbackContext> action)
        {
            return _listeners.Remove(action);
        }

        public void Invoke(ICollection<ButtonBinding> bindings, InputState state)
        {
            CallbackContext context = new CallbackContext
            {
                Action = this,
                ValueType = typeof(bool)
            };

            foreach (var listener in _listeners)
            {
                if (!Consumed)
                    listener.Invoke(context);
            }
        }

        public readonly struct CallbackContext
        {
            public readonly InputAction Action { get; init; }
            public readonly Type ValueType { get; init; }
            public readonly dynamic? Value { get; init; }

            public readonly T? ReadValue<T>() where T : notnull
            {
                return Value;
            }

            public readonly object? ReadValue()
            {
                return Convert.ChangeType(Value, ValueType);
            }
        }
    }
}
