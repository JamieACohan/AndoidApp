// Entity Framework Core demo, Code First
// 1:N relationship, navigation property, a lecturer teaches many modules

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PTTrainerApp
{
    public class Trainer
    {
        public int ID { get; set; }                 // PK and identity
        public string Name { get; set; }            // null
        public string Phone { get; set; }
        public double Rating { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public double Price { get; set; }   // null

        // navigation property to modules that Lecturer teaches, virtual => lazy loading  
        public virtual ICollection<Availability> Availabilities { get; set; }
    }

    public class Availability
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }  // PK and identity
        public string Location { get; set; }// null
                                            // not null, use int? for null

        // foreign key property, null, follows convention for naming
        public int? TrainerID { get; set; }
        // update relationship through this property, not through navigation property
        // int would not allow null for LecturerID                 

        // navigation property to Lecturer for this module
        public virtual Trainer Trainer { get; set; }           // virtual enables "lazy loading" 
    }

    // context class
    public class TrainerContext : DbContext
    {
        // localDB connection string
        // c:\users\gary\CollegeDB1.mdf
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:ptbookingapp.database.windows.net,1433;Initial Catalog=PTBookingAppDB;Persist Security Info=False;User ID=JamieTania;Password=Tallaght1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
    }


    class TrainerRepository
    {

        // print lecturers, their ids, names, and the names of modules they teach 
        public void DoTrainerQuery()
        {
            using (TrainerContext db = new TrainerContext())
            {
                var trainers = db.Trainers.OrderBy(t => t.ID).Select(t => new { Id = t.ID, Name = t.Name, Availabilities = t.Availabilities });

                Console.WriteLine("\nTrainers:");
                foreach (var trainer in trainers)
                {
                    Console.WriteLine("id: " + trainer.Id + " name: " + trainer.Name);
                    Console.WriteLine("available: ");

                    // Modules is a navigation propery of type ICollection<Module>

                    var trainerAvailabilities = trainer.Availabilities;
                    foreach (var trainerAvailability in trainerAvailabilities)
                    {
                        Console.WriteLine(trainerAvailability.Location);
                    }
                }
            }
        }

        // prints the ID and name of each module and the name of the lecturer who teaches it
        public void DoAvailabilityQuery()
        {
            using (TrainerContext db = new TrainerContext())
            {
                // select all modules, ordered by module name
                var availabilities = db.Availabilities.OrderBy(availability => availability.ID).ToList();       // load

                Console.WriteLine("\nAvailabilities:");
                foreach (var availability in availabilities)
                {
                    Console.WriteLine("id: " + availability.ID + " name: " + availability.Location + " ");

                    if (availability.Trainer != null)
                    {
                        // Lecturer is a navigation property of type Lecturer
                        Console.WriteLine(" availability by: " + availability.Trainer.Name);
                    }
                }
            }
        }

        // add a lecturer, modules being taught left null for moment
        public void AddTrainer(Trainer trainer)
        {
            using (TrainerContext db = new TrainerContext())
            {
                try
                {
                    // add and save
                    db.Trainers.Add(trainer);
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        // add a module, contains lecturerID
        public void AddAvailability(Availability availability)
        {
            using (TrainerContext db = new TrainerContext())
            {
                try
                {
                    // add and save
                    db.Availabilities.Add(availability);
                    db.SaveChanges();
                    // navigation properties updated on both sides
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }


        class Program
        {
            static void Main()
            {

                TrainerRepository repository = new TrainerRepository();

                Trainer gc = new Trainer() { ID = 1, Name = "GC", Phone = "2898", Rating = 5, Email = "Garry@G.ie", Gender = "Female", Price = 10 };
                repository.AddTrainer(gc);         // ID now assigned

                // teaches 2 modules
                Availability monday = new Availability() { Day = "Monday", Time = "12.00", Location="Tallaght", TrainerID = gc.ID };
                Availability tuesday = new Availability() { Day = "Tuesday", Time = "12.00", Location="Ballier", TrainerID = gc.ID };
                //Module oosdev2 = new Module() { Name = "OOSDEV2", Credits = 5, LecturerID = gc.ID };

                Availability a1 = new Availability() { Day = "Sunday", Time = "1.00", Location="Tallaght"};       // null for LecturerID

                repository.AddAvailability(monday);
                repository.AddAvailability(tuesday);
                repository.AddAvailability(a1);

                repository.DoTrainerQuery();
                repository.DoAvailabilityQuery();

                Console.ReadLine();
            }
        }
    }
}



