using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace King_Minors_Fan_Curve {

    class FanCurve {
        private List<Point> curveList; //List that stores all points on fan curve.
        private Point pointBelowTemp = new Point(0,0);
        private Point pointAboveTemp = new Point(100,100);
        private int whatSpeedShouldISet;

        bool addAnother = false; //used for initializeing fan curve.

        public FanCurve () { //Constructor
            this.curveList = new List<Point>();
        }

        public void InitializeFanCurve () {
            Console.WriteLine("[PLEASE CREATE THE POINTS FOR THE FAN CURVE0]");
            do {
                Console.Write("What is the temp value: "); //Asks for temp value
                int pointTempValue = int.Parse(Console.ReadLine());

                Console.Write("What is the speed percentage for the point: "); //Asks for speed perecentage value.
                int pointSpeedPercentage = int.Parse(Console.ReadLine());

                curveList.Add(new Point(pointTempValue, pointSpeedPercentage));

                Console.Write("Add another point? (type YES): ");
                string awnser = Console.ReadLine().ToLower();
                if (awnser == "yes") {
                    addAnother = true;
                }
                else {
                    addAnother = false;
                }

            } while (addAnother == true);

            Console.WriteLine("Fan Curve Intialized");
            SortList();
            DisplayAllPoints();
        }

        private void SortList () {
            curveList.Sort((p1,p2) => p1.GetTemp().CompareTo(p2.GetTemp()));
        }

        private void GetPointBelowAndAbove(int currentTemp) {
            foreach (Point point in curveList) {

                //Point Below Temp
                if(point.GetTemp() <= currentTemp && pointBelowTemp.GetTemp() < point.GetTemp()) {
                    pointBelowTemp = point;
                }

                //Point Above Temp
                if(point.GetTemp() >= currentTemp && pointAboveTemp.GetTemp() > point.GetTemp()) {
                    pointAboveTemp = point;
                }
            }
            Console.WriteLine($"Point Below: Temp={pointBelowTemp.GetTemp()}, Speed={pointBelowTemp.GetSpeedPercentage()}");
            Console.WriteLine($"Point Above: Temp={pointAboveTemp.GetTemp()}, Speed={pointAboveTemp.GetSpeedPercentage()}");
        }

        private double CalculateSlope () {
            double rise = pointAboveTemp.GetSpeedPercentage() - pointBelowTemp.GetSpeedPercentage();
            double run = pointAboveTemp.GetTemp() - pointBelowTemp.GetTemp();
            if (run == 0) {
                return 100;
            }
            return rise / run;
        }

        private double CalculateYIntercept () {
            double yIntercept = pointBelowTemp.GetSpeedPercentage() - CalculateSlope() * pointBelowTemp.GetTemp();
            return yIntercept;
        }

        public int CalculateFanSpeed(int temp) {
            GetPointBelowAndAbove(temp);
            double slope = CalculateSlope();
            double yIntercept = CalculateYIntercept();

            whatSpeedShouldISet = (int)Math.Floor(slope * temp + yIntercept);
            Console.WriteLine($"Slope={slope}, Temp={temp}, Y-Intercept={yIntercept}");
            return whatSpeedShouldISet;
        }

        public void DisplayAllPoints() {
            foreach(Point point in curveList) {
                Console.WriteLine($"Temp={point.GetTemp()}, Speed={point.GetSpeedPercentage()}.");
            }
        }
        
    }

    class Point {
        private int temp;
        private int speedPercentage;

        public Point(int temp, int speedPercentage)
        {
            this.temp = temp;
            this.speedPercentage = speedPercentage;
        }

        public int GetTemp() {
            return this.temp;
        }

        public int GetSpeedPercentage() {
            return this.speedPercentage;
        }

        public void ChangeTemp(int newTemp) {
            this.temp = newTemp;
        }

        public void ChangeSpeedPercentage(int newSpeedPercentage) {
            this.speedPercentage = newSpeedPercentage;
        }
    }
}