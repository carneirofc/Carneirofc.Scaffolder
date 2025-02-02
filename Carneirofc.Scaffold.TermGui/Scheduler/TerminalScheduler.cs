using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Carneirofc.Scaffold.TermGui.Scheduler
{

    public class TerminalScheduler : LocalScheduler
    {
        public static readonly TerminalScheduler Default = new();
        private TerminalScheduler() { }

        public override IDisposable Schedule<TState>(
            TState state,
            TimeSpan dueTime,
            Func<IScheduler, TState, IDisposable> action
        )
        {
            IDisposable PostOnMainLoop()
            {
                var composite = new CompositeDisposable(2);
                var cancellation = new CancellationDisposable();

                Terminal.Gui.Application.Invoke(
                                    () =>
                                    {
                                        if (!cancellation.Token.IsCancellationRequested)
                                        {
                                            composite.Add(action(this, state));
                                        }
                                    }
                                   );
                composite.Add(cancellation);

                return composite;
            }

            IDisposable PostOnMainLoopAsTimeout()
            {
                var composite = new CompositeDisposable(2);

                object timeout = Terminal.Gui.Application.AddTimeout(
                                                         dueTime,
                                                         () =>
                                                         {
                                                             composite.Add(action(this, state));

                                                             return false;
                                                         }
                                                        );
                composite.Add(Disposable.Create(() => Terminal.Gui.Application.RemoveTimeout(timeout)));

                return composite;
            }

            return dueTime == TimeSpan.Zero
                       ? PostOnMainLoop()
                       : PostOnMainLoopAsTimeout();
        }
    }
}
