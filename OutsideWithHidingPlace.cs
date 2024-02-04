using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_House_With_IHidingPlace
{
    class OutsideWithHidingPlace: Outside,IHidingPlace
    {
        public OutsideWithHidingPlace(string name,bool hot,string hidingPlace):base(name,hot)
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
