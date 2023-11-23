using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Human> humans = new List<Human>();

    static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("1- Add a student");
            Console.WriteLine("2- Add an employee");
            Console.WriteLine("3- Search");
            Console.WriteLine("4- Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        AddEmployee();
                        break;
                    case 3:
                        SearchMenu();
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (choice != 4);
    }

    static void AddStudent()
    {
        Console.Write("Enter student's name: ");
        string name = Console.ReadLine();
        Console.Write("Enter student's surname: ");
        string surname = Console.ReadLine();
        Console.Write("Enter student's age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Invalid input. Please enter a valid age.");
        }

        Console.Write("Enter student's grade: ");
        int grade;
        while (!int.TryParse(Console.ReadLine(), out grade))
        {
            Console.WriteLine("Invalid input. Please enter a valid grade.");
        }

        Student student = new Student(name, surname, age, grade);
        humans.Add(student);
        Console.WriteLine("Student added successfully.");
    }

    static void AddEmployee()
    {
        Console.Write("Enter employee's name: ");
        string name = Console.ReadLine();
        Console.Write("Enter employee's surname: ");
        string surname = Console.ReadLine();
        Console.Write("Enter employee's age: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age))
        {
            Console.WriteLine("Invalid input. Please enter a valid age.");
        }

        Console.Write("Enter employee's position: ");
        string position = Console.ReadLine();

        Employee employee = new Employee(name, surname, age, position);
        humans.Add(employee);
        Console.WriteLine("Employee added successfully.");
    }

    static void SearchMenu()
    {
        int searchChoice;
        do
        {
            Console.WriteLine("1- Employee search");
            Console.WriteLine("2- Student search");
            Console.WriteLine("3- Back to main menu");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out searchChoice))
            {
                switch (searchChoice)
                {
                    case 1:
                        EmployeeSearch();
                        break;
                    case 2:
                        StudentSearch();
                        break;
                    case 3:
                        Console.WriteLine("Returning to the main menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (searchChoice != 3);
    }

    static void EmployeeSearch()
    {
        Console.Write("Enter position to search: ");
        string position = Console.ReadLine();
        var employees = humans.OfType<Employee>().Where(e => e.Position == position).ToList();

        if (employees.Any())
        {
            Console.WriteLine("Employees in position {0}:", position);
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Name} {employee.Surname}");
            }
        }
        else
        {
            Console.WriteLine("No employees found in position {0}.", position);
        }
    }

    static void StudentSearch()
    {
        Console.Write("Enter minGrade: ");
        int minGrade;
        while (!int.TryParse(Console.ReadLine(), out minGrade))
        {
            Console.WriteLine("Invalid input. Please enter a valid grade.");
        }

        Console.Write("Enter maxGrade: ");
        int maxGrade;
        while (!int.TryParse(Console.ReadLine(), out maxGrade) || maxGrade < minGrade)
        {
            Console.WriteLine("Invalid input. Please enter a valid grade greater than or equal to minGrade.");
        }

        var students = humans.OfType<Student>().Where(s => s.Grade >= minGrade && s.Grade <= maxGrade).ToList();

        if (students.Any())
        {
            Console.WriteLine("Students with grades between {0} and {1}:", minGrade, maxGrade);
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} {student.Surname}, Grade: {student.Grade}");
            }
        }
        else
        {
            Console.WriteLine("No students found in the specified grade range.");
        }
    }
}

class Human
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public Human(string name, string surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }
}

class Student : Human
{
    public int Grade { get; set; }

    public Student(string name, string surname, int age, int grade) : base(name, surname, age)
    {
        if (grade < 0)
        {
            throw new ArgumentException("Grade cannot be negative.");
        }
        Grade = grade;
    }
}

class Employee : Human
{
    public string Position { get; set; }

    public Employee(string name, string surname, int age, string position) : base(name, surname, age)
    {
        if (string.IsNullOrEmpty(position))
        {
            throw new ArgumentException("Position cannot be null or empty.");
        }
        Position = position;
    }
}