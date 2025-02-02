// See https://aka.ms/new-console-template for more information
using Carneirofc.Scaffold.Application.Contracts.Services;
using Carneirofc.Scaffold.Application.Services;
using Carneirofc.Scaffold.TermGui.Scheduler;
using Carneirofc.Scaffold.TermGui.ViewModel;
using Carneirofc.Scaffold.TermGui.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Reactive.Concurrency;
using Terminal.Gui;


var services = new ServiceCollection();
services.AddSingleton<IInstallerService, InstallerService>();
services.AddHttpClient();

services.AddSingleton<InstallersViewModel>();
services.AddSingleton<InstallersView>();

var provider = services.BuildServiceProvider();


Application.Init();

//ConfigurationManager.Themes.Theme = "Dark";
ConfigurationManager.Apply();

RxApp.MainThreadScheduler = TerminalScheduler.Default;
RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

//IInstallerService installerService = new InstallerService();
//var vm = new InstallersViewModel(installerService);
//var view = new InstallersView(vm);

Application.Run(provider.GetRequiredService<InstallersView>());

Application.Top?.Dispose();

Application.Shutdown();
