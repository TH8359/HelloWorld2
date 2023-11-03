using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;


internal class Program
{
    

    private static void Main(string[] args)
    {
        //Empty lists
       string validList = "Valid Email Addresses: ";
       string invalidList = "Invalid Email Addresses: ";

        //Starting point
        Console.WriteLine("Enter file name");
        var file = Console.ReadLine();
        Console.WriteLine($"{Environment.NewLine}Searching directory...");

        //Searches the directory for the file and parses the information within the file
        string directoryPath = @"C:\Users\hogue\source\repos\Technical Assignment 2\Technical Assignment 3\TA4\TA5\HelloWorld2";
        string targetFileName = $"{file}";

        string[] csvFiles = Directory.GetFiles(directoryPath, targetFileName, System.IO.SearchOption.AllDirectories);

        if (csvFiles.Length > 0) 
        {
            Console.WriteLine("File located.");

            string csvFilePath = $@"C:\Users\hogue\source\repos\Technical Assignment 2\Technical Assignment 3\TA4\TA5\HelloWorld2\{file}";
            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields()!;
                
                if (fields != null)
                {
                    
                    foreach (string field in fields)
                    {
                        Console.Write(field + "\t");
                    }
                    Console.WriteLine(); 
                }
            }

            //Determines whether a row's email address is valid or not and then adds it to the correct list
            string[] lines = File.ReadAllLines(csvFilePath);
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        Regex regex = new Regex(emailPattern);

         foreach (var line in lines)
        {
            
            string[] fields = line.Split(',');

            
            string emailAddress = fields.Length > 2 ? fields[2].Trim() : null!;
            

            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                if (regex.IsMatch(emailAddress))
                {
                    
                    validList = validList + emailAddress + ", ";
                }
                else
                {
                    
                    invalidList = invalidList + emailAddress + ", ";
                }
            }
            else
            {
                Console.WriteLine("Email address is missing or empty.");
            }

        }
        }

        //Lists for the valid and invalid email addresses are printed out
        validList = validList.Remove(validList.Length - 2);
        invalidList = invalidList.Remove(invalidList.Length - 2);
        Console.WriteLine($"{validList}");
        Console.WriteLine($"{invalidList}");

        }
        else
        {
            Console.WriteLine("File not found");
             }
    
        //Used to exit the program
        Console.Write($"{Environment.NewLine}Press any key to exit...");
        Console.ReadKey(true);
    }
}