using Carneirofc.Scaffold.Domain.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Terminal.Gui;


namespace Carneirofc.Scaffold.TermGui.Views.DataSource
{
    public interface IItemRenderer<T>
    {
        public string Render(T item);
    }

    public class InstallerItemRenderer : IItemRenderer<Installer>
    {
        private const int _maxNameLength = 40;
        private const int _maxFileNameLength = 60;

        public string Render(Installer item)
        {
            return $"{item.Name,-_maxNameLength}";
        }
    }

    public class RendererListWrapper<T> : IListDataSource, IDisposable
    {
        private int _count;
        private BitArray _marks;
        private readonly IItemRenderer<T> _renderer;
        private readonly ObservableCollection<T> _source;

        public RendererListWrapper(ObservableCollection<T> source, IItemRenderer<T> renderer)
        {
            if (source is { })
            {
                _count = source.Count;
                _marks = new BitArray(_count);
                _source = source;
                _source.CollectionChanged += Source_CollectionChanged;
                Length = GetMaxLengthItem();
            }
            _source = source;
            _renderer = renderer;
        }

        void Source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!SuspendCollectionChangedEvent)
            {
                CheckAndResizeMarksIfRequired();
                CollectionChanged?.Invoke(sender, e);
            }
        }

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc/>
        public int Count => _source?.Count ?? 0;

        /// <inheritdoc/>
        public int Length { get; private set; }

        private bool _suspendCollectionChangedEvent;

        /// <inheritdoc />
        public bool SuspendCollectionChangedEvent
        {
            get => _suspendCollectionChangedEvent;
            set
            {
                _suspendCollectionChangedEvent = value;

                if (!_suspendCollectionChangedEvent)
                {
                    CheckAndResizeMarksIfRequired();
                }
            }
        }

        private void CheckAndResizeMarksIfRequired()
        {
            if (_source != null && _count != _source.Count)
            {
                _count = _source.Count;
                BitArray newMarks = new BitArray(_count);
                for (var i = 0; i < Math.Min(_marks.Length, newMarks.Length); i++)
                {
                    newMarks[i] = _marks[i];
                }
                _marks = newMarks;

                Length = GetMaxLengthItem();
            }
        }

        /// <inheritdoc/>
        public bool IsMarked(int item)
        {
            if (item >= 0 && item < _count)
            {
                return _marks[item];
            }

            return false;
        }

        /// <inheritdoc/>
        public void SetMark(int item, bool value)
        {
            if (item >= 0 && item < _count)
            {
                _marks[item] = value;
            }
        }

        /// <inheritdoc/>
        public IList ToList() { return _source; }

        /// <inheritdoc/>
        public int StartsWith(string search)
        {
            if (_source is null || _source?.Count == 0)
            {
                return -1;
            }

            for (var i = 0; i < _source.Count; i++)
            {
                object t = _source[i];

                if (t is string u)
                {
                    if (u.ToUpper().StartsWith(search.ToUpperInvariant()))
                    {
                        return i;
                    }
                } else if (t is string s)
                {
                    if (s.StartsWith(search, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private int GetMaxLengthItem()
        {
            if (_source is null || _source?.Count == 0)
            {
                return 0;
            }

            var maxLength = 0;

            for (var i = 0; i < _source.Count; i++)
            {
                object t = _source[i];
                int l;

                if (t is string u)
                {
                    l = u.GetColumns();
                } else if (t is string s)
                {
                    l = s.Length;
                } else
                {
                    l = t.ToString().Length;
                }

                if (l > maxLength)
                {
                    maxLength = l;
                }
            }

            return maxLength;
        }

        private void RenderUstr(ConsoleDriver driver, string ustr, int col, int line, int width, int start = 0)
        {
            string str = start > ustr.GetColumns() ? string.Empty : ustr.Substring(Math.Min(start, ustr.ToRunes().Length - 1));
            string u = TextFormatter.ClipAndJustify(str, width, Alignment.Start);
            driver.AddStr(u);
            width -= u.GetColumns();

            while (width-- > 0)
            {
                driver.AddRune((Rune)' ');
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_source is { })
            {
                _source.CollectionChanged -= Source_CollectionChanged;
            }
        }

        // <inheritdoc />
        public void Render(ListView container, ConsoleDriver driver, bool marked, int item, int col, int line, int width, int start = 0)
        {
            container.Move(Math.Max(col - start, 0), line);

            if (_source is { })
            {
                var t = _source[item];
                if (t is null)
                {
                    RenderUstr(driver, "", col, line, width);
                } else
                {
                    var s = _renderer.Render(t);
                    RenderUstr(driver, s, col, line, width, start);
                }
            }
        }

    }

   }
