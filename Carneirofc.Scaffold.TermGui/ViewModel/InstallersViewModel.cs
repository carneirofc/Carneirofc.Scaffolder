using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Domain.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Carneirofc.Scaffold.TermGui.ViewModel
{
    [DataContract]
    public partial class InstallersViewModel : ReactiveObject
    {
        [IgnoreDataMember]
        private readonly IInstallerService _installerService;

        [DataMember]
        [Reactive]
        public ObservableCollection<Installer> Installers { get; } = new();

        [DataMember]
        [Reactive]
        private string _path = string.Empty;

        [DataMember]
        [Reactive]
        private string _filter = string.Empty;


        [ReactiveCommand]
        public void Clear(HandledEventArgs args) { }

        [ReactiveCommand]
        public void ListInstallers(EventArgs? args)
        {
            if (_installerService is null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(Path))
            {
                return;
            }
            var items = _installerService.List(_path, _filter).ToBlockingEnumerable().ToList();
            if (items is null)
            {
                return;
            }
            Installers.Clear();
            foreach (var item in items)
            {
                Installers.Add(item);
            }
        }

        public InstallersViewModel(IInstallerService installerService) : base()
        {
            _installerService = installerService;
            ClearCommand.Subscribe((x) =>
            {
                Installers.Clear();
            });

            // Then path changes, list installers
            this.WhenAnyValue(x => x.Path)
                .Subscribe(_ => ListInstallers(null));
        }

    }
}
