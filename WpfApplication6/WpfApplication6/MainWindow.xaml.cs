using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

using Leap;

namespace WpfApplication6
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller ctr;
        WPFListener lst;
        Ellipse elp;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //センサーとリスナの定義
                lst = new WPFListener();

                ctr = new Controller(lst);
                ctr.EnableGesture(Gesture.GestureType.TYPECIRCLE);
                //ctr.AddListener(lst);
                lst.LeapFrameReady += lst_LeapFrameReady;

                elp = new Ellipse() { Width = 20, Height = 20, Stroke = Brushes.Red, StrokeThickness = 3, Fill = Brushes.Red };
                cvs.Children.Add(elp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK);
                Application.Current.Shutdown();
            }
        }

        void lst_LeapFrameReady(object sender, LeapFrameReadyEventArgs e)
        {
            if (!e.Frame.Fingers.IsEmpty)
            {
                Finger f = e.Frame.Fingers[0];

                //メインスレッド処理はココ
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    elp.Width = Math.Abs(160 - f.TipPosition.z);
                    elp.Height = Math.Abs(160 - f.TipPosition.z);

                    Canvas.SetLeft(elp, (this.ActualWidth / 2) + f.TipPosition.x);
                    Canvas.SetTop(elp, (this.ActualHeight / 2) - f.TipPosition.y);

                    LblX.Content = f.TipPosition.x.ToString();
                    LblY.Content = f.TipPosition.y.ToString();
                    LblZ.Content = f.TipPosition.z.ToString();
                }));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ctr.RemoveListener(lst);
            ctr.Dispose();
            lst.Dispose();
        }
    }
}
