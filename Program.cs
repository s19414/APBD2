using APBD2;
using System.IO;
using System.Text.Json;

namespace ConsoleApp
{
    public class Program
    {
        const string ERRORLOGPATH = "log.txt";
        public static void Main(string[] args)
        {
            string pathArg = "data.csv";
            string destArg = "result.json";
            string formatArg = "json";
            //overwrite error log after every run(this is just personal taste) and log system time
            File.WriteAllText(ERRORLOGPATH, "Run time: " + DateTime.Now.ToString());

            //if only 1 param is passed, its treated as pathArg
            if (args.Length == 1 ) {
                pathArg = args[0];    
                //if only 2 params are passed, they're treated as pathArg and destArg
                if (args.Length == 2 )
                {
                    destArg = args[1];
                    if(args.Length == 3)
                    {
                        formatArg = args[2];
                    }
                }
            }
            //Error handling - incorrect file path
            if (!Uri.TryCreate(pathArg, UriKind.RelativeOrAbsolute, out _))
            {
                File.AppendAllText(ERRORLOGPATH, "Argument Exception (\"The path is incorrect\")");
                throw new ArgumentException("The path is incorrect");
            }
            //Error handling - file doesn't exist
            if(!File.Exists(pathArg)) {
                File.AppendAllText(ERRORLOGPATH, "FileNotFoundException(\"File does not exist\")");
                throw new FileNotFoundException("File does not exist");
            }

            List<Student> validStudents = new List<Student>();
            List<ActiveStudy> activeStudies = new List<ActiveStudy>();
            //create raw list of students from csv., ommiting those with empty cells
            foreach (string row in File.ReadLines(pathArg))
            {
                //ROW STRUCTURE
                //fname, lname, studyName, studyMode, indexNumber, birthdate, email, motherName, fathersName;
                bool incompleteRow = false;
                string[] splitRow = row.Split(',');
                foreach (string cell in splitRow)
                {
                    if (cell.Equals(""))
                    {
                        incompleteRow = true;
                    }
                }
                if (!incompleteRow)
                {
                    Student student = new Student(splitRow[0], splitRow[1], splitRow[2], splitRow[3], splitRow[4], splitRow[5]
                        , splitRow[6], splitRow[7], splitRow[8]);
                    //ignore students with duplicate <fname, lname, indexNumber>
                    if (!studentIsDuplicate(student, validStudents))
                    {
                        validStudents.Add(student);
                        //populate activeStudies
                        bool studyExists = false;
                        foreach (ActiveStudy activeStudy in activeStudies)
                        {
                            //if study exists, increment numberOfStudents
                            if (activeStudy.name.Equals(student.studies.name))
                            {
                                int newNumberOfStudents = int.Parse(activeStudy.numberOfStudents) + 1;
                                activeStudy.numberOfStudents = newNumberOfStudents.ToString();
                                studyExists = true;
                            }
                        }
                        //if study doesn't exist, make new study entry
                        if (!studyExists)
                        {
                            activeStudies.Add(new ActiveStudy(student.studies.name, "1"));
                        }
                    }
                }
                else
                {
                    IncompleteRecordError error = new IncompleteRecordError("Incomplete record: " + row);
                    File.AppendAllText(ERRORLOGPATH, "\n" + error.ToString());
                }

                
            }
            //fill out University class
            University university = new University(DateTime.Now.ToString(), "Thomas Yeow", validStudents, activeStudies);
            
            //create active studies class
            string jsonText = JsonSerializer.Serialize(university);
            Console.Write(jsonText);
            File.WriteAllText(destArg, jsonText);
            
        }

        //Helper function for checking if student is a duplicate in this list
        private static bool studentIsDuplicate(Student student, List<Student> studentList) {
            foreach (Student validStudent in studentList)
            {
                if (validStudent.fname.Equals(student.fname) && validStudent.lname.Equals(student.lname) 
                    &&validStudent.indexNumber.Equals(student.indexNumber)) {
                    return true;
                }
            }
            return false;
        }
    }
}