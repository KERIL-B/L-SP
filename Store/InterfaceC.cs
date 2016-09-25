using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Store
{
    class Buyer
    {
        public int timeOutQueue { get; set; }
        public int timeInQueue { get; set; }
        public int cBN { get; set; }
        public int inQueueN { get; set; }
    }

    class MyQueue
    {
        public Queue<Buyer> queue = new Queue<Buyer>();
        public int nCB { get; set; }

        public void Add(Buyer newBuyer, Canvas canvas1)
        {
            newBuyer.cBN = nCB;
            queue.Enqueue(newBuyer);

        }
    }

    class InterfaceC
    {
        Random rand = new Random();

        public List<MyQueue> list = new List<MyQueue>(nGlobal);
        public List<Buyer> buyersInShopingZoneList = new List<Buyer>();
        static int nGlobal;

        public int s { get; set; }
        public int nextBuyerTime { get; set; }

        public void StartMethod(int n, Canvas canvas1)
        {
            s = 0;
            DrawAllCB(n, canvas1);
            DrawShopZone(canvas1);
            GenerateNextBuyerTime();
            nGlobal = n;
            MyQueue tmpQ;
            for (int i = 0; i < nGlobal; i++)
            {
                tmpQ = new MyQueue();
                tmpQ.nCB = i;
                list.Add(tmpQ);
            }

        }

        public void TimeCheck(Canvas canvas1)
        {
            if (s == nextBuyerTime)
            {
                AddBuyerInShopingZone(canvas1);

                GenerateNextBuyerTime();
            }
            for (int i = 0; i < buyersInShopingZoneList.Count; i++)
            {
                if (buyersInShopingZoneList[i].timeOutQueue == s)
                {
                    SendInQueue(i, canvas1);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].queue.Count != 0)

                    if (list[i].queue.Peek().timeInQueue == s)
                    {
                        LeaveStore(list[i], canvas1);
                    }
            }

        }

        private void LeaveStore(MyQueue queue, Canvas canvas1)
        {
            queue.queue.Dequeue();
            if (queue.queue.Count != 0)
                queue.queue.Peek().timeInQueue = s + rand.Next(30) + 5;
            ClearFromQueue(queue, canvas1);
        }

        private void ClearFromQueue(MyQueue queue, Canvas canvas1)
        {
            Rectangle clearR = new Rectangle();
            int hM = queue.nCB * 75 + 20;
            int vM = 231 - 30 * queue.queue.Count;
            clearR.Fill = Brushes.White;
            clearR.StrokeThickness = 0;
            clearR.Height = 30;
            clearR.Width = 30;
            clearR.Margin = new System.Windows.Thickness(hM, vM, 0, 0);
            canvas1.Children.Add(clearR);


        }

        private void SendInQueue(int i, Canvas canvas1)
        {
            ChooseQueue(buyersInShopingZoneList[i], canvas1);
            DrawInQueue(buyersInShopingZoneList[i], canvas1);
            DeleteFromShopingZone(i, canvas1);
        }

        private void DrawInQueue(Buyer buyerObj, Canvas canvas1)
        {
            int hM = buyerObj.cBN * 75 + 23;
            int vM = -buyerObj.inQueueN * 30 + 234;
            Ellipse buyer = new Ellipse();
            buyer.Width = 24;
            buyer.Height = buyer.Width;
            buyer.Fill = Brushes.MediumAquamarine;
            buyer.Stroke = Brushes.SlateGray;
            buyer.StrokeThickness = 1;
            buyer.Margin = new System.Windows.Thickness(hM, vM, 0, 0);
            canvas1.Children.Add(buyer);
        }

        private void ChooseQueue(Buyer buyer, Canvas canvas1)
        {
            int min = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[min].queue.Count > list[i].queue.Count)
                    min = i;
            }

            buyer.cBN = min;
            buyer.inQueueN = list[min].queue.Count;
            if (list[min].queue.Count == 0)
                buyer.timeInQueue = s + rand.Next(30) + 5;
            list[min].Add(buyer, canvas1);
        }

        private void DeleteFromShopingZone(int k, Canvas canvas1)
        {
            Rectangle clearR = new Rectangle();
            clearR.Width = 435;
            clearR.Height = 47;
            clearR.Fill = Brushes.White;
            clearR.StrokeThickness = 0;
            canvas1.Children.Add(clearR);
            DrawShopZone(canvas1);

            for (int i = k; i < buyersInShopingZoneList.Count - 1; i++)
            {
                buyersInShopingZoneList[i] = buyersInShopingZoneList[i + 1];
            }

            buyersInShopingZoneList.RemoveAt(buyersInShopingZoneList.Count - 1);

            for (int i = 0; i < buyersInShopingZoneList.Count; i++)
            {
                DrawBuyerInShopingZone(i + 1, canvas1);
            }
        }

        private void AddBuyerInShopingZone(Canvas canvas1)
        {
            Buyer tmpB = new Buyer();
            tmpB.timeOutQueue = s + rand.Next(30) + 5;
            buyersInShopingZoneList.Add(tmpB);
            DrawBuyerInShopingZone(buyersInShopingZoneList.Count, canvas1);

        }

        private void DrawBuyerInShopingZone(int k, Canvas canvas1)
        {
            Ellipse buyer = new Ellipse();
            buyer.Width = 24;
            buyer.Height = buyer.Width;
            buyer.Fill = Brushes.MediumAquamarine;
            buyer.Stroke = Brushes.SlateGray;
            buyer.StrokeThickness = 1;
            buyer.Margin = new System.Windows.Thickness(k * 12 - 5, 12, 0, 0);
            canvas1.Children.Add(buyer);
        }

        private void GenerateNextBuyerTime()
        {
            nextBuyerTime = s + rand.Next(30) + 5;
        }

        public Boolean CheckNumberOfCahBoxes(TextBox inputTB, Canvas canvas1)
        {
            if (inputTB.Text != "")
            {
                try
                {
                    int n = Convert.ToInt32(inputTB.Text);
                    canvas1.Children.Clear();
                    StartMethod(n, canvas1);
                    inputTB.Text = n + "";
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
            }
            else return false;
        }
        private void DrawShopZone(Canvas canvas1)
        {
            Rectangle shopZone = new Rectangle();
            shopZone.Height = 44;
            shopZone.Width = 435;
            shopZone.Stroke = Brushes.Black;
            shopZone.StrokeThickness = 1;
            shopZone.Margin = new System.Windows.Thickness(1, 1, 0, 0);
            canvas1.Children.Add(shopZone);

            TextBlock tb = new TextBlock();
            tb.Height = 44;
            tb.Text = "Shoping Zone";
            tb.Foreground = Brushes.Silver;
            tb.FontSize = 25;
            tb.Margin = new System.Windows.Thickness(150, 5, 0, 0);
            canvas1.Children.Add(tb);
        }

        private void DrawAllCB(int n, Canvas canvas1)
        {
            const int d = 75;
            int betweenDistance = 10;

            for (int i = 0; i < n; i++)
            {
                DrawOneCB(betweenDistance, canvas1);
                betweenDistance += d;
            }

        }

        private void DrawOneCB(int n, Canvas canvas1)
        {
            Rectangle cb = new Rectangle();
            cb.Height = 25;
            cb.Width = 50;
            cb.Stroke = Brushes.Black;
            cb.StrokeThickness = 1;
            cb.Fill = Brushes.Silver;
            cb.Margin = new System.Windows.Thickness(n, 264, 0, 0);
            canvas1.Children.Add(cb);
        }


    }
}
