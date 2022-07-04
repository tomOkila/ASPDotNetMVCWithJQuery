using ASPDotNetMVCWithJQuery.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPDotNetMVCWithJQuery.Controllers
{
    public class StudentController : Controller
    {

        //shared database connections.
        string studentDBConn = ConfigurationManager.ConnectionStrings["studentDBConn"].ConnectionString;
        SqlConnection con;
        Response response = new Response();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetData()
        {
            var student = new Student();
            List<Student> StudentList = new List<Student>();

            try
            {

                using (con = new SqlConnection(studentDBConn))
                {
                    SqlCommand cmd = new SqlCommand("GetStudentData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //18 / 05 / 2021 01:10:00
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    CultureInfo culture = new CultureInfo("en-US");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StudentList.Add(
                            new Student
                            {
                                StudentID = Convert.ToInt32(dr["StudentID"]),
                                FirstName = Convert.ToString(dr["FirstName"]),
                                LastName = Convert.ToString(dr["LastName"]),
                                Age = Convert.ToInt32(dr["Age"]),
                                //RegistrationDate = DateTime.Parse(Convert.ToString(dr["RegistrationDate"]))
                                //RegistrationDate = JsonConvert.DeserializeObject<DateTime>(dr["RegistrationDate"].ToString())
                                RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"].ToString()),
                            });
                    }


                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "GETSTUDENTDATA");
            }
            finally
            {
                //close connection.
                con.Close();
            }

            //return StudentList;
            return Json(new { data = StudentList }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            List<Student> studentList = new List<Student>();
            var student = new Student();
            if (id == 0)
                return View(new Student());
            else
            {
                try
                {

                    using (con = new SqlConnection(studentDBConn))
                    {
                        SqlCommand cmd = new SqlCommand("GetStudentDataById", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@StudID", id));
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }


                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {

                            while (rdr.Read())
                            {
                                student.FirstName = rdr["FirstName"].ToString();
                                student.LastName = rdr["LastName"].ToString();
                                student.Age = Convert.ToInt32(rdr["Age"]);
                                student.RegistrationDate = DateTime.Parse(rdr["RegistrationDate"].ToString());
                                student.StudentID = id;
                                studentList.Add(student);
                            }
                        }
                        con.Close();
                    }

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteLog(ex.Message, "GETSTUDENTDATA");
                }
                finally
                {
                    //close connection.
                    con.Close();
                }
                return View(student);
                //return Json(new { success = "success", studentList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AddOrEdit(Student std)
        {
            try
            {

                //cheeck for new student
                if (std.StudentID == 0)
                {
                    using (con = new SqlConnection(studentDBConn))
                    {
                        SqlCommand cmd = new SqlCommand("InsertStudentData", con);
                        cmd.Parameters.Add(new SqlParameter("@FirstName", std.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", std.LastName));
                        cmd.Parameters.Add(new SqlParameter("@Age", std.Age));

                        cmd.Parameters.Add(new SqlParameter("@RegistrationDate", std.RegistrationDate));

                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlDataReader rdr = cmd.ExecuteReader();
                    }

                    //formulate the response message
                    response.Status = true;
                    response.Message = "Saved Successfully";
                }
                //update existing student.
                else
                {
                    using (con = new SqlConnection(studentDBConn))
                    {
                        SqlCommand cmd = new SqlCommand("UpdateStudentData", con);
                        cmd.Parameters.Add(new SqlParameter("@StudentID", std.StudentID));
                        cmd.Parameters.Add(new SqlParameter("@FirstName", std.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", std.LastName));
                        cmd.Parameters.Add(new SqlParameter("@Age", std.Age));
                        cmd.Parameters.Add(new SqlParameter("@RegistrationDate", std.RegistrationDate));

                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlDataReader rdr = cmd.ExecuteReader();
                    }

                    //formulate the response message
                    response.Status = true;
                    response.Message = "Updated Successfully";
                }

            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "INSERTSTUDENTDATA");
            }
            finally
            {
                //close connection.
                con.Close();
            }

            return Json(new { success = response.Status, message = response.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            try
            {
                using (con = new SqlConnection(studentDBConn))
                {
                    SqlCommand cmd = new SqlCommand("DeleteStudentData", con);
                    cmd.Parameters.Add(new SqlParameter("@StudentID", id));

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlDataReader rdr = cmd.ExecuteReader();
                }

                //formulate the response message
                response.Status = true;
                response.Message = "Deleted Successfully";
            }
            catch (Exception ex)
            {
                ErrHandler.WriteLog(ex.Message, "DELETESTUDENTDATA");
            }

            return Json(new { success = response.Status, message = response.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}