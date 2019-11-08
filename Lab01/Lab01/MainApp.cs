using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class MainApp
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello, this is PhoneBook utility, want to continue? [y/n]");
                string hello_answer = Console.ReadLine();
                if (hello_answer == "y")
                {
                    Console.WriteLine("What do you want [new/delete/edit/show/glance]?");
                    string wtd_decision = Console.ReadLine();
                    if (wtd_decision == "new")
                    {
                        Console.WriteLine("Give me information by the following format. All blank fields will be filled with NS(NotStated) expression");
                        Console.WriteLine("Name: *name*, MiddleName: *middle name*, Surname: *surname*, PhoneNumber: *phone number*, " +
                                "Country: *country*, DateOfBirth: *date of birth*, Organisation: *organisation*, Position: *position*, Marks: *marks*");
                        Console.WriteLine("Remember, note must have \"Name\", \"Surname\", \"PhoneNumber\" and \"Country\" fields, otherwise note won't be added");
                        string raw_info = Console.ReadLine();
                        Dictionary<string, string> ParsedInfo = Parser.Parse(raw_info);
                        if ((ParsedInfo["Surname"] != "NS") && (ParsedInfo["Name"] != "NS") && (ParsedInfo["PhoneNumber"] != "NS") && (ParsedInfo["Country"] != "NS"))
                        {
                            PhoneBook.book.Add(new Note(ParsedInfo));
                            Console.WriteLine("Done!");
                        }
                        else Console.WriteLine("Not all necessary fields added or format violation! Try again and be assured that \"Name\", " +
                            "\"Surname\", \"PhoneNumber\" and \"Country\" fields are filled and format is followed");
                    }
                    if (wtd_decision == "delete")
                    {
                        Console.WriteLine("OK, which note do you want to delete? [ID]");
                        int id;
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            PhoneBook.Delete(id);
                            Console.WriteLine("Done!");
                        }
                        else
                        {
                            Console.WriteLine("ID should be integer");
                        }
                    }
                    if (wtd_decision == "edit")
                    {
                        Console.WriteLine("OK, which note do you want to edit? [ID]");
                        int id;
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Give me information by the following format. All blank fields will be filled with NS(NotStated) expression");
                            Console.WriteLine("Name: *name*, MiddleName: *middle name*, Surname: *surname*, PhoneNumber: *phone number*, " +
                                    "Country: *country*, DateOfBirth: *date of birth*, Organisation: *organisation*, Position: *position*, Marks: *marks*");
                            string raw_info = Console.ReadLine();
                            PhoneBook.Edit(id, Parser.Parse(raw_info));
                            Console.WriteLine("Done!");
                        }
                        else
                        {
                            Console.WriteLine("ID should be integer");
                        }
                    }
                    if (wtd_decision == "show")
                    {
                        PhoneBook.Show();
                    }
                    if (wtd_decision == "glance")
                    {
                        PhoneBook.Glance();
                    }

                }
                else if (hello_answer == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Your answer in not clear, please make sure it looks like \"y\" or \"n\" ");
                }
            }
        }
    }
}
