using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_House_With_IHidingPlace
{
    class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {
        public RoomWithDoor(string name, string decoration, string hidingPlace, string doorDescription) : base(name, decoration, hidingPlace)
        {
            this.DoorDescription = doorDescription;
        }
        private string doorDescription;
        public string DoorDescription { 
            get {return this.doorDescription; }
            set { this.doorDescription = value; }
        }

        private Location doorLocation;
        public Location DoorLocation {
            get { return this.doorLocation; }
            set { this.doorLocation = value; }
        }
    }
}
