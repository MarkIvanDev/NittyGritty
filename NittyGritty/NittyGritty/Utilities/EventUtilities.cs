using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Utilities
{
    public static class EventUtilities
    {

        /// <summary>Registers a weak event handler which is automatically deregistered after the subscriber 
        /// has been garbage collected (checked on each event call). </summary>
        /// <param name="subscriber"></param>
        /// <param name="deregister"></param>
        /// <param name="register"></param>
        /// <param name="converter">The converter: h => (o, e) => h(o, e)</param>
        /// <param name="handler"></param>
        public static TDelegate RegisterEvent<TSubscriber, TDelegate, TArgs>(
            TSubscriber subscriber,
            Action<TDelegate> register,
            Action<TDelegate> deregister,
            Func<EventHandler<TArgs>, TDelegate> converter,
            Action<TSubscriber, object, TArgs> handler)
            where TArgs : EventArgs
            where TDelegate : class
            where TSubscriber : class
        {
            var weakReference = new WeakReference(subscriber);
            TDelegate @delegate = null;
            @delegate = converter(
                (s, e) =>
                {
                    var strongReference = weakReference.Target as TSubscriber;
                    if (strongReference != null)
                        handler(strongReference, s, e);
                    else
                    {
                        deregister(@delegate);
                        @delegate = null;
                    }
                });
            register(@delegate);
            return @delegate;
        }

    }
}
