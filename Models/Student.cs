using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.Data.SqlClient;

namespace LucirWeb_MVC.Models
{
    public class Student
    {

        public int StudentID { get; set; }
        [DisplayName("Last Name : ")]
        [StringLength(50, MinimumLength = 1,
    ErrorMessage = "Last name must be 1 to 50 Characters only")]
        public string Lastname { get; set; }
        [DisplayName("First Name : ")]
        [StringLength(50, MinimumLength = 1,
ErrorMessage = "First name must be 1 to 50 Characters only")]
        public string Firstname { get; set; }
        public int OldSid { get; set; }
        public string Name { get; set; }

        public string n = "";

        private DAL dao = new DAL(Cs.myserver);

        public void Add()
        {
            this.V();
            dao.Set("INSERT INTO Students VALUES " + "(@ar,@al)");
            dao.Par("@ar", this.Lastname);
            dao.Par("@al", this.Firstname);
            dao.Exe(true);
        }

        public void HistoryDb()
        {
            dao.Set("INSERT INTO S VALUES " + "(@ala)");
            dao.Par("@ala", this.Name);
            n = this.Name;
            dao.Exe(true);
        }

        public void Delete()
        {
            dao.Set("DELETE Students WHERE StudentID = @sd");
            dao.Par("@sd", this.StudentID);
            dao.Exe(true);
        }

        public void Delete(int id)
        {
            this.StudentID = id;
            this.Delete();
        }


        public List<Student> GetStudentWithName()
        {
            List<Student> l = new();
            dao.Open();
            dao.Set("SELECT * FROM Students WHERE Lastname = @ln OR Firstname = @fn");
            dao.Par("@ln", n);
            dao.Par("@fn", n);
            SqlDataReader q = dao.StartRead();
            for (; q.Read() == true;)
            {
                Student s = new();
                s.StudentID = (int)q[0];
                s.Lastname = (string)q[1];
                s.Firstname = (string)q[2];

                l.Add(s);
            }

            dao.EndReader();
            dao.Close();
            return l;
        }

        public Student GetStudent(int id)
        {
            Student xs = new();
            dao.Open();
            dao.Set("SELECT * FROM Students WHERE StudentID = @i");
            dao.Par("i", id);
            SqlDataReader x = dao.StartRead();
            if (x.Read() == true)
            {
                xs.StudentID = (int)x[0];
                xs.Lastname = (string)x[1];
                xs.Firstname = (string)x[2];
            }

            dao.EndReader();
            dao.Close();
            return xs;
        }
        public void Update(int id)
        {
            this.V();
            dao.Set("UPDATE Students SET Lastname = @ns," +
                "Firstname = @na WHERE StudentID = @s");
            dao.Par("@ns", this.Lastname);
            dao.Par("@na", this.Firstname);
            dao.Par("@s", id);
            dao.Exe(true);
        }



        public List<Student> GetAllStudents()
        {
            List<Student> l = new List<Student>();
            dao.Open();
            dao.Set("SELECT * FROM Students");
            SqlDataReader q = dao.StartRead();
            for (; q.Read() == true;)
            {
                Student s = new();
                s.StudentID = (int)q[0];
                s.Lastname = (string)q[1];
                s.Firstname = (string)q[2];

                l.Add(s);
            }
            dao.EndReader();
            dao.Close();
            return l;
        }

        public void V()
        {
            if (this.Lastname == null)
            {
                Exception ex = new Exception(
                    "Lastname must be 1 to 50 Characters only");
                ex.Source = "Student Class";
                throw ex;
            }
            if (this.Firstname == null)
            {
                Exception ex = new Exception(
                    "Firstname must be 1 to 50 Characters only");
                ex.Source = "Student Class";
                throw ex;
            }

        }
    }
}
