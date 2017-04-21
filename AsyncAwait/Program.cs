using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExecution
{
    class Program
    {
        static void Main(string[] args)
        {

            DoTaskParallelWay();
            //DoSerialWay();

            Console.ReadLine();

        }

        private static void DoTaskParallelWay()
        {
            StopWatch watch = StopWatch.StartNew();

            Task<int> hotelTask = Task.Factory.StartNew<int>(HotelBooking);
            Task<int> carTask = Task.Factory.StartNew<int>(CarBooking);
            Task<int> flightTask = Task.Factory.StartNew<int>(FlightBooking);
            Task followupCarTask = carTask.ContinueWith(
                prevTask => Console.WriteLine(string.Format("Setting up car {0}", prevTask.Result))
                );

            Task.WaitAll(hotelTask, carTask, flightTask);

            Console.WriteLine(String.Format("Car ID={0} ", carTask.Result));
            Console.WriteLine(String.Format("Parallel Time elapsed {0} seconds ", watch.ElapsedMilliseconds / 1000.0));
            
        }

        private static void DoSerialWay()
        {
            StopWatch watch = StopWatch.StartNew();

            int hotelID = HotelBooking();
            int carID = CarBooking();
            int flightID = FlightBooking();

            Console.WriteLine(String.Format("Serial Time elapsed {0} seconds ", watch.ElapsedMilliseconds / 1000.0 ));
        }

        private static int FlightBooking()
        {
            Console.WriteLine("Flight Starting");
            List<Int64> list = new List<Int64>();
            for (int i = 0; i < 100000; i++)
            {
                Int64 a = 1 + i;
                a = a * 2;
                list.Add(a);
                list.Sum();
            }
            Console.WriteLine("Flight Doing Execution");
            Thread.Sleep(3000);
            Console.WriteLine("Flight Ended");

            return new Random().Next(50);
        }

        private static int CarBooking()
        {
            Console.WriteLine("Car Starting");
            List<Int64> list = new List<Int64>();
            for (int i = 0; i < 1000000; i++)
            {
                Int64 a = 1 + i;
                a = a * 2;
                list.Add(a);
                list.Sort();
            }
            Console.WriteLine("Car Doing Execution");
            Thread.Sleep(2000);
            Console.WriteLine("Car Ended");

            return new Random().Next(50);
        }

        private static int HotelBooking()
        {
            Console.WriteLine("Hotel Starting");
            Thread.Sleep(5000);
            Console.WriteLine("Hotel Ended");

            return new Random().Next(50);
        }
    }
}
