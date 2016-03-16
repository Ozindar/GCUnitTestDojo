using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Warehouse.Models;

namespace Warehouse.ConsoleRunner.Screens
{
    public class BuildingsScreen : ScreenBase
    {
        private IList<Building> _buildings;

        public BuildingsScreen(ISessionFactory sessionFactory, ScreenHandler screenHandler) : base(sessionFactory, screenHandler)
        {
            _buildings = Session.QueryOver<Building>().List();
        }

        public override string Name => "Buildings";

        public override void ScreenShow()
        {
            Console.WriteLine("**** Buildings ****");
            ShowAll();
        }

        private void ShowAll()
        {
            _buildings = Session.QueryOver<Building>().List();
            if (!_buildings.Any())
            {
                Console.WriteLine("No products");
                return;
            }
            int index = 0;
            foreach (var building in _buildings)
            {
                Console.WriteLine($"{index++}) {building.Name} (Airco: {building.Airco.AircoStatus})");
            }
        }

        public override void ScreenOptions()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("A) Add Building");
            Console.WriteLine("D) Details of Building");
        }

        public override bool HandleKey(char key)
        {
            switch (key)
            {
                case 'a':
                    AddBuilding();
                    break;
                case 'd':
                    DetailsOfBuilding();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void DetailsOfBuilding()
        {
            if (!_buildings.Any())
            {
                Console.WriteLine("Add some buildings first...");
            }
            else
            {
                Console.Clear();
                ShowAll();
                Console.WriteLine("Info of which building?");
                var buildingNumber = Console.ReadLine();
                int index = 0;

                while (!int.TryParse(buildingNumber, out index) || index >= _buildings.Count)
                {
                    Console.WriteLine("Type an integer please that is in range of the list of buildings:");
                    buildingNumber = Console.ReadLine();
                }

                ScreenHandler.ShowScreen(typeof (BuildingScreen), _buildings[index]);
            }
            
        }

        private void AddBuilding()
        {
            Console.Clear();
            Console.WriteLine("Type buildingname to add:");
            var buildingName = Console.ReadLine();

//            Console.WriteLine("Type weight in kg to add:");
//            var weight = Console.ReadLine();
//            var weightKg = 0m;
//            while (!decimal.TryParse(weight, out weightKg))
//            {
//                Console.WriteLine("Type a decimal please:");
//                weight = Console.ReadLine();
//            }

            var building = new Building { Name = buildingName };
            Session.SaveOrUpdate(building);

            Console.WriteLine($"{buildingName} saved.");
            Console.ReadLine();
            Show();
        }
    }
}