namespace Warehouse.Screens
{
    using System.Collections.Generic;
    using System.Linq;
    using Handlers;
    using Models;
    using NHibernate;

    public class BuildingsScreen : CrudScreen<Building>
    {
        private IList<Building> _buildings;

        public BuildingsScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler) : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
            _buildings = Session.QueryOver<Building>().List();
        }

        public override string Name => "Buildings";

        protected override void ScreenOptions()
        {
            base.ScreenOptions();
            ResponseHandler.WriteLine("----------------------");
            ResponseHandler.WriteLine("D) Details of Building");
        }

        protected override bool HandleKey(char key)
        {
            if (base.HandleKey(key))
                return true;

            switch (key)
            {
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
                ResponseHandler.WriteLine("Add some buildings first...");
            }
            else
            {
                ResponseHandler.Clear();
                ResponseHandler.WriteLine("Info of which building?");
                List();

                try
                {
                    Building chosenBuilding = RequestHandler.RequestChoice<Building>(ResponseHandler, Session);
                    ScreenHandler.ShowScreen(typeof(BuildingScreen), true, chosenBuilding);
                }
                catch (NoChoiceException)
                {
                    ResponseHandler.Clear();
                    Show();
                }

            }
        }

        protected override void Add()
        {
            ResponseHandler.Clear();
            ResponseHandler.WriteLine("Type buildingname to add:");
            var buildingName = RequestHandler.ReadLine();

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
            ResponseHandler.Clear();
            ResponseHandler.WriteLine($"{buildingName} saved.");
            Show();
        }
    }
}