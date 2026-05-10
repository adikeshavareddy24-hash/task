using Microsoft.Win32;
using Newtonsoft.Json;
using studentweb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace studentweb.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbaseconnection"].ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
            return View();
        }
        [HttpPost]
        public string Student(Student std)
        {
            con.Open();
            string query = "insert into student(Name,Email,course,phonenumber) values(@Name,@Email,@course,@phonenumber)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", std.Name);
            cmd.Parameters.AddWithValue("@Email", std.Email);
            cmd.Parameters.AddWithValue("@course", std.Course);
            cmd.Parameters.AddWithValue("@phonenumber", std.phonenumber);
            var data = cmd.ExecuteNonQuery();
            con.Close();
            //using(HttpClient client=new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7048/");
            //    var json = JsonConvert.SerializeObject(std);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    HttpResponseMessage response =  client.PostAsync("api/Student",content).Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        ViewBag.Message = "student added successfully";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "error while adding the student";
            //    }
            //}
            return "saved";
        }
        [HttpGet]
        public string Getdetails(Student std)
        {
            List<Student> stdlist=new List<Student>();
            con.Open();
            string query="select * from student";
            SqlCommand cmd=new SqlCommand(query, con);
            var data = cmd.ExecuteReader();
            while (data.Read())
            {
                Student stud = new Student();
                stud.Id = Convert.ToInt32(data["Id"].ToString());
                stud.Name= data["Name"].ToString();
                stud.Email = data["Email"].ToString();
                stud.Course = data["course"].ToString();
                stud.phonenumber = Convert.ToInt64(data["phonenumber"].ToString());
                stdlist.Add(stud);
            }
            con.Close();
            string jasonobject=JsonConvert.SerializeObject(stdlist);
            return jasonobject;
            
        }
        [HttpGet]
        public string Editstudent(int Id)
        {
            Student stud = new Student();
            con.Open();
            string query = "select * from student where Id='"+Id+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            var data = cmd.ExecuteReader();
            while (data.Read())
            {
                
                stud.Id = Convert.ToInt32(data["Id"].ToString());
                stud.Name = data["Name"].ToString();
                stud.Email = data["Email"].ToString();
                stud.Course = data["course"].ToString();
                stud.phonenumber = Convert.ToInt64(data["phonenumber"].ToString());
                
            }
            con.Close();
            string jasonobject = JsonConvert.SerializeObject(stud);
            return jasonobject;

        }
        [HttpPost]
        public string Updatedetails(Student std)
        {
            con.Open();
            string query = "update student set Name=@Name,Email=@Email,course=@course,phonenumber=@phonenumber where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", std.Name);
            cmd.Parameters.AddWithValue("@Email", std.Email);
            cmd.Parameters.AddWithValue("@course", std.Course);
            cmd.Parameters.AddWithValue("@phonenumber", std.phonenumber);
            cmd.Parameters.AddWithValue("@Id", std.Id);
            var data = cmd.ExecuteReader();
            con.Close();
            return "updated successfully";
        }
        [HttpGet]
        public string Deletestudent(int Id)
        {
            con.Open();
            string query = "delete from student where Id='"+Id+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            var data = cmd.ExecuteNonQuery();
            con.Close();
            if (data == 1)
            {
                return "record deleted successfully";
            }
            else
            {
                return "record not deleted";
            }
        }
    }
}