/*****************************************
 * Program: Assignment 1
 * Programmer: Sam Rutledge & Andrew Madden
 * zID: z1584845 & z1784580
 * Due Date: 2/2/18
 ****************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assign1
{
    public class Person : IComparable <Person>
    {
        //private data members
        private string name;
        private string officeNum;
        private string phoneNum;

        //constructor
        public Person(string n, string o, string p)
        {
            name = n;
            officeNum = o;
            phoneNum = p;
        }

        //set & get method for Name
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //set & get method for OfficeNum
        public string OfficeNum
        {
            get { return officeNum; }
            set { officeNum = value; }
        }

        //set & get method for PhoneNum
        public string PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }

        //compare function for IComparable interface
        public int CompareTo(Person other)
        {
            return name.CompareTo(other.name);
        }
    }


    class Program
    {
        public static int InUse = 0; //counter for number of entries
        public static Person[] arr = new Person[20]; //array to hold 20 Persons
        public static string name, office, phone; //data members

        static void Main(string[] args)
        {
            using (StreamReader SR = new StreamReader("data1.txt"))
            {
                //read information from data1.txt
                while ((name = SR.ReadLine()) != null)
                {
                    office = SR.ReadLine();
                    phone = SR.ReadLine();

                    //create new person object with data
                    arr[InUse] = new Person(name, office, phone);

                    //increment counter
                    InUse++;
                }
            }
            //call main menu after reading file
            MainMenu();
        }

        public static void MainMenu()
        {
            //holds user selection
            string selection;

            //display menu
            Console.WriteLine("Please make a selection\n");
            Console.WriteLine("a) Print the list");
            Console.WriteLine("b) Add an entry");
            Console.WriteLine("c) Search for a name");
            Console.WriteLine("d) Search for an office number");
            Console.WriteLine("e) Search for a telephone number");
            Console.WriteLine("f) Change an office number");
            Console.WriteLine("g) Sort the list by name");
            Console.WriteLine("h) Quit");
            Console.WriteLine();

            //read user input and convert to lower case
            selection = Console.ReadLine();
            selection = selection.ToLower();

            //switch for menu options, controls flow of program
            switch (selection)
            {
                case ("a"):
                    PrintList();
                    break;
                case ("b"):
                    AddPerson();
                    break;
                case ("c"):
                    NameSearch();
                    break;
                case ("d"):
                    OfficeSearch();
                    break;
                case ("e"):
                    PhoneSearch();
                    break;
                case ("f"):
                    ChangeOfficeNum();
                    break;
                case ("g"):
                    SortArray();
                    Console.WriteLine("\n\nArray sorted.\n");
                    MainMenu();
                    break;
                case ("h"):
                    break;
                default:
                    Console.WriteLine("\n\nPlease enter only the letters \"a\" through \"h\"\n");
                    MainMenu();
                    break;
            }
        }
        
        public static void PrintList()
        {
            Console.WriteLine("\n\n");

            //loop through array and print data members
            for (int i = 0; i < InUse; i++)
            {
                Console.WriteLine("{0} {1} {2}", arr[i].Name.PadRight(15), arr[i].OfficeNum.PadRight(8), arr[i].PhoneNum.PadRight(15));
            }
            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();

            //call back to main menu again
            MainMenu();
        }

        private static void AddPerson()
        {
            //variables to hold user input
            string newName, newOffice, newPhone;
            bool nameMatch = false;

            //assign input to variables
            Console.WriteLine("\nPlease enter a name");
            newName = Console.ReadLine();
            Console.WriteLine("\nPlease enter a office number");
            newOffice = Console.ReadLine();
            Console.WriteLine("\nPlease eneter a phone number");
            newPhone = Console.ReadLine();

            //check for duplicate names
            for (int i = 0; i < InUse; i++)
            {
                if (newName == arr[i].Name)
                {
                    Console.WriteLine("\nError. Entered name already exists.\n");
                    nameMatch = true;
                }
            }

            //if name is not a duplicate
            if (!nameMatch)
            {
                //add new person into Person array and add to data1.txt
                using (StreamWriter SW = new StreamWriter("data1.txt", true))
                {
                    arr[InUse] = new Person(newName, newOffice, newPhone);
                    InUse++;
                    SW.WriteLine(newName);
                    SW.WriteLine(newOffice);
                    SW.WriteLine(newPhone);
                    SW.Close();
                }
                Console.WriteLine("\nDone!\n");
            }
            //call back to main menu again
            MainMenu();
        }

        private static void NameSearch()
        {
            //hold the name of person being searched
            string name;
            bool found = false;

            //prompt user for search name
            Console.WriteLine("Please enter the name of the person you'd like to search");
            name = Console.ReadLine();

            //search through array for match & print the Person
            for (int i = 0; i < InUse; i++)
            {
                if (name == arr[i].Name)
                {
                    Console.WriteLine("\n\n{0} {1} {2}\n", arr[i].Name, arr[i].OfficeNum, arr[i].PhoneNum);
                    found = true;
                }
            }

            //if there is no match
            if (!found)
                Console.WriteLine("\n\nThe name {0} was not found\n", name);

            //call back to main menu again
            MainMenu();
        }

        private static void OfficeSearch()
        {
            //hold the office number of person being searched
            string office;
            bool found = false;

            //prompt user for search name
            Console.WriteLine("Please enter the office number of the person you'd like to search");
            office = Console.ReadLine();

            //search through array for match
            for (int i = 0; i < InUse; i++)
            {
                if (office == arr[i].OfficeNum)
                {
                    Console.WriteLine("{0} {1} {2}", arr[i].Name, arr[i].OfficeNum, arr[i].PhoneNum);
                    found = true;
                }
            }

            //if there is no match
            if (!found)
                Console.WriteLine("\n\nThe office number {0} was not found\n", office);

            //call back to main menu again
            MainMenu();
        }

        private static void PhoneSearch()
        {
            //hold the name of person being searched
            string phone;
            bool found = false;

            //prompt user for search phone number
            Console.WriteLine("Please enter the telephone number of the person you'd like to search");
            phone = Console.ReadLine();

            //search through array for phone number, then print the Person
            for (int i = 0; i < InUse; i++)
            {
                if (phone == arr[i].PhoneNum)
                {
                    Console.WriteLine("\n\n{0} {1} {2}\n", arr[i].Name, arr[i].OfficeNum, arr[i].PhoneNum);
                    found = true;
                }
            }

            //if there is no match
            if (!found)
                Console.WriteLine("\n\nThe telephone number {0} was not found\n", phone);

            //call back to main menu again
            MainMenu();
        }

        private static void ChangeOfficeNum()
        {
            //holds user input
            string name;
            string newOffice;
            bool found = false;

            //prompt the user to enter existing name and new office number
            Console.WriteLine("Please enter existing name for office number you would like to change");
            name = Console.ReadLine();
            Console.WriteLine("Please enter the new office number");
            newOffice = Console.ReadLine();

            //search through array for name, then update office number
            for (int i = 0; i < InUse; i++)
            {
                if (name == arr[i].Name)
                {
                    found = true;
                    arr[i].OfficeNum = newOffice;
                    Console.WriteLine("\nDone!\n");
                }
            }

            //if the name cannot be found
            if(!found)
                Console.WriteLine("Office number could not be changed. Name not found.");
            
            //call back to main menu again
            MainMenu();
        }

        private static void SortArray()
        {
            //sorts the array invoking the CompareTo function
            Array.Sort(arr, 0, InUse);
        }
    }
}

