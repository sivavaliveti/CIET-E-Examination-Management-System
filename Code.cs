using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

class Person
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string gender { get; set; }
    public Person(string name, string username, string password, string phoneNumber, string email, DateTime dateOfBirth)
    {
        Name = name;
        Username = username;
        Password = password;
        PhoneNumber = phoneNumber;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public virtual void DisplayProfile()
    {

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("          PROFILE       ");
        Console.WriteLine("=============================");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"\tNAME          : {Name}");
        Console.WriteLine($"\tUSERNAME      : {Username}");
        Console.WriteLine($"\tPHONE N0      : {PhoneNumber}");
        Console.WriteLine($"\tE-MAIL        : {Email}");
        Console.WriteLine($"\tDATE OF BIRTH : {DateOfBirth.ToShortDateString()}");
        Console.WriteLine();

    }
}
class Student : Person
{
   

    public Student(string name, string username, string password, string phoneNumber, string email, string gender, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
        
    }
    public List<Answer> Answers { get; set; } = new List<Answer>();

    public override void DisplayProfile()
    {
        Console.WriteLine(" PORTFOLIO :");
        base.DisplayProfile();


        Console.WriteLine();

    }

}
class TeachingStaff : Person
{
  

    public TeachingStaff(string name, string username, string password, string phoneNumber, string email, string gender, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
       
    }

    public override void DisplayProfile()
    {
        Console.WriteLine("\t\t\t\tTEACHING STAFF PROFILE");
        base.DisplayProfile();

    }
}
class NonTeachingStaff : Person
{
   
    public NonTeachingStaff(string name, string username, string password, string phoneNumber, string email, string gender, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
    }

    public override void DisplayProfile()
    {
        Console.WriteLine("\t\t\t\tNON-TEACHING STAFF PROFILR");
        base.DisplayProfile();
    }
}
class HOD : Person
{
    public HOD(string name, string username, string password, string phoneNumber, string email, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
    }

    public override void DisplayProfile()
    {
        Console.WriteLine("HOD PROFILE :");
        base.DisplayProfile();
    }
}
class HOI : Person
{
    public HOI(string name, string username, string password, string phoneNumber, string email, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
    }

    public override void DisplayProfile()
    {
        Console.WriteLine("\t\t\tHOI PROFILE");
        base.DisplayProfile();
    }
}
class COE : Person
{
    public COE(string name, string username, string password, string phoneNumber, string email, DateTime dateOfBirth)
        : base(name, username, password, phoneNumber, email, dateOfBirth)
    {
    }

    public void DisplayProfile()
    {
        Console.WriteLine("\t\t\tCOE PROFILE");
        base.DisplayProfile();
    }
}
public class Answer
{
    public string Question { get; set; }
    public string ChosenOption { get; set; }
}
public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> Options { get; set; } = new List<string>();
    public string CorrectAnswer { get; set; }
}

public class QuestionPaper
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public List<Question> Questions { get; set; } = new List<Question>();
}

public class QuestionPaperManager
{
    private static List<QuestionPaper> questionPapers = new List<QuestionPaper>();
    private static int nextQuestionPaperId = 1;
    private static List<Question> questions = new List<Question>();

    // Create Question Paper based on code 2
    public static void AddQuestionPaper()
    {
        Console.Write("ENTER THE TITLE OF QUESTION PAPER :");
        string title = Console.ReadLine();

        QuestionPaper newQuestionPaper = new QuestionPaper
        {
            Id = nextQuestionPaperId++,
            Title = title,
            DateCreated = DateTime.Now,
            Questions = new List<Question>(questions)  // Assigning the questions from code 2
        };

        questionPapers.Add(newQuestionPaper);
        Console.WriteLine("QUESTION PAPER ADDED SUCESSFULLY!\n");
    }

    // Read
    public static void ViewQuestionPapers()
    {
        if (questionPapers.Count == 0)
        {
            Console.Write("NO QUESTION PAPERS AVAILABLE.\n");
            return;
        }

        Console.Write("LIST OF QUESTION PAPER :");
        foreach (var paper in questionPapers)
        {
            Console.WriteLine($"ID: {paper.Id}, TITLE: {paper.Title}, DATE CREATED: {paper.DateCreated.ToShortDateString()}");
        }
        Console.WriteLine();
    }

    // Update
    public static void UpdateQuestionPaper()
    {
        Console.Write("ENTER THE ID OF QUESTION PAPER TO UPDATE :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("INVALID INPUT, PLEASE ENTER A VALID ID .");
        }

        QuestionPaper paperToUpdate = questionPapers.FirstOrDefault(p => p.Id == id);

        if (paperToUpdate != null)
        {
            Console.Write("ENTER THE NEW TITLE         :");
            paperToUpdate.Title = Console.ReadLine();

            Console.Write("ENTER THE NEW QUESTION TEXT :");
            paperToUpdate.Questions[0].Text = Console.ReadLine();

            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Enter new option {i + 1}:");
                paperToUpdate.Questions[0].Options[i] = Console.ReadLine();
            }

            Console.Write("ENTER THE CORRECT ANSWER (1/2/3/4):");
            int correctAnswerIndex;
            while (!int.TryParse(Console.ReadLine(), out correctAnswerIndex) || correctAnswerIndex < 1 || correctAnswerIndex > 4)
            {
                Console.Write("INVALID INPUT, PLEASE ENTER A NUMBER BETWEEN 1 AND 4.");
            }
            paperToUpdate.Questions[0].CorrectAnswer = paperToUpdate.Questions[0].Options[correctAnswerIndex - 1];

            Console.Write("QUESTION PAPER UPDATED SUCCESFULLY!\n");
        }
        else
        {
            Console.Write("QUESTION PAPER NOT FOUND.\n");
        }
    }

    // Delete
    public static void DeleteQuestionPaper()
    {
        Console.Write("ENTER THE ID OF QUESTION PAPER TO DELETE :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("INVALID INPUT , PLEASE ENTER VALID ID");
        }

        QuestionPaper paperToDelete = questionPapers.FirstOrDefault(p => p.Id == id);

        if (paperToDelete != null)
        {
            questionPapers.Remove(paperToDelete);
            Console.WriteLine("QUESTION PAPER DELETED SUCCESFULLY!\n");
        }
        else
        {
            Console.WriteLine("QUESTION PAPER NOT FOUND.\n");
        }
    }

    // Create Question Paper based on code 2
    static void CreateQuestionPaper()
    {
        Console.Write("HOW MANY QUESTIOND DO U WANT TO KEEP IN EXAM ? :");
        int numberOfQuestions;
        while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
        {
            Console.Write("INVALID INPUT , PLEASE ENTER A VALID NUMBER GREATER THAN ZER0.");
        }

        questions.Clear(); // Clear the list before adding new questions

        for (int i = 0; i < numberOfQuestions; i++)
        {
            Question question = new Question();

            Console.Write($"ENTER QUESTION {i + 1}:");
            question.Text = Console.ReadLine();

            for (int j = 0; j < 4; j++)
            {
                Console.Write($"ENTER OPTION {j + 1}:");
                question.Options.Add(Console.ReadLine());
            }

            Console.WriteLine("ENTER THE CORRECT ANSWER (1/2/3/4):");
            int correctAnswerIndex;
            while (!int.TryParse(Console.ReadLine(), out correctAnswerIndex) || correctAnswerIndex < 1 || correctAnswerIndex > 4)
            {
                Console.Write("INVALID INPUT, PLEASE ENTER A NUMBER BETWEEN  1 AND 4.");
            }
            question.CorrectAnswer = question.Options[correctAnswerIndex - 1];

            questions.Add(question);
        }

        Console.WriteLine("QUESTION PAPER SUCCESFULLY CREATED !");
        Thread.Sleep(2000);
        Console.Clear();
    }

    // View Question Paper based on code 2
    static void ViewQuestionPaper()
    {
        Console.Write("      QUESTION PAPER ");
        Console.WriteLine("- - - - - - - - - -");

        if (questions.Count == 0)
        {
            Console.WriteLine("No QUESTIONS AVAILABLE ");
            return;
        }

        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine($"QUESTION {i + 1}: {questions[i].Text}");

            for (int j = 0; j < questions[i].Options.Count; j++)
            {
                Console.WriteLine($"  {j + 1}. {questions[i].Options[j]}");
            }

            Console.WriteLine($"CORRECT ANSWER : {questions[i].CorrectAnswer}");
            Console.WriteLine("----------------");
        }
    }
    class Examination : QuestionPaperManager
    {
        static List<HOI> hoiList = new List<HOI>();//hoi
        static List<Student> students = new List<Student>();
        static List<TeachingStaff> teachingStaffList = new List<TeachingStaff>();
        static List<NonTeachingStaff> nonTeachingStaffList = new List<NonTeachingStaff>();
        static List<HOD> hodList = new List<HOD>();//hod
        static List<COE> coeList = new List<COE>();//coe
        static void Main(string[] args)
        {
            while (true)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("                        ||              WELCOME TO CIET E-EXAMINATION MANAGEMENT SYSTEM          || ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                                CHOOSE  YOUR  DESIGNATION    ");
                Console.WriteLine("                                          ===================================");
                Console.WriteLine("  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t\t\t\t\t1.STUDENT \n\t\t\t\t\t2.HEAD OF INSTITUTION - { PRINCIPAL }\n\t\t\t\t\t3.FACULTY\n\t\t\t\t\t4.CONTROLLER OF EXAMINATION - { C O E } \n\t\t\t\t\t5.EXAMINATION BLOCK \n\t\t\t\t\t6.EXIT ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ENTER YOUR DESIGNATION NUMBER: ");
                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        StudentMenu();
                        break;
                    case "2":
                        HOIMenu();
                        break;
                    case "3":
                        FacultyMenu();
                        break;
                    case "4":
                        COEMenu();
                        break;
                    case "5":
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("                                                CHOOSE  YOUR  DESIGNATION    ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("                                          ===================================");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("\t\t\t\t\t1.TEACHING-FACULTY \n\t\t\t\t\t2.HOD \n\t\t\t\t\t3.PRINCIPAL \n\t\t\t\t\t4.COE\n\t\t\t\t\t5.STUDENT\n\t\t\t\t\t6.EXIT");
                            Console.Write("\t\tENTER YOUR ROLE: ");
                            string que = Console.ReadLine();
                            Console.Clear();
                            switch (que)
                            {
                                case "1":
                                    Console.WriteLine("  \t\t\t\t\t L0GIN WITH YOUR DETAILS \n");

                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write("\tENTER USER_NAME  :");
                                    string username = Console.ReadLine();
                                    Console.Write("\tENTER PASSWORD   :");
                                    string password = Console.ReadLine();

                                    TeachingStaff teachingStaff = teachingStaffList.Find(ts => ts.Username == username && ts.Password == password);

                                    if (teachingStaff != null)
                                    {
                                        Console.WriteLine("\t\t\t\t\tLOGIN SUCCESFUL!\n");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        try
                                        {       // password verification
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\t\t\t\t\t\t TEACHING - FACULTY MENU ");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine("\t\t1. ADD    QUESTION PAPER");
                                            Console.WriteLine("\t\t2. VIEW   QUESTION PAPERS LIST");
                                            Console.WriteLine("\t\t3. UPDATE QUESTION PAPER");
                                            Console.WriteLine("\t\t4. DELETE QUESTION PAPER");
                                            Console.WriteLine("\t\t5. CREATE QUESTION PAPER ");
                                            Console.WriteLine("\t\t6. VIEW   QUESTION PAPER ");
                                            Console.WriteLine("\t\t7. EXIT");
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("\nENTER YOUR CHOICE : ");
                                            int QUE;
                                            while (!int.TryParse(Console.ReadLine(), out QUE))
                                            {
                                                Console.WriteLine("INVALID INPUT ,PLEASE ENTER A NUMBER ");
                                            }

                                            switch (QUE)
                                            {
                                                case 1:
                                                    AddQuestionPaper();
                                                    break;
                                                case 2:
                                                    ViewQuestionPapers();
                                                    break;
                                                case 3:
                                                    UpdateQuestionPaper();
                                                    break;
                                                case 4:
                                                    DeleteQuestionPaper();
                                                    break;
                                                case 5:
                                                    CreateQuestionPaper();
                                                    break;
                                                case 6:
                                                    ViewQuestionPaper();
                                                    break;
                                                case 7:
                                                    Environment.Exit(0);
                                                    break;
                                                default:
                                                    Console.WriteLine("INVALID CHOICE , PLEASE ENTER A NUMBER BETWEEN 1 AND 7 !");
                                                    break;
                                            }

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Exception Raised : Incorrect Password Entered ..try again " + e);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid username or password. Please try again.\n");
                                    }

                                    break;
                                case "2":
                                    Console.ForegroundColor = ConsoleColor.Green;

                                    Console.WriteLine("\t\t\tLOGIN ");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("\tENTER USERNAME :");
                                    username = Console.ReadLine();
                                    Console.Write("\tENTER PASSWORD :");
                                    password = Console.ReadLine();

                                    HOD hod = hodList.Find(h => h.Username == username && h.Password == password);

                                    if (hod != null)
                                    {

                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\t\t\t\tLOGIN SUCCESFUL!\n");
                                        Thread.Sleep(3000);
                                        Console.Clear();
                                        try
                                        {


                                            Console.WriteLine("1. VIEW ALL QUESTION PAPERS");
                                            Console.WriteLine("2. UPDATE QUESTION PAPER");
                                            Console.WriteLine("3. DELETE QUESTION PAPER");
                                            Console.WriteLine("4.VIEW QUESTION PAPER ");
                                            Console.WriteLine("5. EXIT");
                                            Console.Write("ENTER YOUR CHOICE : ");

                                            int QUE;
                                            while (!int.TryParse(Console.ReadLine(), out QUE))
                                            {
                                                Console.WriteLine("Invalid input. Please enter a number.");
                                            }

                                            switch (QUE)
                                            {
                                                case 1:
                                                    ViewQuestionPapers();
                                                    break;
                                                case 2:
                                                    UpdateQuestionPaper();
                                                    break;
                                                case 3:
                                                    DeleteQuestionPaper();
                                                    break;
                                                case 4:
                                                    ViewQuestionPaper();
                                                    break;

                                                case 5:
                                                    Environment.Exit(0);
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                                                    break;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Exception Raised : Incorrect Password Entered ..try again " + e);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid username or password. Please try again.\n");
                                    }
                                    break;
                                case "3":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("                                                        LOGIN PAGE\n");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("           \t\t\t ENTER USER_NAME :");
                                    username = Console.ReadLine();
                                    Console.Write("            \t\t\tENTER PASSWORD  :");
                                    password = Console.ReadLine();

                                    HOI hoi = hoiList.Find(h => h.Username == username && h.Password == password);

                                    if (hoi != null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("                                                        LOGIN SUCCESFUL\n");
                                        Thread.Sleep(2000);
                                        Console.Clear();
                                        try
                                        {

                                            Console.WriteLine("1. VIEW ALL QUESTION PAPERS");
                                            Console.WriteLine("2. UPDATE QUESTION PAPER");
                                            Console.WriteLine("3. DELETE QUESTION PAPER");
                                            Console.WriteLine("4.VIEW QUESTION PAPER ");
                                            Console.WriteLine("5. EXIT");
                                            Console.Write("ENTER YOUR CHOICE : ");

                                            int QUE;
                                            while (!int.TryParse(Console.ReadLine(), out QUE))
                                            {
                                                Console.WriteLine("Invalid input. Please enter a number.");

                                            }
                                            switch (QUE)
                                            {
                                                case 1:
                                                    ViewQuestionPapers();
                                                    break;
                                                case 2:
                                                    UpdateQuestionPaper();
                                                    break;
                                                case 3:
                                                    DeleteQuestionPaper();
                                                    break;
                                                case 4:
                                                    ViewQuestionPaper();
                                                    break;
                                                case 5:
                                                    Environment.Exit(0);
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                                                    break;
                                            }


                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Exception Raised : Incorrect Password Entered ..try again " + e);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("INVALID USERNAME OR PASSWORD TRY AGAIN\n");
                                        Thread.Sleep(3000);
                                        Console.Clear();
                                    }
                                    break;
                                case "4":
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\t\t\t\t\t  COE LOGIN PORTAL ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\tENTER USERNAME :");
                                    username = Console.ReadLine();
                                    Console.Write("\tENTER PASSWORD :");
                                    password = Console.ReadLine();

                                    COE coe = coeList.Find(c => c.Username == username && c.Password == password);
                                    Thread.Sleep(3000);
                                    Console.Clear();

                                    if (coe != null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("LOGIN SUCCESFUL!!\n");
                                        Thread.Sleep(3000);
                                        Console.Clear();
                                        try
                                        {

                                            Console.WriteLine("1. VIEW ALL QUESTION PAPERS");
                                            Console.WriteLine("2. UPDATE QUESTION PAPER");
                                            Console.WriteLine("3. DELETE QUESTION PAPER");
                                            Console.WriteLine("4. VIEW QUESTION PAPER ");
                                            Console.WriteLine("5. EXIT");
                                            Console.Write("\nENTER YOUR CHOICE : ");

                                            int QUE;
                                            while (!int.TryParse(Console.ReadLine(), out QUE))
                                            {
                                                Console.WriteLine("Invalid input. Please enter a number.");
                                            }

                                            switch (QUE)
                                            {
                                                case 1:
                                                    ViewQuestionPapers();
                                                    break;
                                                case 2:
                                                    UpdateQuestionPaper();
                                                    break;
                                                case 3:
                                                    DeleteQuestionPaper();
                                                    break;
                                                case 4:
                                                    ViewQuestionPaper();
                                                    break;
                                                case 5:
                                                    Environment.Exit(0);
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                                                    break;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Exception Raised : Incorrect Password Entered ..try again " + e);
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid username or password. Please try again.\n");
                                    }
                                    //--------------------------------------------------------

                                    break;
                                case "5":

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("                                                          STUDENT LOGIN ");
                                    Console.WriteLine("                                                       ====================");

                                    Console.Write(" ");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write("\tENTER USER_NAME : ");
                                    username = Console.ReadLine().Trim();
                                    while (string.IsNullOrEmpty(username))
                                    {
                                        Console.WriteLine("USER NAME CANNOT BE EMPTY,PLEASE ENTER A USER NAME :");
                                        username = Console.ReadLine().Trim();
                                    }

                                    Console.Write("\tENTER PASSWORD  : ");
                                    password = Console.ReadLine();
                                    while (string.IsNullOrEmpty(password))
                                    {
                                        Console.WriteLine("PASSWORD CANNOT BE EMPTY,PLEASE ENTER A PASSWORD   :");
                                        password = Console.ReadLine();
                                    }

                                    Student student = students.Find(s => s.Username == username && s.Password == password);
                                    Console.Clear();

                                    if (student != null)
                                    {
                                        Console.WriteLine("LOGGED SUCCESSSFULLY !\n");
                                        try
                                        {
                                            Console.WriteLine("1.WRITE EXAM\n2.EXIT");
                                            Console.Write("Enter your choice: ");
                                            int QUE;
                                            while (!int.TryParse(Console.ReadLine(), out QUE))
                                            {
                                                Console.WriteLine("Invalid input. Please enter a number.");
                                            }
                                            switch (QUE)
                                            {
                                                case 1:
                                                    QuestionPaperManager.ViewQuestionPapers();
                                                    Console.WriteLine("Enter the ID of the question paper you want to attempt:");
                                                    int selectedPaperId;
                                                    while (!int.TryParse(Console.ReadLine(), out selectedPaperId))
                                                    {
                                                        Console.WriteLine("Invalid input. Please enter a valid ID.");
                                                    }

                                                    var selectedPaper = QuestionPaperManager.questionPapers.FirstOrDefault(p => p.Id == (selectedPaperId+1));

                                                    if (selectedPaper != null)
                                                    {
                                                        Console.WriteLine("START ANSWERING FOR THE ABOVE QUESTIONS : ");

                                                        int correctAnswersCount = 0;
                                                        int totalQuestions = selectedPaper.Questions.Count;

                                                        for (int i = 0; i < totalQuestions; i++)
                                                        {
                                                            var question = selectedPaper.Questions[i];

                                                            Console.WriteLine($"{i + 1}. {question.Text}");

                                                            for (int j = 0; j < question.Options.Count; j++)
                                                            {
                                                                Console.WriteLine($"{j + 1}. {question.Options[j]}");
                                                            }

                                                            Console.Write("ENTER YOUR ANSWER (1/2/3/4): ");
                                                            int selectedOption;
                                                            while (!int.TryParse(Console.ReadLine(), out selectedOption) || selectedOption < 1 || selectedOption > 4)
                                                            {
                                                                Console.WriteLine("INVALID INPUT. PLEASE ENTER A NUMBER BETWEEN 1 AND 4.");
                                                            }

                                                            string selectedAnswer = question.Options[selectedOption - 1];
                                                            // Assuming you have a student object available to store answers.
                                                            student.Answers.Add(new Answer { Question = question.Text, ChosenOption = selectedAnswer });

                                                            if (selectedAnswer == question.CorrectAnswer)
                                                            {
                                                                correctAnswersCount++;
                                                            }
                                                        }

                                                        // Compute and display result
                                                        double percentage = ((double)correctAnswersCount / totalQuestions) * 100;
                                                        Console.WriteLine($"\nTOTAL QUESTIONS : {totalQuestions}");
                                                        Console.WriteLine($"CORRECT ANSWERS   : {correctAnswersCount}");
                                                        Console.WriteLine($"INCORRECT ANSWERS : {totalQuestions - correctAnswersCount}");
                                                        Console.WriteLine($"PERCENTAGE        : {percentage}%");

                                                        if (percentage >= 60)
                                                        {
                                                            Console.WriteLine("CONGRATULATIONS BUDDY YOU ARE PASSED, IN CIET! ");
                                                        }
                                                        else
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("SORRY BUDDY , YOU R FAILED (-_-) U R MONEY GOES TO COLLEGE PATCH WORK.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("QUESTION PAPER NOT FOUND .\n");
                                                    }

                                                    break;

                                                case 2:
                                                    Environment.Exit(0);
                                                    break;
                                                default:
                                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                                                    break;
                                            }


                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Exception Raised : Incorrect Password Entered ..try again " + e);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\t\t\t\tINVALID USERNAME ...PLEASE TRY AGAIN #\n");
                                        Thread.Sleep(3000);
                                        Console.Clear();
                                    }
                                    break;
                                case "6":
                                    Console.WriteLine("\t\t\t\t\tRETURNING TO MENU ");
                                    return;
                                default:
                                    break;
                            }
                        }
                        
                        break;
                    case "6":
                        Console.WriteLine("\t\t\t\t\tRETURNING TO MENU");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void StudentMenu()

        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                                    WELCOME TO STUDENT PORTAL ");
            Console.WriteLine("                                       ---------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\t\t1. REGISTER\n\t\t\t\t\t\t2. LOGIN\n\t\t\t\t\t\t3. EXIT");
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("                 ENTER YOUR CHOICE : ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    RegisterStudent();
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case "2":
                    StudentLogin();

                    break;
                case "3":
                    Console.WriteLine("RETURNING TO MENU.....");
                    Thread.Sleep(3000);

                    Console.Clear();

                    return;
                default:
                    Console.WriteLine("INVALID CHOICE !! PLEASE TRY AGAIN LATER");
                    break;
            }
        }
        static void RegisterStudent()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("                                                 STUDENT REGISTRATION PAGE                      ");
            Console.WriteLine("                        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            string name, username, password, phoneNumber, email, gender;
            DateTime dateOfBirth;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("");
            Console.Write("\tENTER YOUR NAME                              : ");
            name = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("NAME CANNOT BE , EMPTY PLEASE ENTER YOUR NAME !");
                name = Console.ReadLine().Trim();
            }

            Console.Write("\tENTER USER_NAME                              : ");
            username = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("USER_NAME CANNOT BE EMPTY, PLEASE EMTER A USER NAME :");
                username = Console.ReadLine().Trim();
            }

            Console.Write("ENTER PASSWORD (MIN 6 CHARACTERS)                    : ");
            password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                Console.Write("PASSWORD MUST BE 6 CHARACTERS LONG, PLEASE ENTER A VALID PASSWORD:");
                password = Console.ReadLine();
            }

            Console.Write("ENTER PHONE NUMBER                                   : ");
            phoneNumber = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                Console.Write("INVALID PHONE NUMBER FORMAT, PLEASE ENTER A 10 -DIGIT PHONE NUMBER:");
                phoneNumber = Console.ReadLine().Trim();
            }

            Console.Write("ENTER YOUR E-MAIL                                    : ");
            email = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"))
            {
                Console.Write("Invalid email format. Please enter a valid email:");
                email = Console.ReadLine().Trim();
            }

            Console.Write("ENTER GENDER                                         : ");
            gender = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(gender))
            {
                Console.WriteLine("GENDER CANNOT BE EMPTY PLEASE ,ENTER YOUR GENDER :");
                gender = Console.ReadLine().Trim();
            }

            Console.Write("ENTER DATE OF BIRTH (yyyy-MM-dd)                     : ");
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                Console.WriteLine("Invalid date format. Please enter the date of birth in yyyy-MM-dd format:");
            }

            students.Add(new Student(name, username, password, phoneNumber, email, gender, dateOfBirth));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("STUDENT REGISTRATION SUCCESSFUL, REDIRECTING  TO MENU !\n");
        }
        static void StudentLogin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                          STUDENT LOGIN ");
            Console.WriteLine("                                                       ====================");

            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\tENTER USER_NAME : ");
            string username = Console.ReadLine().Trim();
            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("USER NAME CANNOT BE EMPTY,PLEASE ENTER A USER NAME :");
                username = Console.ReadLine().Trim();
            }

            Console.Write("\tENTER PASSWORD  : ");
            string password = Console.ReadLine();
            while (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("PASSWORD CANNOT BE EMPTY,PLEASE ENTER A PASSWORD   :");
                password = Console.ReadLine();
            }

            Student student = students.Find(s => s.Username == username && s.Password == password);
            Console.Clear();

            if (student != null)
            {
                Console.WriteLine("LOGGED SUCCESSSFULLY !\n");
                StudentMenu(student);
            }
            else
            {
                Console.WriteLine("INVALID USERNAME ...PLEASE TRY AGAIN #\n");
                Thread.Sleep(3000);
                Console.Clear();
            }
        }
        static void StudentMenu(Student student)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("                                                          STUDENT MENU ");
                Console.WriteLine("──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────                               ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("");
                Console.WriteLine("\t1. REGISTER FOR EXAM \n\t2. VIEW PROFILE\n\t3. UPDATE DETAILS \n\t4. DELETE STUDENT \n\t5. EXIT");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("");
                Console.Write("\t\tENTER YOUR CHOICE : ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("HURRAY REGISTERED FOR EXAM !");
                        break;
                    case "2":
                        student.DisplayProfile();
                        break;
                    case "3":
                        UpdateStudentProfile();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        Console.WriteLine("                                             RETURNING TO MENU .......");
                        return;
                    default:
                        Console.WriteLine("INVALID CHOICE , PLEASE TRY AGAIN");
                        break;
                }
            }
        }
        static void HOIMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                                     WELCOME TO HEAD OF INSTITUTION PORTAL !");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────                               ");
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("");
            Console.WriteLine("\t1. REGISTER AS PRINCIPAL?\n\t2. LOGIN\n\t3. EXIT");
            Console.Write("\t\nENTER YOUR CHOICE : ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    RegisterHOI();
                    break;
                case "2":
                    LoginHOI();
                    break;
                case "3":
                    Console.WriteLine("RETURNING TO MENU ...");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("INVALID CHOICE PLEASE TRY AGAIN.....");
                    break;
            }
        }
        static void RegisterHOI()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("                                           HEAD OF INSTITUTION  REGISTRATION PAGE                      ");
            Console.WriteLine("                        ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" \tENTER NAME       :");
            string name = Console.ReadLine();
            Console.Write("\tENTER USER_NAME  :");
            string username = Console.ReadLine();
            Console.Write("\tENTER PASSWORD   :");
            string password = Console.ReadLine();
            Console.Write("\tENTER PH-NUMBER  :");
            string phoneNumber = Console.ReadLine();
            Console.Write("\tENTER E-MAIL     :");
            string email = Console.ReadLine();

            Console.Write("\tENTER DATE OF BIRTH (yyyy-MM-dd) :");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.WriteLine("Invalid date format. Please enter in yyyy-MM-dd format:");
            }

            hoiList.Add(new HOI(name, username, password, phoneNumber, email, dateOfBirth));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\tHOI REGISTRATION SUCCESFUL !\n");
            Thread.Sleep(3000); Console.Clear();
        }
        static void LoginHOI()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                                     HOI  LOGIN PAGE\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("           \t\t\t ENTER USER_NAME :");
            string username = Console.ReadLine();
            Console.Write("            \t\t\t ENTER PASSWORD :");
            string password = Console.ReadLine();

            HOI hoi = hoiList.Find(h => h.Username == username && h.Password == password);

            if (hoi != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                                                        LOGIN SUCCESFUL\n");
                Thread.Sleep(2000);
                Console.Clear();
                while (hoi != null)
                {
                    Console.WriteLine("\t\t\t\t    PRINCIPAL MENU    ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t1. VIEW PROFILE\n\t2. VIEW STUDENT LIST \n\t3. VIEW FACULTY LIST\n\t4. UPDATE PROFILE \n\t5. DELETE PROFILE \n\t6. EXIT ");
                    string h = Console.ReadLine();
                    Console.Clear();

                    switch (h)
                    {
                        case "1":
                            {
                                hoi.DisplayProfile();

                                break;
                            }

                        case "2":
                            if (students.Count == 0)
                            {
                                Console.WriteLine("NO STUDENTS REGISTERED");
                            }
                            else
                            {
                                ViewStudentList();
                            }


                            break;

                        case "3":

                            if (nonTeachingStaffList.Count == 0)
                            {
                                Console.WriteLine("NON-TEACHING STAFF NOT REGISTERED.");
                            }
                            else
                            {
                                ViewNonTeachingStaffList();
                            }
                            if (teachingStaffList.Count == 0)
                            {
                                Console.WriteLine("TEACHING STAFF NOT REGISTERD ");
                            }
                            else
                            {
                                ViewTeachingStaffList();
                            }

                            if (hodList.Count == 0)
                            {
                                Console.WriteLine("TEACHING STAFF NOT REGISTERD");
                            }
                            else
                            {
                                ViewHODList();
                            }
                            break;
                        case "4":
                            UpdateHOIProfile();
                            break;
                        case "5":
                            DeleteHOI();
                            break;

                        case "6":
                            Console.WriteLine("RETURNING TO MENU...");
                            Thread.Sleep(3000);
                            Console.Clear();
                            return;
                        default:
                            break;

                    }
                }
            }
            else
            {
                Console.WriteLine("INVALID USERNAME OR PASSWORD TRY AGAIN\n");
                Thread.Sleep(3000);
                Console.Clear();
            }
        }
        static void FacultyMenu()
        {

            Console.WriteLine("                               WELCOME TO YOUR FAVOURITE FACULTY BLOCK (CSE)");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────────────── ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\t1. TEACHING STAFF\n\t2. NON-TEACHING STAFF\n\t3. HEAD OF DEPARTMENT\n\t4. BACK TO MENU");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ENTER YOUR CHOICE : ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    TeachingStaffMenu();
                    break;
                case "2":
                    NonTeachingStaffMenu();
                    break;
                case "3":
                    HODMenu();
                    break;
                case "4":
                    Console.WriteLine("Returning to Main Menu...");
                    Thread.Sleep(3000);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(3000);
                    Console.Clear();
                    break;

            }
        }
        static void TeachingStaffMenu()
        {
            Console.WriteLine("\t\t\t\t\tWELCOME TO TEACHING STAFF PORTAL ");
            Console.WriteLine("                             ======================================================== ");
            Console.WriteLine("\t1. REGISTER\n\t2. LOGIN\n\t3. BACK");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("ENTER YOUR CHOICE : ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    RegisterTeachingStaff();
                    break;
                case "2":
                    TeachingStaffLogin();
                    break;
                case "3":
                    Console.WriteLine("RETURNING TO FACULTY MENU...");
                    Thread.Sleep(3000);
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(3000);
                    Console.Clear();
                    break;
            }
        }
        static void RegisterTeachingStaff()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\tREGISTERING FOR TEACHING STAFFFF!");
            Console.WriteLine("\t\t\t\t   ========================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ENTER NAME      :");
            string name = Console.ReadLine();
            Console.Write("ENTER USER_NAME :");
            string username = Console.ReadLine();
            Console.Write("ENTER PASSWORD  :");
            string password = Console.ReadLine();
            Console.Write("ENTER PH-NUMBER :");
            string phoneNumber = Console.ReadLine();
            Console.Write("ENTER E-MAIL    :");
            string email = Console.ReadLine();
            Console.Write("ENTER GENDER    :");
            string gender = Console.ReadLine();
            Console.Write("ENTER DATE OF BIRTH (yyyy-MM-dd):");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.WriteLine("Invalid date format. Please enter in yyyy-MM-dd format:");
                Thread.Sleep(2000);
            }

            teachingStaffList.Add(new TeachingStaff(name, username, password, phoneNumber, email, gender, dateOfBirth));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\tTEACHING STAFF REGISTRATION SUCCESFUL!\n");
        }
        static void TeachingStaffLogin()
        {
            Console.WriteLine("\t\t\t\t\tTEACHING STAFF LOGIN \n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\tENTER USER_NAME:");
            string username = Console.ReadLine();
            Console.Write("\tENTER PASSWORD :");
            string password = Console.ReadLine();

            TeachingStaff teachingStaff = teachingStaffList.Find(ts => ts.Username == username && ts.Password == password);

            if (teachingStaff != null)
            {
                Console.WriteLine("\t\t\t\t\tLOGIN SUCCESFUL!\n");
                Thread.Sleep(2000);
                Console.Clear();
                TeachingStaffMenu(teachingStaff);
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.\n");
            }
        }
        static void TeachingStaffMenu(TeachingStaff teachingStaff)
        {
            while (true)
            {

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t\t\t\t\tTEACHING STAFF MENU :");
                Console.WriteLine("\t");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t1. VIEW STUDENTS LIST\n\t2. VIEW PROFILE\n\t3. UPDATE PROFILE\n\t4. DELETE PROFILE\n\t5. EXIT");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ENTER YOUR CHOICE : ");
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        if (students.Count == 0)
                        {
                            Console.WriteLine("STUDENTS NOT REGISTERED YET !");
                        }
                        else
                        {
                            ViewStudentList();
                        }
                        break;
                    case "2":
                        teachingStaff.DisplayProfile();
                        break;
                    case "3":
                        UpdateTeachingStaffProfile();
                        break;
                    case "4":
                        DeleteTeachingStaff();
                        break;
                    case "5":
                        Console.WriteLine("RETURNING TO MENU ....");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void NonTeachingStaffMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t\t\tWELCOME TO NON-TEACHING FACULTY REGISTRATION PORTAL");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────────────────────── ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t1. REGISTER\n\t2. LOGIN\n\t3. BACK");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("ENTER YOUR CHOICE : ");
            string choice = Console.ReadLine();
            Thread.Sleep(1000);
            Console.Clear();

            switch (choice)
            {
                case "1":
                    RegisterNonTeachingStaff();
                    break;
                case "2":
                    NonTeachingStaffLogin();
                    break;
                case "3":
                    Console.WriteLine("RETURNING TO MENU ...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        static void RegisterNonTeachingStaff()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t\t\t\t\t NON-TEACHING FACULTY REGISTRATION PORTAL");
            Console.Write(
