using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_House_With_IHidingPlace
{
    class RoomWithHidingPlace: Room, IHidingPlace
    {
        public RoomWithHidingPlace(string name,string decoration,string hidingPlace) : base(name, decoration)
        {
            this.hidingPlace = hidingPlace;
        }

        private string hidingPlace;

        public string HidingPlace
        {
            get { return hidingPlace; }
        }
    }
}
