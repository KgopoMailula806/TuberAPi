using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.models;
using Microsoft.EntityFrameworkCore;

namespace TuberAPI.Infrastructure
{
    public static class SeedDataExtension
    {

        public static void SeedDB(this ModelBuilder modelBuilder)
        {
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
                    isActive =1
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
                           Surname = "Petroes",
                           Valid_Phone_Number = "0815569870",
                           Email_Address = "Petroes@gmail.com",
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
                            Surname = "Mngomezulu",
                            Valid_Phone_Number = "0418996358",
                            Email_Address = "Mngomezulu@gmail.com",
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
                             User_Discriminator = "Manager",
                             isActive = 1
                         },

                          new User
                          {
                              Id = 11,
                              FullNames = "Max",
                              Surname = "Ndlovu",
                              Valid_Phone_Number = "0112368795",
                              Email_Address = "Maximus@gmail.com",
                              PassWord = "testData123",
                              Gender = "Female",
                              Image = 0,
                              Age = 22,
                              User_Discriminator = "Tutor",
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
                               User_Discriminator = "Tutor",
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
                                User_Discriminator = "Tutor",
                                isActive = 1
                            },
                             new User
                             {
                                 Id = 14,
                                 FullNames = "UserNo14",
                                 Surname = "Test",
                                 Valid_Phone_Number = "0147895236",
                                 Email_Address = "Test@gmail.com",
                                 PassWord = "testData123",
                                 Gender = "Male",
                                 Image = 0,
                                 Age = 19,
                                 User_Discriminator = "Tutor",
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
                                 FullNames = "UserNo16",
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
            modelBuilder.Entity<Tutor>().HasData(
                 new Tutor
                 {
                     Id = 1,                
                     Is_Accepted = 0,
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
                       Is_Accepted = 0,
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
                         Is_Accepted = 0,
                         User_Table_Reference = 5
                     },
                      new Tutor
                      {
                          Id = 6,                         
                          Is_Accepted = 0,
                          User_Table_Reference = 6
                      },
                       new Tutor
                       {
                           Id = 7,                           
                           Is_Accepted = 0,
                           User_Table_Reference = 11
                       },
                        new Tutor
                        {
                            Id = 8,                         
                            Is_Accepted = 0,
                            User_Table_Reference = 12
                        },
                         new Tutor
                         {
                             Id = 9,                      
                             Is_Accepted = 0,
                             User_Table_Reference = 13
                         },
                          new Tutor
                          {
                              Id = 10,                           
                              Is_Accepted = 0,
                              User_Table_Reference = 14
                          }

              );
            modelBuilder.Entity<Client>().HasData(
               new Client
               {
                   Id = 1,
                   Current_Grade = "First year",
                   Institution = "University of Johannesburg",
                   User_Table_Reference = 15
               },
                new Client
                {
                    Id = 2,
                    Current_Grade = "12",
                    Institution = "John Ore High",
                    User_Table_Reference = 16
                },
                 new Client
                 {
                     Id = 3,
                     Current_Grade = "Second year",
                     Institution = "Wits",
                     User_Table_Reference = 17
                 },
                  new Client
                  {
                      Id = 4,
                      Current_Grade = "third year",
                      Institution = "UP",
                      User_Table_Reference = 18
                  },
                   new Client
                   {
                       Id = 5,
                       Current_Grade = "8",
                       Institution = "Sedibeng high",
                       User_Table_Reference = 19
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
                },
                new Manager
                {
                    Id = 4,                  
                    User_Table_Reference = 10
                }


                );

            modelBuilder.Entity<Tutor_Module>().HasData(
                new Tutor_Module
                {

                    Id = 1,
                    Date_Assigned = "20/01/2020",
                    Is_Active = 1,
                    Tutor_Reference = 1,
                    Module_Reference = 1
                },
                 new Tutor_Module
                 {

                     Id = 2,
                     Date_Assigned = "20/01/2020",
                     Is_Active = 1,
                     Tutor_Reference = 1,
                     Module_Reference = 2
                 },
                  new Tutor_Module
                  {

                      Id = 3,
                      Date_Assigned = "20/02/2020",
                      Is_Active = 1,
                      Tutor_Reference = 2,
                      Module_Reference = 2
                  },
                   new Tutor_Module
                   {

                       Id = 4,
                       Date_Assigned = "21/02/2020",
                       Is_Active = 1,
                       Tutor_Reference = 2,
                       Module_Reference = 1
                   },
                    new Tutor_Module
                    {

                        Id = 5,
                        Date_Assigned = "20/01/2020",
                        Is_Active = 1,
                        Tutor_Reference = 6,
                        Module_Reference = 5
                    },
                     new Tutor_Module
                     {

                         Id = 6,
                         Date_Assigned = "25/02/2020",
                         Is_Active = 1,
                         Tutor_Reference = 7,
                         Module_Reference = 10
                     },
                      new Tutor_Module
                      {

                          Id = 7,
                          Date_Assigned = "22/03/2020",
                          Is_Active = 1,
                          Tutor_Reference = 4,
                          Module_Reference = 9
                      },
                       new Tutor_Module
                       {

                           Id = 8,
                           Date_Assigned = "10/01/2020",
                           Is_Active = 1,
                           Tutor_Reference = 4,
                           Module_Reference = 8
                       },
                        new Tutor_Module
                        {
                            Id = 9,
                            Date_Assigned = "20/01/2020",
                            Is_Active = 1,
                            Tutor_Reference = 5,
                            Module_Reference = 1
                        },
                         new Tutor_Module
                         {

                             Id = 10,
                             Date_Assigned = "20/01/2020",
                             Is_Active = 1,
                             Tutor_Reference = 3,
                             Module_Reference = 6
                         }
           );
            modelBuilder.Entity<Client_Module>().HasData(
                    new Client_Module
                    {
                        Id = 1,
                        DateAssigned = "2020/01/02",
                        Is_Active = 1,
                        ModuleId = 1,
                        clientRef = 1,
                    },
                      new Client_Module
                      {
                          Id = 2,
                          DateAssigned = "2020/01/02",
                          Is_Active = 1,
                          ModuleId = 1,
                          clientRef = 2
                      },
                        new Client_Module
                        {
                            Id = 3,
                            DateAssigned = "2020/01/02",
                            Is_Active = 1,
                            ModuleId = 2,
                            clientRef = 3
                        },
                          new Client_Module
                          {
                              Id = 4,
                              DateAssigned = "2020/01/06",
                              Is_Active = 1,
                              ModuleId = 5,
                              clientRef = 2
                          },
                            new Client_Module
                            {
                                Id = 5,
                                DateAssigned = "2020/01/07",
                                Is_Active = 1,
                                ModuleId = 10,
                                clientRef = 4
                            },
                              new Client_Module
                              {
                                  Id = 6,
                                  DateAssigned = "2020/02/02",
                                  Is_Active = 1,
                                  ModuleId = 9,
                                  clientRef = 5
                              }
            );
        }
    }

}
