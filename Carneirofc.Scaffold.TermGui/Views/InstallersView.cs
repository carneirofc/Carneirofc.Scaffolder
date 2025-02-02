
//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v2.0.0.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------

using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

using Terminal.Gui;

using Carneirofc.Scaffold.TermGui.ViewModel;
using Carneirofc.Scaffold.TermGui.Views.DataSource;
using Carneirofc.Scaffold.Domain.Models;

namespace Carneirofc.Scaffold.TermGui.Views
{

    public partial class InstallersView : IViewFor<InstallersViewModel>
    {

        public InstallersView(InstallersViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeView();
        }

        private void InitializeView()
        {
            InitializeComponent();

            _pathTextField.Events()
                .TextChanged
                .Select(_ => _pathTextField.Text)
                .DistinctUntilChanged()
                .BindTo(ViewModel, x => x.Path);

            _filesListView.KeyBindings.Remove(Key.Space);
            _filesListView.Visible = true;
            _filesListView.AllowsMultipleSelection = false;
            _filesListView.AllowsMarking = true;
            _filesListView.Source = new RendererListWrapper<Installer>(ViewModel.Installers, new InstallerItemRenderer());

            var menuBarItems = new MenuBarItem[]
            {
                new MenuBarItem("_File", new MenuItem[]
                {
                    new MenuItem("Open", "", () => { }),
                    new MenuItem("Close", "", () => { }),
                }),
                new MenuBarItem("Edit", new MenuItem[]
                {
                    new MenuItem("Copy", "", () => { }),
                    new MenuItem("Cut", "", () => { }),
                    new MenuItem("Paste", "", () => { }),
                }),
            };
            _menuBar.Menus = menuBarItems;
        }
        public InstallersViewModel? ViewModel { get; set; }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (InstallersViewModel)value; }
    }
}
