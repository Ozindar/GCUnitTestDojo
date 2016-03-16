using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Warehouse.Models;

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
            Console.WriteLine("1) Turn Airco ON");
            Console.WriteLine("2) Turn Airco OFF");
        }

        public override bool HandleKey(char key)
        {
            switch (key)
            {
                case '1':
                    //AddBuilding();
                    break;
                case '2':
                    //DetailsOfBuilding();
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}