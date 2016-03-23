using System;
using FluentNHibernate.Utils;
using NHibernate;
using Warehouse.Models;
using Warehouse.Models.Enums;
using Warehouse.Models.Exceptions;

namespace Warehouse.ConsoleRunner.Screens
{
    public class BuildingScreen : ScreenBase
    {
        private Building _building;

        public BuildingScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler, Building building)
            : base(sessionFactory, screenHandler)
        {
            if (building == null)
            {
                throw new ArgumentNullException(nameof(building));
            }

            _building = building;
        }

        public override string Name => "Building info";

        public override void ScreenShow()
        {
            Console.WriteLine("**** Building info ****");
            ShowDetails();
        }

        private void ShowDetails()
        {
            Console.Clear();
            Console.WriteLine($"{_building.Name} (Airco: {_building.Airco.AircoStatus})");
        }


        public override void ScreenOptions()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("N) Turn Airco ON");
            Console.WriteLine("F) Turn Airco OFF");
        }

        public override bool HandleKey(char key)
        {
            switch (key.ToLowerInvariantString())
            {
                case "n":
                    SetAircoStatus(AircoStatus.On);
                    break;
                case "f":
                    SetAircoStatus(AircoStatus.Off);
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void SetAircoStatus(AircoStatus aircoStatus)
        {
            try
            {
                _building.SetAircoStatus(aircoStatus);

                using (var t = Session.BeginTransaction())
                {
                    try
                    {

                    Session.Update(_building.Airco);
                    t.Commit();
                    }
                    catch
                    {
                        t.Rollback();
                    }
                }

                //OF
                //Session.Update(_building.Airco);
                //Session.Flush();

                Console.Out.WriteLine($"Airco is turned {aircoStatus}");
            }
            catch (AircoTemperatureTooHighException)
            {
                Console.Out.WriteLine("The outside temparature is too high: don't turn off the airco!");
            }
            catch (AircoTemperatureTooLowException)
            {
                Console.Out.WriteLine("The outside temparature is too low: no need to turn on the arico.");
            }
            finally
            {
                Console.ReadLine();
                Show();
            }
        }
    }
}