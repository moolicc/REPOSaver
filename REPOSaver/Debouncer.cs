using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    static class Debouncer<TKey>
    {
        private static CancellationTokenSource _debounceCancel;
        private static int _counter;
        private static object _debounceLock;

        static Debouncer()
        {
            _debounceLock = new object();
        }

        public static int GetCount()
        {
            return Interlocked.Increment(ref _counter);
        }

        public static async Task<bool> DebounceAsync(int timeout = 1000)
        {
            lock (_debounceLock)
            {
                _debounceCancel?.Cancel();
                _debounceCancel?.Dispose();
                _debounceCancel = new CancellationTokenSource();
            }

            try
            {
                await Task.Delay(timeout, _debounceCancel.Token);
            }
            catch (TaskCanceledException)
            {
                return false;
            }

            return true;
        }

        public static Task<bool> ThreadedDebounce(int timeout = 1000)
        {
            Task<bool> task = Task.Run(async () =>
            {
                lock (_debounceLock)
                {
                    _debounceCancel?.Cancel();
                    _debounceCancel?.Dispose();
                    _debounceCancel = new CancellationTokenSource();
                }

                try
                {
                    await Task.Delay(timeout, _debounceCancel.Token);
                }
                catch (TaskCanceledException)
                {
                    return false;
                }

                return true;
            });

            return task;
        }
    }
}
