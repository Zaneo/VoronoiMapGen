using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using VoronoiMapGen.CustomEvents;
using VoronoiMapGen.Generation;

namespace VoronoiMapGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public event TaskChangingHandler TaskChanging;
        private readonly BackgroundWorker _worker;
        private CancellationTokenSource _cancelTask;

        private const string GeneratorIdleText = "Generate";
        private const string GeneratorActiveText = "Cancel";
        
        public MainWindow(){
            InitializeComponent();
            Thread.CurrentThread.Name = "UI";
            GenerateButton.Content = GeneratorIdleText;
            _worker = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            _worker.DoWork+= new DoWorkEventHandler(Generate);
            _worker.ProgressChanged += (sender, args) => CurrentTaskProgress.Value = args.ProgressPercentage;
            TaskChanging += UpdateCurrentTask;
            TaskChanging += UpdateGeneratorButton;
            _cancelTask = new CancellationTokenSource();
        }

        private void GenerateButtonClick(object sender, RoutedEventArgs e) {
            VoronoiGeneration test = new VoronoiGeneration();
            test.BeginTask(ref MapThumbnail);
            
            if (_worker.IsBusy) {
                _cancelTask.Cancel();
            }
            else {
                _worker.RunWorkerAsync();
            }
        }


        private void UpdateGeneratorButton(object sender, TaskChangingEventArgs e) {
            string title;
            switch (e.Reason) {
                case ChangeReason.NextStep:
                    return;
                case ChangeReason.Cancelled:
                case ChangeReason.Completed:
                    title = GeneratorIdleText;
                    break;
                case ChangeReason.Starting:
                    title = GeneratorActiveText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (GenerateButton.Dispatcher.CheckAccess()) {
                GenerateButton.Content = title;
            }
            else {
                GenerateButton.Dispatcher.Invoke(DispatcherPriority.DataBind, (Action<string>)(text => GenerateButton.Content = text), title);
            }
        }

        private void UpdateCurrentTask(object sender, TaskChangingEventArgs e){
            if (CurrentTask.Dispatcher.CheckAccess()) {
                CurrentTask.Text = e.NewTaskName;
            }
            else {
                CurrentTask.Dispatcher.Invoke(DispatcherPriority.DataBind, (Action<string>)(text => CurrentTask.Text = text), e.NewTaskName);
            }
        }

        private void Generate(object sender, DoWorkEventArgs e) {
            if (sender == null)
                throw new ArgumentNullException("sender", "Cannot start task on null BackgroundWorker.");

            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw == null)
                throw new InvalidCastException(string.Format("Could not convert from type: {0} to Backgroundworker", sender.GetType()));
            

            Thread.CurrentThread.Name = "Background";
            TaskChanging(this, new TaskChangingEventArgs(ChangeReason.Starting, "Starting"));
            bw.ReportProgress(0);
            Thread.Sleep(500);
            try {
                TaskChanging(this, new TaskChangingEventArgs(ChangeReason.NextStep, "Generating Voronoi Diagrams"));
                GenerateVoronoi(ref bw);
                TaskChanging(this, new TaskChangingEventArgs(ChangeReason.NextStep, "Generating Map"));
                GenerateMap(ref bw);
                TaskChanging(this, new TaskChangingEventArgs(ChangeReason.Completed, "Finished"));

            }
            catch (OperationCanceledException) {
                TaskChanging(this, new TaskChangingEventArgs(ChangeReason.Cancelled, "Cancelled"));
                _cancelTask = new CancellationTokenSource();
            }
        }

        private void GenerateVoronoi(ref BackgroundWorker bw) {
            var cancelToken = _cancelTask.Token;
            for (int i = 0; i < 100; i++) {
                cancelToken.ThrowIfCancellationRequested();
                bw.ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        private void GenerateMap(ref BackgroundWorker bw)
        {
            var cancelToken = _cancelTask.Token;
            for (int i = 0; i < 100; i++)
            {
                cancelToken.ThrowIfCancellationRequested();
                bw.ReportProgress(i);
                Thread.Sleep(100);
            }
        }
    }
}
