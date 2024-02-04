using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _7_House_With_IHidingPlace
{
    class Oponent
    {
        private Location myLocation;
        private Random random;
        public Oponent(Location myLocation)
        {
            this.myLocation = myLocation;
            this.random = new Random();
        }
        public void Move()
        {
            
            if (this.myLocation is IHasExteriorDoor )
            {   
                if(this.random.Next(2) == 1)
                {
                    IHasExteriorDoor myLocationHasExteriorDoor = this.myLocation as IHasExteriorDoor;
                    this.myLocation = myLocationHasExteriorDoor.DoorLocation;
                }
                
            }
            bool hidden = false;
            while (!hidden)
            {
                int rdm = this.random.Next(0, myLocation.Exits.Length);
                myLocation = myLocation.Exits[rdm];
                if(myLocation is IHidingPlace)
                {
                    hidden = true;
                }
            }
            Console.WriteLine(this.myLocation.Name);
        }

        public bool Check(Location locationToCheck)
        {
            return (this.myLocation == locationToCheck);
        }

        public void PopMessageFound(int moves)
        {
            MessageBox.Show($"You found me in {moves.ToString()} moves");
        }
    }
}
