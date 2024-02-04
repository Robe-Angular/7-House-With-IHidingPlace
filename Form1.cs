using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_House_With_IHidingPlace
{
    public partial class Form1 : Form
    {
        Location currentLocation;
        RoomWithDoor livingRoom;
        Room diningRoom;
        RoomWithDoor kitchen;
        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        Room stairs;
        RoomWithHidingPlace upstairsHallway;
        RoomWithHidingPlace masterBedroom;
        RoomWithHidingPlace secondBedroom;
        RoomWithHidingPlace bathroom;
        OutsideWithHidingPlace driveway;
        Oponent oponent;
        int moves;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            ResetGame();
        }

        public void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living Room", "An antique carpet", "inside the closet","an oak door with a brass knob");
            diningRoom = new RoomWithHidingPlace("Dining Room", "a crystal chandeller","in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen","stainless steel appliances", "in the cabinet","a screen door");
            frontYard = new OutsideWithDoor("Front Yard", false,"an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false,"Sheed");
            stairs = new Room("Stairs", "woooden bannister");
            upstairsHallway = new RoomWithHidingPlace("Upstairs hallway", "picture of a dog", "closet");
            masterBedroom = new RoomWithHidingPlace("Master bedroom", "","Large bed");
            secondBedroom = new RoomWithHidingPlace("Second bedroom", "", "Small bed");
            bathroom = new RoomWithHidingPlace("Bathroom", "A sink and a Toilet", "Shower");
            driveway = new OutsideWithHidingPlace("Driveway", true, "Garage");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom,stairs };
            kitchen.Exits = new Location[] { diningRoom };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            stairs.Exits = new Location[] { livingRoom, upstairsHallway };
            upstairsHallway.Exits = new Location[] { stairs,masterBedroom,secondBedroom, bathroom };
            masterBedroom.Exits = new Location[] { upstairsHallway };
            secondBedroom.Exits = new Location[] { upstairsHallway };
            bathroom.Exits = new Location[] { upstairsHallway };
            driveway.Exits = new Location[] { frontYard, backYard };



            

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;

        }
        private void MoveToANewLocation(Location newLocation)
        {
            currentLocation = newLocation;
            descriptionTextBox.Text = newLocation.Description;
            exitsComboBox.Items.Clear();

            foreach(var exit in newLocation.Exits)
            {
                exitsComboBox.Items.Add(exit.Name);
            }
            exitsComboBox.SelectedIndex = 0;
            goThroughTheDoorButton.Visible = currentLocation is IHasExteriorDoor;
            RedrawForm();
        }

        private void goHereButton_Click(object sender, EventArgs e)
        {
            this.moves++;
            MoveToANewLocation(currentLocation.Exits[exitsComboBox.SelectedIndex]);
            
        }

        private void goThroughTheDoorButton_Click(object sender, EventArgs e)
        {
            this.moves++;
            IHasExteriorDoor currentLocationWithDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(currentLocationWithDoor.DoorLocation);
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            hideButton.Visible = false;
            for (int i = 0; i < 10; i++)
            {
                descriptionTextBox.Text = i.ToString();
                this.oponent.Move();
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);
                
            }
            descriptionTextBox.Text = "Ready or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            MoveToANewLocation(livingRoom);
            RedrawForm();
        }

        private void RedrawForm()
        {
            goHereButton.Visible = true;
            exitsComboBox.Visible = true;
            checkButton.Visible = currentLocation is IHidingPlace;
            if (checkButton.Visible)
            {
                IHidingPlace currentLocationWithHidingPlace = currentLocation as IHidingPlace;
                checkButton.Text = $"Check on {currentLocationWithHidingPlace.HidingPlace}";
                Console.WriteLine(currentLocationWithHidingPlace.HidingPlace);
            }
            
        }

        private void ResetGame()
        {
            goHereButton.Visible = false;
            exitsComboBox.Visible = false;
            goThroughTheDoorButton.Visible = false;
            checkButton.Visible = false;
            hideButton.Visible = true;
            this.oponent = new Oponent(frontYard);
            this.moves = 0;
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            this.moves++;
            if (this.oponent.Check(currentLocation))
            {
                this.oponent.PopMessageFound(this.moves);
                descriptionTextBox.Text = $"You found the oponent on: {this.currentLocation.Name}, in {this.moves} moves";
                ResetGame();
            }
        }
    }

}
