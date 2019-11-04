using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Note
    {
        private static int amount = 0;
        public int Id { get; set; }
        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string MiddleName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Country { get; private set; }
        public string Dob { get; private set; }
        public string Organisation { get; private set; }
        public string Position { get; private set; }
        public string Marks { get; private set; }
        public Note(Dictionary<string, string> fields)
        {
            Note.amount += 1;
            this.Id = Note.amount;
            this.Surname = fields["Surname"];
            this.Name = fields["Name"];
            this.MiddleName = fields["MiddleName"];
            this.PhoneNumber = fields["PhoneNumber"];
            this.Country = fields["Country"];
            this.Dob = fields["DateOfBirth"];
            this.Organisation = fields["Organisation"];
            this.Position = fields["Position"];
            this.Marks = fields["Marks"];
        }

        public override string ToString()
        {
            return $"ID: {Id}; Name: {Name}; Middle name: {MiddleName}; Surname: {Surname}; Phone Number: {PhoneNumber}; " +
                $"Country: {Country}; Date of Birth: {Dob}; Organisation: {Organisation}; Position: {Position}; Marks: {Marks}";
        }
    }
}
