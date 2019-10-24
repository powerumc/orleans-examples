using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans.Runtime;

namespace HelloObservers
{
    public class GrainObserverManager<T> : IEnumerable<T> where T : IAddressable
    {
        private readonly IDictionary<T, DateTime> _observers = new ConcurrentDictionary<T, DateTime>();

        public GrainObserverManager()
        {
            GetDateTime = () => DateTime.UtcNow;
        }

        public Func<DateTime> GetDateTime { get; set; }
        public TimeSpan ExpirationDuration { get; set; }
        public int Count => _observers.Count;

        public void Clear()
        {
            _observers.Clear();
        }

        public bool IsSubscribed(T observer)
        {
            return _observers.ContainsKey(observer);
        }

        public void Subscribe(T observer)
        {
            _observers[observer] = GetDateTime();
        }

        public void Unsubscribe(T observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(Func<T, Task> notification, Func<T, bool> predicate = null)
        {
            var now = GetDateTime();
            var defunct = default(List<T>);
            foreach (var observer in _observers)
            {
                if (ExpirationDuration != TimeSpan.Zero && observer.Value + ExpirationDuration < now)
                {
                    defunct ??= new List<T>();
                    defunct.Add(observer.Key);
                    continue;
                }

                if (predicate != null && !predicate(observer.Key))
                {
                    continue;
                }

                try
                {
                    await notification(observer.Key);
                }
                catch (Exception)
                {
                    defunct ??= new List<T>();
                    defunct.Add(observer.Key);
                }
            }

            if (defunct != default(List<T>))
            {
                foreach (var observer in defunct)
                {
                    _observers.Remove(observer);
                }
            }
        }

        public void Notify(Action<T> notification, Func<T, bool> predicate = null)
        {
            var now = GetDateTime();
            var defunct = default(List<T>);
            foreach (var observer in _observers)
            {
                if (ExpirationDuration != TimeSpan.Zero && observer.Value + ExpirationDuration < now)
                {
                    defunct ??= new List<T>();
                    defunct.Add(observer.Key);
                    continue;
                }

                if (predicate != null && !predicate(observer.Key))
                {
                    continue;
                }

                try
                {
                    notification(observer.Key);
                }
                catch (Exception)
                {
                    defunct ??= new List<T>();
                    defunct.Add(observer.Key);
                }
            }

            if (defunct != default(List<T>))
            {
                foreach (var observer in defunct)
                {
                    _observers.Remove(observer);
                }
            }
        }

        public void ClearExpired()
        {
            var now = GetDateTime();
            var defunct = default(List<T>);
            foreach (var observer in _observers)
            {
                if (observer.Value + ExpirationDuration < now)
                {
                    defunct ??= new List<T>();
                    defunct.Add(observer.Key);
                }
            }

            if (defunct != default(List<T>))
            {
                foreach (var observer in defunct)
                {
                    _observers.Remove(observer);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _observers.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _observers.Keys.GetEnumerator();
        }
    }
}
