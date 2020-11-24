//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    static public class DataSource
    {
        public static List<Person> persons;
        public static List<Host> hosts;
        public static List<HostingUnit> hostingUnits;
        public static List<Order> orders;
        public static List<GuestRequest> guestRequests;
        public static List<BankBranch> bankBranches;
        static DataSource()
        {
               //hard coding our datasource
            persons = new List<Person>()
            {
                new Person()
                {
                    UserType="Host",
                    Id = 2,
                    IdType = ID.ID,
                    Status = Status.ACTIVE,
                    Password = "2",
                    FirstName = "Mendi",
                    LastName = "Shneorson",
                    PhoneNumber = 100,
                    MailAddress = "Mendi160@gmail.com",
                },
                new Person()
                {
                    UserType="Guest",
                    Id = 1,
                    IdType = ID.PASSPORT,
                    Status = Status.ACTIVE,
                    Password = "1",
                    FirstName = "Yehonatan",
                    LastName = "Eliyahu",
                    PhoneNumber = 102,
                    MailAddress = "rede24@gmail.com",
                },
                new Person()
                {
                    Id = 111555387,
                    IdType = ID.PASSPORT,
                    Status = Status.ACTIVE,
                    Password = "123456",
                    FirstName = "yossi",
                    LastName = "Jaguar",
                    PhoneNumber = 102,
                    MailAddress = "jkuuj@pp.com",
                },
                 new Person()
                {
                    Id = 111555387,
                    IdType = ID.ID,
                    Status = Status.ACTIVE,
                    Password = "123456",
                    FirstName = "yossi",
                    LastName = "Jaguar",
                    PhoneNumber = 102,
                    MailAddress = "jkuuj@pp.com",
                },
                new Person()
                {
                    Id = 204290689,
                    IdType = ID.ID,
                    Status = Status.ACTIVE,
                    Password = "danzilbers",
                    FirstName = "dan",
                    LastName = "ziber",
                    PhoneNumber = 100,
                    MailAddress = "jkj@hj.com",
                }
            };
            hosts = new List<Host>()
            {
                new Host()
                {
                    HostID = 204290688,
                    BankNumber = 11,
                    BranchNumber = 196,
                    BankAccuontNumber = 111,
                    CollectionClearance = true,
                    Website=  "xxx",
                    TotalCommission= 0,
                    Status= Status.ACTIVE
                },
                new Host()
                {
                    HostID = 1,
                    BankNumber = 11,
                    BranchNumber = 196,
                    BankAccuontNumber = 111,
                    CollectionClearance = true,
                    Website=  "xxx",
                    TotalCommission= 0,
                    Status= Status.ACTIVE
                }
            };
            hostingUnits = new List<HostingUnit>()
            {
                new HostingUnit()
                {
                    Key = 123,
                    Owner = 1,
                    HostingUnitName = "blue sky",
                    Diary = new bool[12, 31],
                    Status= Status.ACTIVE,
                    Area= Location.JERUSALEM
                },new HostingUnit()
                {
                    Key = 111,
                    Owner = 1,
                    HostingUnitName = "Bamba center",
                    Diary = new bool[12, 31],
                    Status= Status.ACTIVE,
                    Area= Location.CENTER
                },new HostingUnit()
                {
                    Key = 222,
                    Owner = 1,
                    HostingUnitName = "Crap",
                    Diary = new bool[12, 31],
                    Status= Status.ACTIVE,
                    Area= Location.NORTH,
                    PricePerNight =400
                },new HostingUnit()
                {
                    Key = 333,
                    Owner = 1,
                    HostingUnitName = "Antricote",
                    Diary = new bool[12, 31],
                    Status= Status.ACTIVE,
                    Area= Location.JERUSALEM,
                    ImageLink1="https://q-cf.bstatic.com/images/hotel/max1024x768/232/232804926.jpg",
                      ImageLink2="https://q-cf.bstatic.com/images/hotel/max1024x768/232/232804926.jpg",
                        ImageLink3="https://q-cf.bstatic.com/images/hotel/max1024x768/232/232804926.jpg"
                }

            };
            guestRequests = new List<GuestRequest>()
            {
                new GuestRequest()
                {
                    GuestRequestKey = 1,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.JERUSALEM,                   
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 2,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 111555387
                },
                new GuestRequest()
                {
                    GuestRequestKey = 3,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.JERUSALEM,                    
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 111555387
                },
                new GuestRequest()
                {
                    GuestRequestKey = 4,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey =5,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 6,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 111555387
                },
                new GuestRequest()
                {
                    GuestRequestKey = 6,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 7,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 8,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 4,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 9,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },          new GuestRequest()
                {
                    GuestRequestKey = 1,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.JERUSALEM,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 2,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 5,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 3,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.JERUSALEM,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 4,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 8,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey =5,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 6,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 7,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 3,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 8,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 3,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 9,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },          new GuestRequest()
                {
                    GuestRequestKey = 1,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.JERUSALEM,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 2,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 3,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.JERUSALEM,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 4,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey =5,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 6,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.CENTER,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 7,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 20),
                    ReleaseDate = new DateTime(2020, 2, 25),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 6,
                    children = 0,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 8,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.SOUTH,
                    Type = Hosting_Type.HOTEL,
                    Adults = 2,
                    children = 5,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                },
                new GuestRequest()
                {
                    GuestRequestKey = 9,
                    Status = Request_Statut.OPEN,
                    RegistrationDate = DateTime.Now,
                    EntryDate = new DateTime(2020, 2, 24),
                    ReleaseDate = new DateTime(2020, 2, 28),
                    Area = Location.NORTH,
                    Type = Hosting_Type.ZIMMER,
                    Adults = 2,
                    children = 1,
                    Pool = Preferences.YES,
                    Jacuzzi = Preferences.YES,
                    Garden = Preferences.YES,
                    ChildrensAttractions = Preferences.NO,
                    ClientID= 1
                }

            };
            bankBranches = new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankName= "BOA",
                    BankNumber= 11,
                    BranchAddress = "11213 Brooklyn NY",
                    BranchCity = "NYC baby",
                    BranchNumber= 196

                },
                 new BankBranch()
                {
                    BankName= "BOA",
                    BankNumber= 12,
                    BranchAddress = "11213 crown",
                    BranchCity = "atlanta",
                    BranchNumber= 999

                },
                  new BankBranch()
                {
                    BankName= "BOA",
                    BankNumber= 12,
                    BranchAddress = "11213 hollywood",
                    BranchCity = "los angels",
                    BranchNumber= 396

                },
                   new BankBranch()
                {
                    BankName= "BOA",
                    BankNumber= 12,
                    BranchAddress = "167 miami beach",
                    BranchCity = "miami",
                    BranchNumber= 543

                },
                    new BankBranch()
                {
                    BankName= "BOA",
                    BankNumber= 12,
                    BranchAddress = "768 vhj",
                    BranchCity = "boston",
                    BranchNumber= 770

                },

            };
            orders = new List<Order>()
            {
                new Order()
                {
                    Key = 5,
                    HostingUnitKey = 222,
                    GuestRequestKey = 3,
                    Status = Order_Status.PENDING,
                    OrderDate =new DateTime(2019, 12, 23),
                    SentDate = new DateTime(2019, 12, 25),
                    
                    HostID=1,
                    CliendID=1,
                    Commission =0
                }
            };

        }
    }
}
