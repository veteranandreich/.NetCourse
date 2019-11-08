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
        public string PhoneNumber { get; private set; } // PhoneNumber is a string and it's not a mistake. For example, in the US it's common to use letters, like "1-800-MY-APPLE" etc.
        public string Country { get; private set; }
        public string Dob { get; private set; }
        public string Organisation { get; private set; }
        public string Position { get; private set; }
        public string Marks { get; private set; }
        public Note(Dictionary<string, string> fields)
        {
            this.Id = ++Note.amount;
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

        public void Edit(Dictionary<string, string> data)
        {
            if (data["Surname"] != "NS") this.Surname = data["Surname"];
            if (data["Name"] != "NS") this.Name = data["Name"];
            if(data["MiddleName"] != "NS") this.MiddleName = data["MiddleName"];
            if(data["PhoneNumber"] != "NS") this.PhoneNumber = data["PhoneNumber"];
            if(data["Country"] != "NS") this.Country = data["Country"];
            if (data["DateOfBirth"] != "NS") this.Dob = data["DateOfBirth"];
            if (data["Organisation"] != "NS") this.Organisation = data["Organisation"];
            if (data["Position"] != "NS") this.Position = data["Position"];
            if (data["Marks"] != "NS") this.Marks = data["Marks"];
        }

        public override string ToString()
        {
            return $"ID: {Id}; Name: {Name}; Middle name: {MiddleName}; Surname: {Surname}; Phone Number: {PhoneNumber}; " +
                $"Country: {Country}; Date of Birth: {Dob}; Organisation: {Organisation}; Position: {Position}; Marks: {Marks}";
        }
    }
}
