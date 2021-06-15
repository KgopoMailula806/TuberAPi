using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TuberAPI.Data;

namespace TuberAPI.Infrastructure
{
    public static class SeedDataExtension_
    {

        public static void SeedDB(this ModelBuilder modelBuilder)
        {
            //Modules
            modelBuilder.Entity<Module>().HasData(
                new Module
                {
                    Id = 1,
                    Module_Name = "History",
                    Module_Code = "HS",
                },
                  new Module
                  {
                      Id = 2,
                      Module_Name = "Graph Theory",
                      Module_Code = "GT",
                  },
                    new Module
                    {
                        Id = 3,
                        Module_Name = "Linear Algebra",
                        Module_Code = "LA",
                    },
                      new Module
                      {
                          Id = 4,
                          Module_Name = "Applied math",
                          Module_Code = "Apm",
                      }, new Module
                      {
                          Id = 5,
                          Module_Name = "Natural science",
                          Module_Code = "Ns",
                      }, new Module
                      {
                          Id = 6,
                          Module_Name = "Life orientation",
                          Module_Code = "Lo",
                      }, new Module
                      {
                          Id = 7,
                          Module_Name = "Social Science",
                          Module_Code = "SS",
                      }, new Module
                      {
                          Id = 8,
                          Module_Name = "Physical Science",
                          Module_Code = "Phys",
                      }, new Module
                      {
                          Id = 9,
                          Module_Name = "Life Science",
                          Module_Code = "Ls",
                      }, new Module
                      {
                          Id = 10,
                          Module_Name = "Agriculture",
                          Module_Code = "Agri",
                      }

                );

            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullNames = "Mustard Malala",
                    Surname = "Petroes",
                    Valid_Phone_Number = "0817896358",
                    Email_Address = "malala@gmail.com",
                    PassWord = "testData123",
                    Gender = "Male",
                    Image = 0,
                    Age = 22,
                    User_Discriminator = "Tutor",
                    isActive = 1
                },
                 new User
                 {
                     Id = 2,
                     FullNames = "Moloya ma",
                     Surname = "kinja",
                     Valid_Phone_Number = "0718896358",
                     Email_Address = "kinja@gmail.com",
                     PassWord = "testData123",
                     Gender = "Female",
                     Image = 0,
                     Age = 24,
                     User_Discriminator = "Tutor",
                     isActive = 1
                 },
                  new User
                  {
                      Id = 3,
                      FullNames = "Jelous",
                      Surname = "Marawa",
                      Valid_Phone_Number = "0715589632",
                      Email_Address = "Marawa@gmail.com",
                      PassWord = "testData123",
                      Gender = "Male",
                      Image = 0,
                      Age = 20,
                      User_Discriminator = "Tutor",
                      isActive = 1
                  },
                   new User
                   {
                       Id = 4,
                       FullNames = "Kombibi",
                       Surname = "Plove",
                       Valid_Phone_Number = "0817896358",
                       Email_Address = "Plove@gmail.com",
                       PassWord = "testData123",
                       Gender = "Female",
                       Image = 0,
                       Age = 19,
                       User_Discriminator = "Tutor",
                       isActive = 1
                   },
                    new User
                    {
                        Id = 5,
                        FullNames = "Skumba",
                        Surname = "Bolopi",
                        Valid_Phone_Number = "0615547889",
                        Email_Address = "Bolopi@gmail.com",
                        PassWord = "testData123",
                        Gender = "Female",
                        Image = 0,
                        Age = 25,
                        User_Discriminator = "Tutor",
                        isActive = 1
                    },
                     new User
                     {
                         Id = 6,
                         FullNames = "Rhulani",
                         Surname = "Mlambo",
                         Valid_Phone_Number = "0815798645",
                         Email_Address = "Mlambo@gmail.com",
                         PassWord = "testData123",
                         Gender = "Male",
                         Image = 0,
                         Age = 35,
                         User_Discriminator = "Tutor",
                         isActive = 1
                     },
                      new User
                      {
                          Id = 7,
                          FullNames = "Keane Allessandro",
                          Surname = "Burgers",
                          Valid_Phone_Number = "0115550125",
                          Email_Address = "Burgers@gmail.com",
                          PassWord = "DataView123",
                          Gender = "Male",
                          Image = 0,
                          Age = 19,
                          User_Discriminator = "Manager",
                          isActive = 1
                      },
                       new User
                       {
                           Id = 8,
                           FullNames = "Kgopo",
                           Surname = "Mailula",
                           Valid_Phone_Number = "0815569870",
                           Email_Address = "Kgopo@gmail.com",
                           PassWord = "testData123",
                           Gender = "Male",
                           Image = 0,
                           Age = 20,
                           User_Discriminator = "Manager",
                           isActive = 1
                       },
                        new User
                        {
                            Id = 9,
                            FullNames = "Jacob",
                            Surname = "Muzonde",
                            Valid_Phone_Number = "0418996358",
                            Email_Address = "CBY@gmail.com",
                            PassWord = "testData123",
                            Gender = "Male",
                            Image = 0,
                            Age = 22,
                            User_Discriminator = "Manager",
                            isActive = 1
                        },

                        
                         new User
                         {
                             Id = 10,
                             FullNames = "Max",
                             Surname = "Maximus",
                             Valid_Phone_Number = "0817896358",
                             Email_Address = "Maximus@gmail.com",
                             PassWord = "testData123",
                             Gender = "Male",
                             Image = 0,
                             Age = 30,
                             User_Discriminator = "Client",
                             isActive = 1
                         },

                          new User
                          {
                              Id = 11,
                              FullNames = "Max",
                              Surname = "Ndlovu",
                              Valid_Phone_Number = "0112368795",
                              Email_Address = "Nd@gmail.com",
                              PassWord = "testData123",
                              Gender = "Female",
                              Image = 0,
                              Age = 22,
                              User_Discriminator = "Client",
                              isActive = 0
                          },
                           new User
                           {
                               Id = 12,
                               FullNames = "Madumane",
                               Surname = "Mphori",
                               Valid_Phone_Number = "0817896358",
                               Email_Address = "Madumane@gmail.com",
                               PassWord = "testData123",
                               Gender = "Male",
                               Image = 0,
                               Age = 45,
                               User_Discriminator = "Client",
                               isActive = 1
                           },
                            new User
                            {
                                Id = 13,
                                FullNames = "Lover",
                                Surname = "Mjekeke",
                                Valid_Phone_Number = "0816589632",
                                Email_Address = "Mjekeke@gmail.com",
                                PassWord = "testData123",
                                Gender = "Female",
                                Image = 0,
                                Age = 19,
                                User_Discriminator = "Client",
                                isActive = 1
                            },
                             new User
                             {
                                 Id = 14,
                                 FullNames = "Chipiwe",
                                 Surname = "Test",
                                 Valid_Phone_Number = "0147895236",
                                 Email_Address = "Test@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Male",
                                 Image = 0,
                                 Age = 19,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             },
                             new User
                             {
                                 Id = 15,
                                 FullNames = "Jery",
                                 Surname = "Springer",
                                 Valid_Phone_Number = "0644456699",
                                 Email_Address = "Springer@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Male",
                                 Image = 0,
                                 Age = 15,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             },
                             new User
                             {
                                 Id = 16,
                                 FullNames = "Daliwonga",
                                 Surname = "UyangTesta",
                                 Valid_Phone_Number = "0147895236",
                                 Email_Address = "UyangTesta@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Female",
                                 Image = 0,
                                 Age = 14,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             },
                             new User
                             {
                                 Id = 17,
                                 FullNames = "Polly",
                                 Surname = "Graphoed",
                                 Valid_Phone_Number = "0817896354",
                                 Email_Address = "Graphoed@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Female",
                                 Image = 0,
                                 Age = 19,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             },
                             new User
                             {
                                 Id = 18,
                                 FullNames = "Vuyo",
                                 Surname = "Big Dreamer",
                                 Valid_Phone_Number = "0147895236",
                                 Email_Address = "Dreams@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Male",
                                 Image = 0,
                                 Age = 19,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             },
                             new User
                             {
                                 Id = 19,
                                 FullNames = "LastNumber",
                                 Surname = "test19",
                                 Valid_Phone_Number = "0816527412",
                                 Email_Address = "test19@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Male",
                                 Image = 0,
                                 Age = 19,
                                 User_Discriminator = "Client",
                                 isActive = 1
                             }

            );



            
            modelBuilder.Entity<Client>().HasData(

                new Client
                {
                    Id = 1,
                    Current_Grade = "Grade 10",
                    Institution = "John Ore High",
                    User_Table_Reference = 10

                },
                new Client
                {
                    Id =2,
                    Current_Grade = "Grade 9",
                    Institution = "Die Fakkel High",
                    User_Table_Reference = 11
                },
                new Client
                {
                    Id = 3,
                    Current_Grade = "Grade 8",
                    Institution = "Mossi High",
                    User_Table_Reference = 12
                },
                new Client
                {
                    Id = 4,
                    Current_Grade = "Grade 12",
                    Institution = "Sir John Adamson",
                    User_Table_Reference = 13
                },
                new Client
                {
                    Id = 5,
                    Current_Grade = "First Year",
                    Institution = "University Of Witswatersrand",
                    User_Table_Reference = 14
                },
                new Client
                {
                    Id = 6,
                    Current_Grade = "Third Year",
                    Institution = "University Of Johannesburg",
                    User_Table_Reference = 15
                },
                new Client
                {
                    Id = 7,
                    Current_Grade = "Grade 5",
                    Institution = "Winchester Ridge ",
                    User_Table_Reference = 16
                },
                new Client
                {
                    Id = 8,
                    Current_Grade = "Fourth Year",
                    Institution = "University Of Johannesburg",
                    User_Table_Reference = 17
                },
                new Client
                {
                    Id = 9,
                    Current_Grade = "Second Year",
                    Institution = "University Of Pretoria",
                    User_Table_Reference = 18
                },
                new Client
                {
                    Id = 10,
                    Current_Grade = "First Year",
                    Institution = "Varsity College",
                    User_Table_Reference = 19
                }
             ) ;


            
            modelBuilder.Entity<Tutor>().HasData
            (
                new Tutor
                {
                    Id = 1,
                    Is_Accepted = 1,
                    User_Table_Reference = 1
                },
                new Tutor
                {
                    Id = 2,
                    Is_Accepted = 1,
                    User_Table_Reference = 2
                },
                new Tutor
                {
                    Id = 3,
                    Is_Accepted = 1,
                    User_Table_Reference = 3
                },
                new Tutor
                {
                    Id = 4,
                    Is_Accepted = 1,
                    User_Table_Reference = 4
                },
                new Tutor
                {
                    Id = 5,
                    Is_Accepted = 1,
                    User_Table_Reference = 5
                },
                new Tutor
                {
                    Id = 6,
                    Is_Accepted = 0,
                    User_Table_Reference = 6
                }

            );



            
            modelBuilder.Entity<Client_Module>().HasData
            (
                new Client_Module
                {
                    Id = 1,
                    clientRef = 1,
                    Is_Active = 1,
                    DateAssigned = "2020/09/21",
                    ModuleId = 1

                },
                 new Client_Module
                 {
                     Id = 2,
                     clientRef = 1,
                     Is_Active = 1,
                     DateAssigned = "2020/09/21",
                     ModuleId = 8

                 },
                  new Client_Module
                  {
                      Id = 3,
                      clientRef = 2,
                      Is_Active = 1,
                      DateAssigned = "2020/09/21",
                      ModuleId = 5

                  },
                   new Client_Module
                   {
                       Id = 4,
                       clientRef = 2,
                       Is_Active = 1,
                       DateAssigned = "2020/09/21",
                       ModuleId = 7

                   },
                    new Client_Module
                    {
                        Id = 5,
                        clientRef = 3,
                        Is_Active = 1,
                        DateAssigned = "2020/09/21",
                        ModuleId = 10

                    },
                     new Client_Module
                     {
                         Id = 6,
                         clientRef = 3,
                         Is_Active = 1,
                         DateAssigned = "2020/09/21",
                         ModuleId = 6

                     },
                     new Client_Module
                     {
                         Id = 7,
                         clientRef = 4,
                         Is_Active = 1,
                         DateAssigned = "2020/09/21",
                         ModuleId = 8

                     },
                     new Client_Module
                     {
                         Id = 8,
                         clientRef = 4,
                         Is_Active = 1,
                         DateAssigned = "2020/09/21",
                         ModuleId = 10

                     },
                     new Client_Module
                     {
                         Id = 9,
                         clientRef = 5,
                         Is_Active = 1,
                         DateAssigned = "2020/09/21",
                         ModuleId = 2

                     },
                      new Client_Module
                      {
                          Id = 10,
                          clientRef = 5,
                          Is_Active = 1,
                          DateAssigned = "2020/09/21",
                          ModuleId = 3

                      },
                       new Client_Module
                       {
                           Id = 11,
                           clientRef = 6,
                           Is_Active = 1,
                           DateAssigned = "2020/09/21",
                           ModuleId = 3

                       },
                        new Client_Module
                        {
                            Id = 12,
                            clientRef = 6,
                            Is_Active = 1,
                            DateAssigned = "2020/09/21",
                            ModuleId = 4

                        },
                         new Client_Module
                         {
                             Id = 13,
                             clientRef = 7,
                             Is_Active = 1,
                             DateAssigned = "2020/09/21",
                             ModuleId = 5

                         },
                          new Client_Module
                          {
                              Id = 14,
                              clientRef = 7,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 7

                          },
                          new Client_Module
                          {
                              Id = 15,
                              clientRef = 8,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 2

                          },
                          new Client_Module
                          {
                              Id = 16,
                              clientRef = 8,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 3

                          },
                          new Client_Module
                          {
                              Id = 17,
                              clientRef = 9,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 4

                          }, new Client_Module
                          {
                              Id = 18,
                              clientRef = 9,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 10

                          },
                          new Client_Module
                          {
                              Id = 19,
                              clientRef = 10,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 1

                          },
                          new Client_Module
                          {
                              Id = 20,
                              clientRef = 10,
                              Is_Active = 1,
                              DateAssigned = "2020/09/21",
                              ModuleId = 10

                          }


             );

            
            modelBuilder.Entity<Tutor_Module>().HasData
            (
                new Tutor_Module
                {
                    Id =1,
                    Is_Active =1 ,
                    Module_Reference = 1,
                    Tutor_Reference = 1,
                    Date_Assigned = "2020/09/21"
                    
                },
                 new Tutor_Module
                 {
                     Id = 2,
                     Is_Active = 1,
                     Module_Reference = 2,
                     Tutor_Reference = 1,
                     Date_Assigned = "2020/09/21"

                 },
                  new Tutor_Module
                  {
                      Id = 3,
                      Is_Active = 1,
                      Module_Reference = 3,
                      Tutor_Reference =2,
                      Date_Assigned = "2020/09/21"

                  },
                   new Tutor_Module
                   {
                       Id = 4,
                       Is_Active = 1,
                       Module_Reference = 4,
                       Tutor_Reference = 3,
                       Date_Assigned = "2020/09/21"

                   },
                    new Tutor_Module
                    {
                        Id = 5,
                        Is_Active = 1,
                        Module_Reference = 5,
                        Tutor_Reference = 5,
                        Date_Assigned = "2020/09/21"

                    },
                     new Tutor_Module
                     {
                         Id = 6,
                         Is_Active = 1,
                         Module_Reference = 6,
                         Tutor_Reference = 6,
                         Date_Assigned = "2020/09/21"

                     },
                      new Tutor_Module
                      {
                          Id = 8,
                          Is_Active = 1,
                          Module_Reference = 7,
                          Tutor_Reference = 5,
                          Date_Assigned = "2020/09/21"

                      },
                       new Tutor_Module
                       {
                           Id = 9,
                           Is_Active = 1,
                           Module_Reference = 8,
                           Tutor_Reference = 4,
                           Date_Assigned = "2020/09/21"

                       },
                        new Tutor_Module
                        {
                            Id = 10,
                            Is_Active = 1,
                            Module_Reference = 9,
                            Tutor_Reference = 6,
                            Date_Assigned = "2020/09/21"

                        },
                         new Tutor_Module
                         {
                             Id = 11,
                             Is_Active = 1,
                             Module_Reference = 10,
                             Tutor_Reference = 2,
                             Date_Assigned = "2020/09/21"

                         }
             );


            
            modelBuilder.Entity<BookingRequest>().HasData
            (
                new BookingRequest
                {
                    Id = 1,
                    ClientProposedLocation = "186 Letitia Street Johannesburg",
                    Client_Reference = 1,
                    EndTime = "14:30",
                    IsRespondedTo = 1 ,
                    Is_Accepted = 1 ,
                    ModuleID1 = 1,
                    Periods = 1 ,
                    RequestDate ="2020/09/21" ,
                    RequestTime = "13:30", 
                    Tutor_Reference = 1
                },
                new BookingRequest
                {
                    Id = 2,
                    ClientProposedLocation = "78 James Street Sadupi",
                    Client_Reference = 5,
                    EndTime = "10:30",
                    IsRespondedTo = 1,
                    Is_Accepted = 1,
                    ModuleID1 = 3,
                    Periods = 1,
                    RequestDate = "2020/09/21",
                    RequestTime = "09:30",
                    Tutor_Reference = 2
                },
                new BookingRequest
                {
                    Id = 3,
                    ClientProposedLocation = "48 Tramway Street Gauteng",
                    Client_Reference = 6,
                    EndTime = "15:30",
                    IsRespondedTo = 1,
                    Is_Accepted = 1,
                    ModuleID1 = 4,
                    Periods = 1,
                    RequestDate = "2020/09/21",
                    RequestTime = "14:30",
                    Tutor_Reference = 3
                },
                new BookingRequest
                {
                    Id = 4,
                    ClientProposedLocation = "15 Standoff CODM Johannesburg",
                    Client_Reference = 5,
                    EndTime = "16:15",
                    IsRespondedTo = 1,
                    Is_Accepted = 1,
                    ModuleID1 = 2,
                    Periods = 1,
                    RequestDate = "2020/09/21",
                    RequestTime = "15:15",
                    Tutor_Reference = 1
                }
             );

            modelBuilder.Entity<ClientBooking>().HasData
            (
                new ClientBooking
                {
                    Id = 1,
                    BookingDetails_BookingRequestTable_Reference = 1,
                    Client_Table_Reference = 1,
                    Date_Time = "2020/09/10 13:30:00",
                    EndTime = "14:30",
                    isActive =1 ,
                    Periods =1,
                    Tutor_Table_Reference = 1 
                },
                new ClientBooking
                {
                    Id = 2,
                    BookingDetails_BookingRequestTable_Reference = 2,
                    Client_Table_Reference = 5,
                    Date_Time = "2020/09/10 09:30:00",
                    EndTime = "10:30",
                    isActive = 1,
                    Periods = 1,
                    Tutor_Table_Reference = 2
                },
                new ClientBooking
                {
                    Id = 3,
                    BookingDetails_BookingRequestTable_Reference = 3,
                    Client_Table_Reference = 6,
                    Date_Time = "2020/09/10 14:30:00",
                    EndTime = "15:30",
                    isActive = 1,
                    Periods = 1,
                    Tutor_Table_Reference = 3
                },
                new ClientBooking
                {
                    Id = 4,
                    BookingDetails_BookingRequestTable_Reference = 4,
                    Client_Table_Reference = 5,
                    Date_Time = "2020/09/10 14:30:00",
                    EndTime = "15:30",
                    isActive = 1,
                    Periods = 1,
                    Tutor_Table_Reference = 1
                }
             );
            
            modelBuilder.Entity<Tutorial_Session>().HasData
            (
                new Tutorial_Session
                {
                    Id = 1,
                    ClientBookingID = 4,
                    Client_Reference =5,
                    Tutor_Id = 1,
                    IsCompleted = 1,
                    Session_Date = "2020/09/10",
                    Session_Start_Time= "14:30" ,
                     Session_End_Time = "15:30" ,
                     Geographic_Location = "Chris Hani Rd, Klipspruit 318-Iq, Johannesburg, 1809, South Africa_-26.2156544_27.8974073",
                     
                },
                new Tutorial_Session
                {
                    Id = 2,
                    ClientBookingID = 3,
                    Client_Reference = 6,
                    Tutor_Id = 3,
                    IsCompleted = 1,
                    Session_Date = "2020/09/10",
                    Session_Start_Time = "14:30",
                    Session_End_Time = "15:30",
                    Geographic_Location = "86 Ralufutso St, Moletsane, Soweto, 1868, South Africa_-26.2156548_27.8974074",

                },
                new Tutorial_Session
                {
                    Id = 3,
                    ClientBookingID = 2,
                    Client_Reference = 5,
                    Tutor_Id = 2,
                    IsCompleted = 1,
                    Session_Date = "2020/09/10",
                    Session_Start_Time = "14:30",
                    Session_End_Time = "15:30",
                    Geographic_Location = "86 Ralufutso St, Moletsane, Soweto, 1868, South Africa_-26.2156548_27.8974074",

                }
             );

            
            modelBuilder.Entity<Rating>().HasData(
                new Rating
                {
                    Id =1,
                    Client_ID = 5,
                    Client_Rating = 5,
                    Tutor_ID = 2,
                    Tutor_Rating = 8,
                    Session_Reference = 3
                },
                new Rating
                {
                    Id = 2,
                    Client_ID = 6,
                    Client_Rating = 8,
                    Tutor_ID = 3,
                    Tutor_Rating = 10,
                    Session_Reference = 2
                },
                new Rating
                {
                    Id = 3,
                    Client_ID = 5,
                    Client_Rating = 9,
                    Tutor_ID = 1,
                    Tutor_Rating = 7,
                    Session_Reference = 1
                }
            );


            
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = 1,
                    Amount = "150.0",
                    Client_ID = 5,
                    Date_Issued = "2020/09/10",
                    Description = "Tutorial Session",
                    is_Paid = 0,
                    Session_ID = 1,
                    Payment_Method =""
                },
                new Invoice
                {
                    Id = 2,
                    Amount = "300.0",
                    Client_ID = 6,
                    Date_Issued = "2020/09/10",
                    Description = "Tutorial Session",
                    is_Paid = 1,
                    Session_ID = 2,
                    Payment_Method = "Pay Pal"
                },
                new Invoice
                {
                    Id = 3,
                    Amount = "500.0",
                    Client_ID = 5,
                    Date_Issued = "2020/09/10",
                    Description = "Tutorial Session",
                    is_Paid = 1,
                    Session_ID = 3,
                    Payment_Method = "Pay Pal",
                }

            );

            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    User_Table_Reference = 7
                },
                new Manager
                {
                    Id = 2,
                    User_Table_Reference = 8
                },
                new Manager
                {
                    Id = 3,
                    User_Table_Reference = 9
                }
            );


        }

            
    }

}
