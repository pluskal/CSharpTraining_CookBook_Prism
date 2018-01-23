using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Prism.Logging;

namespace CookBook.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        /// <summary>
        /// http://www.engineerspock.com/2016/03/15/global-exceptions-handling-in-wpf/
        /// </summary>
        public App()
        {
            this.Dispatcher.UnhandledException += this.DispatcherOnUnhandledException;
            Application.Current.DispatcherUnhandledException +=CurrentOnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += this.TaskSchedulerOnUnobservedTaskException;

        }

        private void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            this._bootstrapper?.BoostrapperLogger?.Log(dispatcherUnhandledExceptionEventArgs.Exception.ToString(),
                Category.Exception, Priority.High);
            dispatcherUnhandledExceptionEventArgs.Handled = true;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            this._bootstrapper?.BoostrapperLogger?.Log(unhandledExceptionEventArgs.ExceptionObject.ToString(),Category.Exception, Priority.High);
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            this._bootstrapper?.BoostrapperLogger?.Log(unobservedTaskExceptionEventArgs.Exception.ToString(),Category.Exception,Priority.High);
            unobservedTaskExceptionEventArgs.SetObserved();
        }
        
        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            this._bootstrapper?.BoostrapperLogger?.Log(dispatcherUnhandledExceptionEventArgs.Exception.ToString(), Category.Exception, Priority.High);
            dispatcherUnhandledExceptionEventArgs.Handled = true;
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this._bootstrapper = new Bootstrapper();
            this._bootstrapper.Run();
        }
    }
}
