using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MariTeam
{
    internal class Ship
    {
        // definisi instance variable
        private string _shipID;
        private string _shipName;
        private double _cargoWeight;
        private string _status;

        // definisi property
        public string ShipID
        {
            get { return _shipID; }
        }
        public string ShipName
        {
            get { return _shipName; }
            set { _shipName = value; }
        }
        public double CargoWeight
        {
            get { return _cargoWeight; }
            set
            {
                if (value >= 0)
                {
                    _cargoWeight = value;
                }
                else
                {
                    throw new ArgumentException("Cargo weight cannot be negative.");
                }
            }
        }
        public string Status
        {
            get { return _status; }
        }
        // definisi method
        public double GetCargoWeight()
        {
            return _cargoWeight;
        }
        public void updateCargoWeight(double newWeight)
        {
            if (newWeight >= 0)
            {
                _cargoWeight = newWeight;
            }
            else
            {
                throw new ArgumentException("Cargo weight cannot be negative.");
            }
        }
        public void setStatus(string newStatus)
        {
            _status = newStatus;
        }
    }
}
