using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.models;
using TuberAPI.Infrastructure;
using System.Security.Claims;

namespace TuberAPI.Data
{
    public class TuberDbContext : DbContext
    {

        public TuberDbContext(DbContextOptions<TuberDbContext> options) : base(options)
        {

        }

        //Setting up the fluent API 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Setting up the  many to many relationships with the fluent api
            builder.Entity<Tutor_Module>().HasOne(x => x.Tutor).WithMany(x => x.Tutor_Modules).HasForeignKey(x => x.Tutor_Reference);
            builder.Entity<Tutor_Module>().HasOne(x => x.Module).WithMany(x => x.Tutor_Modules).HasForeignKey(x => x.Module_Reference);
            builder.Entity<Client_Module>().HasOne(x => x.Module_Reference).WithMany(x => x.Client_Modules).HasForeignKey(x => x.ModuleId);
            builder.Entity<Client_Module>().HasOne(x => x.Client_Reference).WithMany(x => x.Client_Modules).HasForeignKey(x => x.clientRef);

            base.OnModelCreating(builder);
            builder.SeedDB();
        }

        internal object FindAsync(ClaimsPrincipal user, object[] v)
        {
            throw new NotImplementedException();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Client_Module> Client_Modules { get; set; }
        public DbSet<Tutorial_Session> Tutorial_Sessions { get; set; }
        public DbSet<ManagersLog> ManagersLogs { get; set; }
        public DbSet<Tutor_Module> Tutor_Modules { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<Invoice> Invoices{ get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<TutorBooking> TutorBookings { get; set; }
        public DbSet<ClientBooking> ClientBookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<TutorDocument> TutorDocuments { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
