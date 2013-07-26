using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace WpfApplication6
{
    class WPFListener : Listener
    {
        public event EventHandler<LeapFrameReadyEventArgs> LeapFrameReady;

        /// <summary>
        /// リスナー初期化
        /// </summary>
        /// <param name="ctr"></param>
        public override void OnInit(Controller ctr)
        {

        }

        /// <summary>
        /// 接続時
        /// </summary>
        /// <param name="ctr"></param>
        public override void OnConnect(Controller ctr)
        {
            if (!ctr.IsConnected)
            {
                Console.WriteLine("Leap is not connected");
            }
            else
            {
                Console.WriteLine("Leap is connected");
            }
        }

        /// <summary>
        /// 取り外し時
        /// </summary>
        /// <param name="ctr"></param>
        public override void OnDisconnect(Controller ctr)
        {
            Console.WriteLine("Leap was disconnected!");
        }

        /// <summary>
        /// 単位フレーム
        /// </summary>
        /// <param name="ctr">Leap</param>
        public override void OnFrame(Controller ctr)
        {
            Frame frame = ctr.Frame();

            if (LeapFrameReady != null)
            {
                LeapFrameReady(this, new LeapFrameReadyEventArgs(frame));
            }
        }

        /// <summary>
        /// コントローラからリスナが削除・外された時
        /// </summary>
        /// <param name="ctr"></param>
        public override void OnExit(Controller ctr)
        {
            
        }
    }

    class LeapFrameReadyEventArgs : EventArgs
    {
        public Frame Frame { get; set; }

        public LeapFrameReadyEventArgs(Frame frame)
        {
            Frame = frame;
        }
    }
}
