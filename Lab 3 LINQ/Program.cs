/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Names: Nancy Torres, Kaitryn Fredeluces                                             *
 * CECS 475                                                                            *
 * Lab 4 Part 3                                                                        *
 * 03/05/15                                                                            * 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CECS475Part3
{
    class Program
    {
        static void Main(string[] args)
        {
            //CREATE in One-to-One
            // create new student entity object
            var student = new Student();
            // Assign student name
            student.StudentName = "New Student1";
            // Create new StudentAddress entity and assign it to student entity
            student.StudentAddress = new StudentAddress() { Address1 =
            "Student1's Address1",
             Address2 = "Student1's Address2", City =
            "Student1's City",
            State = "Student1's State" };
            //create DBContext object
            using (var dbCtx = new SchoolEntities())
            {
                //Add student object into Student's EntitySet
                dbCtx.Students.Add(student);
                // call SaveChanges method to save student & StudentAddress into database
                dbCtx.SaveChanges();

                               
                var categories = from c in dbCtx.StudentAddresses
                                 select c;
                Console.WriteLine("Create in One-to-One (New Student1):");
                foreach( var category in categories)
                {
                    Console.WriteLine("Name:" + category.Student.StudentName + " Address1:" + category.Address1 + " Address2:" + category.Address2+" City:"+category.City);
                }
                Console.WriteLine();
                
            }//end connection


            //UPDATE in One-to-One
            Student stud;
            // Get student from DB
            using (var dbCtx = new SchoolEntities())
            {
                stud = dbCtx.Students.Where(s => s.StudentName == "New Student1").FirstOrDefault<Student>();

                if (stud != null)
                {
                    stud.StudentName = "Updated Student1";
                }
                

                dbCtx.SaveChanges();

                var categories = from c in dbCtx.StudentAddresses
                                 select c;
                Console.WriteLine("\nUpdate in One-to-One (New Student1 to Updated Student1):");
                foreach (var category in categories)
                {
                    Console.WriteLine("Name:" + category.Student.StudentName + " Address1:" + category.Address1 + " Address2:" + category.Address2 + " City:" + category.City);
                }
            }//end connection


            //DELETE in One-to-One
            using (var dbCtx = new SchoolEntities())
            {
                Student stude = (from s in dbCtx.Students
                                 where s.StudentName == "Student1"
                                 select s).FirstOrDefault<Student>();
                StudentAddress sAddress = stude.StudentAddress;
                dbCtx.StudentAddresses.Remove(sAddress);
                
                dbCtx.SaveChanges();

                var categories = from c in dbCtx.StudentAddresses
                                 select c;
                Console.WriteLine("\nDelete in One-to-One (Student1):");
                foreach (var category in categories)
                {
                    Console.WriteLine("Name:" + category.Student.StudentName + " Address1:" + category.Address1 + " Address2:" + category.Address2 + " City:" + category.City);
                }
            }//end connection


            //CREATE in One-to-Many
            //Create new standard
            var standard = new Standard();
            standard.StandardName = "Standard1";
            //create three new teachers
            var teacher1 = new Teacher();
            teacher1.TeacherName = "New Teacher1";
            var teacher2 = new Teacher();
            teacher2.TeacherName = "New Teacher2";
            var teacher3 = new Teacher();
            teacher3.TeacherName = "New Teacher3";
            //add teachers for new standard
            standard.Teachers.Add(teacher1);
            standard.Teachers.Add(teacher2);
            standard.Teachers.Add(teacher3);
            using (var dbCtx = new SchoolEntities())
            {
                //add standard entity into standards entitySet
                dbCtx.Standards.Add(standard);
                //Save whole entity graph to the database
                dbCtx.SaveChanges();


                var categories = from c in dbCtx.Teachers
                                 select c;
                Console.WriteLine("\nCreate in One-to-Many (New Teacher1, New Teacher2, New Teacher3, Standard1):");
                foreach (Teacher category in categories)
                {
                    Console.WriteLine("Name:" + category.TeacherName+" Standard:"+category.Standard.StandardName);
                }
            }//end connection


            //UPDATE in One-to-Many
            using (var dbCtx = new SchoolEntities())
            {
                //fetching existing standard from the db
                Standard std = (from s in dbCtx.Standards
                                where s.StandardName == "standard3"
                                select s).FirstOrDefault<Standard>();
                std.StandardName = "Updated standard3";

            
                dbCtx.SaveChanges();

                var categories = from c in dbCtx.Teachers
                                 select c;
                Console.WriteLine("\nUpdate in One-to-Many (standard3 to Updated standard3):");
                foreach (Teacher category in categories)
                {
                    Console.WriteLine("Name:" + category.TeacherName + " Standard:" + category.Standard.StandardName);
                }
            }//end connection


            //DELETE in One-to-Many
            using (var dbCtx = new SchoolEntities())
            {
                //fetching existing standard from the db
                Standard std = (from s in dbCtx.Standards
                                where s.StandardName == "Standard2"
                                select s).FirstOrDefault<Standard>();
                //getting first teacher to be removed
                Teacher tchr = std.Teachers.FirstOrDefault<Teacher>();
                //removing teachers
                if (tchr != null)
                    dbCtx.Teachers.Remove(tchr);//.Remove(tchr);
                dbCtx.SaveChanges();

                var categories = from c in dbCtx.Teachers
                                 select c;
                Console.WriteLine("\nDelete in One-to-Many (Standard2):");
                foreach (Teacher category in categories)
                {
                    Console.WriteLine("Name:" + category.TeacherName + " Standard:" + category.Standard.StandardName);
                }
            }//end connection


            //CREATE in Many-to-Many
            //Create student entity
            var student1 = new Student();
            student1.StudentName = "New Student2";
            //Create course entities
            var course1 = new Course();
            course1.CourseName = "New Course1";
            
            var course2 = new Course();
            course2.CourseName = "New Course2";
  
            var course3 = new Course();
            course3.CourseName = "New Course3";
    
            // add multiple courses for student entity
            student1.Courses.Add(course1);
            student1.Courses.Add(course2);
            student1.Courses.Add(course3);
            using (var dbCtx = new SchoolEntities())
            {
                //add student into DBContext
                dbCtx.Students.Add(student1);
                //call SaveChanges
                dbCtx.SaveChanges();
                var categories = from c in dbCtx.View_StudentCourse
                                 select c;
                Console.WriteLine("\nCreate in Many-to-Many (New Student2, New Course1, New Course2, New Course3):");
                foreach (var category in categories)
                {
                    Console.WriteLine("Student Name:" + category.StudentName + " Course Name:" + category.CourseName);
                }
            }//end connection


            //UPDATE in Many-to-Many
            using (var dbCtx = new SchoolEntities())
            {
                Student stude = (from s in dbCtx.Students
                 where s.StudentName == "Student3"
                 select s).FirstOrDefault<Student>();
                stude.StudentName = "Updated Student3";
                Course cours = student.Courses.FirstOrDefault<Course>();
                //removing course from student
                student.Courses.Remove(cours);
                dbCtx.SaveChanges();

                var categories = from c in dbCtx.View_StudentCourse
                                 select c;
                Console.WriteLine("\nUpdate in Many-to-Many (Student3 to Updated Student3):");
                foreach (var category in categories)
                {
                    Console.WriteLine("Student Name:" + category.StudentName + " Course Name:" + category.CourseName);
                }
            }//end connection


            //DELETE in Many-to-Many
            using (var dbCtx = new SchoolEntities())
            {
                Student stude = (from s in dbCtx.Students
                                 where s.StudentName == "Student5"
                                 select s).FirstOrDefault<Student>();
                Course cours = stude.Courses.FirstOrDefault<Course>();
                //removing course from student
                stude.Courses.Remove(cours);
                dbCtx.SaveChanges();

                var categories = from c in dbCtx.View_StudentCourse
                                 select c;
                Console.WriteLine("\nDelete in Many-to-Many (Student5):");
                foreach (var category in categories)
                {
                    Console.WriteLine("Student Name:" + category.StudentName + " Course Name:" + category.CourseName);
                }
            }//end connection
        }
    }
}
