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
            Note note = Find(id);
            if (note != null) book.Remove(note);
        }

        public static void Edit(int id, Dictionary <string, string> data)
        {
            Note note = Find(id);
            if (note != null) note.Edit(data);
        }

        public static Note Find(int id)
        {
            foreach (Note elem in book)
            {
                if (elem.Id == id)
                {
                    return elem;
                }
            }
            return null;
        }
    }
}
