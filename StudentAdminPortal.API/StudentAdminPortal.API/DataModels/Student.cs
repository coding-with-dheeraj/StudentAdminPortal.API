﻿using System.ComponentModel.DataAnnotations;

namespace StudentAdminPortal.API.DataModels
{
    public class Student
    {
        //Use prop + Tab to create property
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid GenderId { get; set; }

        //Navigation Properties
        public Gender? Gender { get; set; }
        public Address? Address { get; set; }
    }
}
