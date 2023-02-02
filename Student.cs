using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APBD2
{
    [Serializable]
    internal class University
    {
        public University(string createdAt, string author, List<Student> students, List<ActiveStudy> activeStudies)
        {
            this.activeStudies = activeStudies;
            this.createdAt = createdAt;
            this.author = author;
            this.students = students;
        }
        [JsonPropertyName("createdAt")]
        public string createdAt { get; set; }
        [JsonPropertyName("author")]
        public string author{ get; set; }
        [JsonPropertyName("students")]
        public List<Student> students{ get; set; }
        [JsonPropertyName("activeStudies")]
        public List<ActiveStudy> activeStudies{ get; set; }
    }
    [Serializable]
    internal class ActiveStudy
    {
        public ActiveStudy(string name, string numberOfStudents) {
            this.name = name;
            this.numberOfStudents = numberOfStudents;
        }
        public string name { get; set; }
        public string numberOfStudents { get; set; }

    }
    [Serializable]
    internal class Student
    {
        public string fname { get; set; }
        public string lname{ get; set; }
        public string indexNumber { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies { get; set; }

        public Student(string fname, string lname, string studyName, string studyMode, string indexNumber, string birthdate, string email, string mothersName, string fathersName)
        {
            this.fname = fname;
            this.lname = lname;
            this.indexNumber = indexNumber;
            this.birthdate = birthdate;
            this.email = email;
            this.mothersName = mothersName;
            this.fathersName = fathersName;
            this.studies = new Studies(studyName, studyMode);
        }
    }
    [Serializable]
    internal class Studies
    {
        public Studies(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public string name { get; set; }
        public string mode { get; set; }
    }
}
