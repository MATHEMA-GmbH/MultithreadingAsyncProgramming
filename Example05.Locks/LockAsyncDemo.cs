namespace Example05.Locks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LockAsyncDemo
    {
        private readonly AsyncLock asyncLock = new AsyncLock();

        public async Task StartAsync()
        {
            Task[] tasks = new Task[5];

            for (int i = 0; i < 5; i++)
            {
                tasks[i] = CalculateAsync(i);
            }

            // Start each task
            await Task.WhenAll(tasks);
        }

        public async Task CalculateAsync(int threadNumber)
        {
            using (await asyncLock.LockAsync())
            {
                Console.Write("Working Thread: {0} is Executing", threadNumber);

                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(new Random().Next(5));
                    Console.Write(" {0},", i);
                }

                Console.WriteLine();
            }
        }
    }

    public class AsyncLock
    {
        private readonly Queue<TaskCompletionSource<bool>> waitingTasks = new Queue<TaskCompletionSource<bool>>();
        private bool isLockAcquired = true;

        public async Task<IDisposable> LockAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            lock (waitingTasks)
            {
                if (isLockAcquired)
                {
                    isLockAcquired = false;
                    return new Releaser(this);
                }

                waitingTasks.Enqueue(tcs);
            }

            await tcs.Task;
            return new Releaser(this);
        }

        private void ReleaseLock()
        {
            TaskCompletionSource<bool> nextTask = null;

            lock (waitingTasks)
            {
                if (waitingTasks.Count > 0)
                    nextTask = waitingTasks.Dequeue();
                else
                    isLockAcquired = true;
            }

            nextTask?.SetResult(true);
        }

        private struct Releaser : IDisposable
        {
            private readonly AsyncLock asyncLock;

            public Releaser(AsyncLock asyncLock)
            {
                this.asyncLock = asyncLock;
            }

            public void Dispose()
            {
                asyncLock.ReleaseLock();
            }
        }
    }
}