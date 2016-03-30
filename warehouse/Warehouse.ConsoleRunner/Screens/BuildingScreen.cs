﻿using System;
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

        public BuildingScreen(ISessionFactory sessionFactory, IScreenHandler screenHandler, IRequestHandler requestHandler, IResponseHandler responseHandler,  Building building)
            : base(sessionFactory, screenHandler, requestHandler, responseHandler)
        {
            if (building == null)
            {
                throw new ArgumentNullException(nameof(building));
            }

            _building = building;
        }

        public override string Name => "Building info";

        protected override void ScreenShow()
        {
            ResponseHandler.WriteLine("**** Building info ****");
            ShowDetails();
        }

        private void ShowDetails()
        {
            ResponseHandler.Clear();
            ResponseHandler.WriteLine($"{_building.Name} (Airco: {_building.Airco.AircoStatus})");
        }


        protected override void ScreenOptions()
        {
            ResponseHandler.WriteLine("----------------------");
            ResponseHandler.WriteLine("N) Turn Airco ON");
            ResponseHandler.WriteLine("F) Turn Airco OFF");
            ResponseHandler.WriteLine("R) Add rack");
        }

        protected override bool HandleKey(char key)
        {
            switch (key.ToLowerInvariantString())
            {
                case "n":
                    SetAircoStatus(AircoStatus.On);
                    break;
                case "f":
                    SetAircoStatus(AircoStatus.Off);
                    break;
                case "r":
                    AddRack();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void AddRack()
        {
            //RequestHandler
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

                ResponseHandler.WriteLine($"Airco is turned {aircoStatus}");
            }
            catch (AircoTemperatureTooHighException)
            {
                ResponseHandler.WriteLine("The outside temparature is too high: don't turn off the airco!");
            }
            catch (AircoTemperatureTooLowException)
            {
                ResponseHandler.WriteLine("The outside temparature is too low: no need to turn on the arico.");
            }
            finally
            {
                RequestHandler.ReadLine();
                Show();
            }
        }
    }
}