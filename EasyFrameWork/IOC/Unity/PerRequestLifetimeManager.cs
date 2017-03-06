/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Easy.IOC.Unity
{

    /// <summary>
    /// Defines a <see cref="LifetimeManager"/> which returns the same object for a web request.
    /// </summary>
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private readonly string _key = typeof(PerRequestLifetimeManager).FullName;
        IDictionary<PerRequestLifetimeManager, object> GetPerRequestLifetimeManagers()
        {
            var valueProvider = ServiceLocator.Current.GetInstance<IHttpItemsValueProvider>();
            if (valueProvider == null) return null;
            IDictionary backingStore = valueProvider.Items;
            if (backingStore == null) return null;
            IDictionary<PerRequestLifetimeManager, object> instances;

            if (backingStore.Contains(_key))
            {
                instances = backingStore[_key] as IDictionary<PerRequestLifetimeManager, object>;
            }
            else
            {
                lock (backingStore)
                {
                    instances = backingStore.Contains(_key) ?
                                backingStore[_key] as IDictionary<PerRequestLifetimeManager, object> :
                                new Dictionary<PerRequestLifetimeManager, object>();

                    if (!backingStore.Contains(_key))
                    {
                        backingStore.Add(_key, instances);
                    }
                }
            }

            return instances;
        }
        public override object GetValue()
        {
            IDictionary<PerRequestLifetimeManager, object> lifetimeManagers = GetPerRequestLifetimeManagers();
            if (lifetimeManagers == null) return null;
            object value;

            lifetimeManagers.TryGetValue(this, out value);

            return value;
        }

        public override void SetValue(object newValue)
        {
            if (newValue == null)
            {
                RemoveValue();
                return;
            }

            IDictionary<PerRequestLifetimeManager, object> lifetimeManagers = GetPerRequestLifetimeManagers();
            if (lifetimeManagers == null) return;

            object value;

            if (lifetimeManagers.TryGetValue(this, out value))
            {
                if ((value != null) && ReferenceEquals(value, newValue))
                {
                    return;
                }

                DisposeValue(value);
            }

            lifetimeManagers[this] = newValue;
        }

        public override void RemoveValue()
        {
            IDictionary<PerRequestLifetimeManager, object> lifetimeManagers = GetPerRequestLifetimeManagers();
            if (lifetimeManagers == null) return;
            object value;

            if (!lifetimeManagers.TryGetValue(this, out value))
            {
                return;
            }

            DisposeValue(value);
            lifetimeManagers.Remove(this);
        }

        private void DisposeValue(object value)
        {
            IDisposable disposable = value as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}