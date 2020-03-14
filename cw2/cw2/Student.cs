using System;
using System.Xml.Serialization;

namespace cw2
{
    [Serializable]
    public class Student
    {
        public Student(string fname, string lname, string indexNumber, string birthdate, string email, string motherName, string fatherName, Studia studia)
        {
            this.fname = fname;
            this.lname = lname;
            this.indexNumber = indexNumber;
            this.birthdate = birthdate;
            this.email = email;
            this.motherName = motherName;
            this.fatherName = fatherName;
            this.studia = studia;
        }

        public Student()
        {

        }

        public string fname { get; set; }
        public string lname { get; set; }

        [XmlAttribute(AttributeName = "IndexNumber")]
        public string indexNumber { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string motherName { get; set; }
        public string fatherName { get; set; }
        public Studia studia { get; set; }

        

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (typeof(Student).IsInstanceOfType(obj))
            {
                Student student = (Student)obj;
                return (student.fname == this.fname) && (student.lname == this.lname) && (student.indexNumber == this.indexNumber);
            }
            return false;
        }
    }
}