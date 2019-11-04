using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public static class PhoneBook
    {
        public static List<Note> book = new List<Note>();
        public static void Glance()
        {
            foreach (Note elem in book)
            {
                Console.WriteLine($"ID: {elem.Id}; Name: {elem.Name}; Surname: {elem.Surname}; Phone Number: {elem.PhoneNumber}");
            }
        }

        public static void Show()
        {
            foreach (Note elem in book)
            {
                Console.WriteLine(elem);
            }
        }

        public static void Delete(int id)
        {
            foreach (Note elem in book)
            {
                if (elem.Id == id)
                {
                    book.Remove(elem);
                    break;
                }
            }
        }
    }
}
