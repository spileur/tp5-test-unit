using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using static  ParseMyCSV.ParseMyCSV;

namespace ParseMyCSV
{
    
    public class Global
    {
        public static readonly string PATH_TESTS_FILES = "../../../TestsFiles/";
    }

    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class AssignHouse
    {

        private Student _createStudent(string Name)
        {
            Student student = new Student(Name, Houses.None, 0);
            AssignHouse(student);
            return student;
        }
        
        [Test]
        public void Harry_Potter()
        {
            Assert.AreEqual(Houses.Gryffindor, _createStudent("Harry Potter").Houses);
        }
        
        [Test]
        public void Ernie_Macmillan()
        {
            Assert.AreEqual(Houses.Hufflepuff, _createStudent("Ernie Macmillan").Houses);
        }
        
        [Test]
        public void Baron_Sanglant()
        {
            Houses houses = _createStudent("Baron Sanglant").Houses;
            Assert.AreNotEqual(Houses.None, houses, "the student doesn't have a house");
            int i = 30;
            while (i > 0 && houses == _createStudent("Baron Sanglant").Houses)
                i--;
            Assert.AreNotEqual(0, i, "the student always has the same house");
        }
    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class CreateStudentsInfoFromFormat
    {

        private readonly string TEST_FILE = Global.PATH_TESTS_FILES + "CreateStudentsInfoFromFormatFile";
        private readonly string EMPTY_FILE = Global.PATH_TESTS_FILES + "EmptyFile";
        private readonly string NOT_FOUND_FILE = Global.PATH_TESTS_FILES + "EmptyFile";

        [Test]
        public void Harry_Potter()
        {
            Assert.AreEqual(new Student("Harry Potter", (Houses) 1, 0), 
                CreateStudentsInfoFromFormat(TEST_FILE)["Harry Potter"],
                "Harry Potter isn't present in the Dictionary or doesn't have the right attributes");
        }
        
        [Test]
        public void Ernie_Macmillan()
        {
            Assert.AreEqual(new Student("Ernie Macmillan", (Houses) 2, 0), 
                CreateStudentsInfoFromFormat(TEST_FILE)["Ernie Macmillan"],
                "Ernie Macmillan isn't present in the Dictionary or doesn't have the right attributes");
        }
        
        [Test]
        public void Draco_Malfoy()
        {
            Student student = CreateStudentsInfoFromFormat(TEST_FILE)["Draco Malfoy"];
            Assert.NotNull(student, "Draco Malfoy isn't present in the Dictionary");
            Assert.AreNotEqual(Houses.None, student.Houses, "Draco Malfoy doesn't have house");
        }
        
        [Test]
        public void Empty_File()
        {
            Assert.IsNull(CreateStudentsInfoFromFormat(EMPTY_FILE),
                "The function doesn't review null");
        }
        
        [Test]
        public void Not_Found_File()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            CreateStudentsInfoFromFormat(NOT_FOUND_FILE);
            Assert.AreEqual("Error: \"NotFound\" does not exist" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the error message");
        }
        
    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class SaveStudents
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "CreateStudentsInfoFromFormatFile";
        private readonly string TEST_FILE = Global.PATH_TESTS_FILES + "SaveStudentsFile";

        private Dictionary<string, Student> _createDictionary()
        {
            Dictionary<string, Student> baseObject = CreateStudentsInfoFromFormat(STUDENTS_FILE);
            SaveStudents(baseObject, TEST_FILE);
            Dictionary<string, Student> newObject = CreateStudentsInfoFromFormat(TEST_FILE);
            File.Delete(TEST_FILE);
            return newObject;
        }
        
        [Test]
        public void Harry_Potter()
        {
            Assert.AreEqual(new Student("Harry Potter", (Houses) 1, 0), 
                _createDictionary()["Harry Potter"],
                "Harry Potter isn't present in the Dictionary or doesn't have the right attributes");
        }
        
        [Test]
        public void Ernie_Macmillan()
        {
            Assert.AreEqual(new Student("Ernie Macmillan", (Houses) 2, 0), 
                _createDictionary()["Ernie Macmillan"],
                "Ernie Macmillan isn't present in the Dictionary or doesn't have the right attributes");
        }
        
        [Test]
        public void Draco_Malfoy()
        {
            Student student = _createDictionary()["Draco Malfoy"];
            Assert.NotNull(student, "Draco Malfoy isn't present in the Dictionary");
            Assert.AreNotEqual(Houses.None, student.Houses, "Draco Malfoy doesn't have house");
        }
        
    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class AddPoints
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "CreateStudentsInfoFromFormatFile";

        private Dictionary<string, Student> _createDictionary()
        {
            Dictionary<string, Student> baseObject = CreateStudentsInfoFromFormat(STUDENTS_FILE);
            return baseObject;
        }
        
        [Test]
        public void Harry_Potter()
        {
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "Harry Potter", 100);
            Assert.AreEqual(new Student("Harry Potter", (Houses) 1, 100),
                students["Harry Potter"],
                "Harry Potter doesn't have the right points");
        }
        
        [Test]
        public void Ernie_Macmillan()
        {
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "Ernie Macmillan", 950);
            AddPoints(students, "Ernie Macmillan", 50);
            Assert.AreEqual(new Student("Ernie Macmillan", (Houses) 2, 1000),
                students["Ernie Macmillan"],
                "Ernie Macmillan doesn't have the right points");
        }
        
        [Test]
        public void NotAStudent()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "NotAStudent", 9000);
            Assert.AreEqual("Error: \"NotAStudent\" does not exist" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the error message");
        }
        
    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class GivePoints
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "CreateStudentsInfoFromFormatFile";

        private Dictionary<string, Student> _createDictionary()
        {
            Dictionary<string, Student> baseObject = CreateStudentsInfoFromFormat(STUDENTS_FILE);
            return baseObject;
        }
        
        [Test]
        public void Harry_Gang()
        {
            Dictionary<string, Student> students = _createDictionary();
            GivePoints(students);
            Assert.IsTrue(students["Harry Potter"].Points >= 99999 && students["Harry Potter"].Points < 99999 + 100,
                "Harry Potter doesn't have the right points");
            Assert.IsTrue(students["Hermione Granger"].Points >= 99999 + 13 && students["Hermione Granger"].Points < 99999 + 13 + 100,
                "Hermione Granger doesn't have the right points");
        }
        
        [Test]
        public void Ernie_Macmillan()
        {
            Dictionary<string, Student> students = _createDictionary();
            GivePoints(students);
            GivePoints(students);
            Assert.IsTrue(students["Ernie Macmillan"].Points >= 0 && students["Ernie Macmillan"].Points < 199,
                "Ernie Macmillan doesn't have the right points");
        }

    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class WinnerOfTheHouseCup
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "CreateStudentsInfoFromFormatFile";

        private Dictionary<string, Student> _createDictionary()
        {
            Dictionary<string, Student> baseObject = CreateStudentsInfoFromFormat(STUDENTS_FILE);
            return baseObject;
        }
        
        [Test]
        public void Gryffindor()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "Harry Potter", 999);
            Assert.AreEqual("Winner of the House Cup is : Gryffindor" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the correct winner");
        }
        
        [Test]
        public void Gryffindor_equals_Hufflepuff()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "Ernie Macmillan", 13);
            Assert.AreEqual("Winner of the House Cup is : Gryffindor" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the correct winner");
        }
        
        [Test]
        public void Hufflepuff()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            Dictionary<string, Student> students = _createDictionary();
            AddPoints(students, "Ernie Macmillan", 750);
            Assert.AreEqual("Winner of the House Cup is : Hufflepuff" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the correct winner");
        }

        public void Save_File()
        {
            using var output = new ConsoleTest.ConsoleOutput();
            Dictionary<string, Student> students = _createDictionary();
            GivePoints(students);
            Assert.AreEqual("Winner of the House Cup is : Gryffindor" + Environment.NewLine, output.GetOutput(),
                "The console doesn't display the correct winner");
            //TODO Check file
        }

    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class ListofHouses
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "StudentsList";
        private readonly string TEST_FILE = Global.PATH_TESTS_FILES + "ListofHousesFile";

        private string[] _saveListOfHouses(Dictionary<string, Student> students)
        {
            ListofHouses(students, TEST_FILE);
            string[] file = File.ReadAllLines(TEST_FILE);
            File.Delete(TEST_FILE);
            return file;
        }
        
        [Test]
        public void Gryffindor()
        {
            Assert.AreEqual("House Gryffindor :", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[0],
                "House Gryffindor isn't present in file");
            Assert.AreEqual("Harry Potter", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[1],
                "Harry Potter isn't present in House Gryffindor");
            Assert.AreEqual("Hermione Granger", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[2],
                "Hermione Granger isn't present in House Gryffindor");
            Assert.AreEqual("Ron Weasley", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[3],
                "Ron Weasley isn't present in House Gryffindor");
        }
        
        [Test]
        public void Space_Between_Houses()
        {
            Assert.AreEqual("", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[4],
                "There are no spaces between the houses");
            Assert.AreEqual("", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[10],
                "There are no spaces between the houses");
        }
        
        [Test]
        public void Ravenclaw()
        {
            Assert.AreEqual("House Ravenclaw :", 
                _saveListOfHouses(CreateStudentsInfoFromFormat(STUDENTS_FILE))[9],
                "House Ravenclaw isn't present in file");
        }

    }
    
    [TestFixture, Timeout(1500), Author("Valentin UHLRICH", "valentin.uhlrich@epita.fr")]
    public class Update
    {

        private readonly string STUDENTS_FILE = Global.PATH_TESTS_FILES + "StudentsList";
        private readonly string UPDATE_FILE = Global.PATH_TESTS_FILES + "UpdateFile";

        private Dictionary<string, Student> _execUpdate()
        {
            Dictionary<string, Student> students = CreateStudentsInfoFromFormat(STUDENTS_FILE);
            Update(students, UPDATE_FILE);
            return students;
        }
        
        [Test]
        public void Rename_Student()
        {
            Assert.AreEqual(new Student("Lucius Malfoy", (Houses) 4, 56), 
                _execUpdate()["Lucius Malfoy"],
                "Lucius Malfoy cannot be found");
        }
        
        [Test]
        public void Change_House()
        {
            Assert.AreEqual(new Student("Harry Potter", (Houses) 4, 100068), 
                _execUpdate()["Harry Potter"],
                "Harry Potter has the wrong house");
        }
        
        [Test]
        public void Add_Student()
        {
            Assert.AreEqual(new Student("Mimi Geignarde", (Houses) 3, 0), 
                _execUpdate()["Mimi Geignarde"],
                "Mimi Geignarde cannot be found");
        }
        
        [Test]
        public void Remove_Student()
        {
            Assert.IsFalse(_execUpdate().ContainsKey("Ron Weasley"),
                "Ron Weasley wasn't removed");
        }

    }
}