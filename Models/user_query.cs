using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace part1_poe.Models
{
    public class user_query
    {


        connectString connect = new connectString();

        public string check_users(string email , string password,string? role)
        {


            string? get_id = "0";

            //error handling 
            try {

                using (SqlConnection get_connection = new SqlConnection(connect.connects() ))
                {
                    get_connection.Open();
                    string Query = @"select * from Users where email='"+email+"' and password='"+password+"';";

                    using (SqlCommand find_user = new SqlCommand(Query,get_connection))
                    {

                        using (SqlDataReader found = find_user.ExecuteReader() )
                        {
                            while (found.Read() )
                            {
                                get_id = found["userID"].ToString()+","+found["role"].ToString();
                                role = found["role"].ToString();


                            }

                        }


                    }
                    get_connection.Close();

                }


            }
            catch (Exception error)
            {

                Console.WriteLine("Eror: " + error.Message);
            }

            return get_id;

        }

        public bool claimsbmit(
           string userId,
           int sessions,
           int hours,
           decimal rate,
           decimal totalAmount,
           string module,
           string faculty,
           string? documentPath)
        {
            bool success = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();

                    string insertQuery = @"
                INSERT INTO Claims 
                (number_of_sessions, number_of_hours, amount_of_rate, module_name, faculty_name, supporting_documents, claim_status, creating_date, lecturerID)
                VALUES 
                (@Sessions, @Hours, @Rate, @Module, @Faculty, @Documents, @Status, @Date, @LecturerID)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sessions", sessions);
                        cmd.Parameters.AddWithValue("@Hours", hours);
                        cmd.Parameters.AddWithValue("@Rate", rate);
                        cmd.Parameters.AddWithValue("@Module", module);
                        cmd.Parameters.AddWithValue("@Faculty", faculty);
                        cmd.Parameters.AddWithValue("@Documents", documentPath ?? string.Empty); 
                        cmd.Parameters.AddWithValue("@Status", "Pending");
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@LecturerID", int.Parse(userId));

                        int rows = cmd.ExecuteNonQuery();
                        success = rows > 0;
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting claim: " + ex.Message);
            }

            return success;
        }








        public string register_user(
      string firstName,
      string surname,
      string email,
      string gender,
      string role,
      string password)
        {

            string? get_id = "0";

            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();

                  
                    // Insert new user
                    string insertQuery = @"
                INSERT INTO Users (full_names, surname, email, role, gender, password, date)
                OUTPUT INSERTED.userID
                VALUES (@FirstName, @Surname, @Email, @Role, @Gender, @Password, @Date)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@FirstName", firstName);
                        insertCmd.Parameters.AddWithValue("@Surname", surname);
                        insertCmd.Parameters.AddWithValue("@Email", email);
                        insertCmd.Parameters.AddWithValue("@Role", role);
                        insertCmd.Parameters.AddWithValue("@Gender", gender);
                        insertCmd.Parameters.AddWithValue("@Password", password); 
                        insertCmd.Parameters.AddWithValue("@Date", DateTime.Now.Date);

                        
                        get_id = insertCmd.ExecuteScalar().ToString();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return get_id;
        }


        public decimal calculate_total(int Sessions, int Rate)
        {
            return Sessions * Rate;
        }


        public List<view_claims> GetClaims(string lecturerId)
        {
            List<view_claims> claimsList = new List<view_claims>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();

                    string query = @"
                SELECT claimID,
                       number_of_sessions,
                       number_of_hours,
                       amount_of_rate,
                       (number_of_sessions * amount_of_rate) AS total_amount,
                       module_name,
                       faculty_name,
                       supporting_documents,
                       claim_status,
                       creating_date
                FROM Claims
                WHERE lecturerID = @lecturerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@lecturerID", lecturerId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var claim = new view_claims
                                {
                                    ClaimID = Convert.ToInt32(reader["claimID"]),
                                    NumberOfSessions = Convert.ToInt32(reader["number_of_sessions"]),
                                    NumberOfHours = Convert.ToInt32(reader["number_of_hours"]),
                                    Rate = Convert.ToDecimal(reader["amount_of_rate"]),
                                    TotalAmount = Convert.ToDecimal(reader["total_amount"]),
                                    ModuleName = reader["module_name"].ToString(),
                                    FacultyName = reader["faculty_name"].ToString(),
                                    SupportingDocument = reader["supporting_documents"].ToString(),
                                    ClaimStatus = reader["claim_status"].ToString(),
                                    CreatingDate = Convert.ToDateTime(reader["creating_date"])
                                };

                                claimsList.Add(claim);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return claimsList;
        }

        public bool CancelClaim(int claimId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();
                    string query = "UPDATE Claims SET claim_status = 'Cancelled' WHERE claimID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", claimId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cancelling claim: " + ex.Message);
                return false;
            }
        }

        public bool DeleteClaim(int claimId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();
                    string query = "DELETE FROM Claims WHERE claimID = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", claimId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting claim: " + ex.Message);
                return false;
            }
        }



        public List<pre_approve> GetAllClaims()
        {
            List<pre_approve> list = new List<pre_approve>();

            using (SqlConnection conn = new SqlConnection(connect.connects()))
            {
                string query = @"
           SELECT claimID,
                  number_of_sessions,
                  number_of_hours,
                  amount_of_rate,
                  (number_of_sessions * amount_of_rate) AS total_amount,
                  module_name,
                  faculty_name,
                  supporting_documents,
                  claim_status,
                  creating_date,
Users.full_names ,
Users.surname
FROM Claims inner join Users on userID=lecturerID  where claim_status!='Pre-Approved' and claim_status!='approved'
           ORDER BY creating_date ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new pre_approve
                            {
                                ClaimID = reader.GetInt32(0),
                                NumberOfSessions = reader.GetInt32(1),
                                NumberOfHours = reader.GetInt32(2),
                                Rate = Convert.ToDecimal(reader.GetInt32(3)),
                                TotalAmount = Convert.ToDecimal(reader.GetInt32(4)),
                                ModuleName = reader.GetString(5),
                                FacultyName = reader.GetString(6),
                                SupportingDocument = reader.IsDBNull(7) ? null : reader.GetString(7),
                                ClaimStatus = reader.GetString(8),
                                CreatingDate = reader.GetDateTime(9),
                                names = reader.GetString(10)+" " + reader.GetString(11)
                            });
                        }
                    }
                }
            }

            return list;
        }


        public List<pre_approve> GetAllClaim()
        {
            List<pre_approve> list = new List<pre_approve>();

            using (SqlConnection conn = new SqlConnection(connect.connects()))
            {
                string query = @"
           SELECT claimID,
                  number_of_sessions,
                  number_of_hours,
                  amount_of_rate,
                  (number_of_sessions * amount_of_rate) AS total_amount,
                  module_name,
                  faculty_name,
                  supporting_documents,
                  claim_status,
                  creating_date,
Users.full_names ,
Users.surname
FROM Claims inner join Users on userID=lecturerID where claim_status!='pending'
           ORDER BY creating_date ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new pre_approve
                            {
                                ClaimID = reader.GetInt32(0),
                                NumberOfSessions = reader.GetInt32(1),
                                NumberOfHours = reader.GetInt32(2),
                                Rate = Convert.ToDecimal(reader.GetInt32(3)),
                                TotalAmount = Convert.ToDecimal(reader.GetInt32(4)),
                                ModuleName = reader.GetString(5),
                                FacultyName = reader.GetString(6),
                                SupportingDocument = reader.IsDBNull(7) ? null : reader.GetString(7),
                                ClaimStatus = reader.GetString(8),
                                CreatingDate = reader.GetDateTime(9),
                                names = reader.GetString(10) + " " + reader.GetString(11)
                            });
                        }
                    }
                }
            }

            return list;
        }

        public bool PreApproveClaim(int claimId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();
                    string query = "UPDATE Claims SET claim_status = @Status WHERE claimID = @ClaimID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "Pre-Approved");
                        cmd.Parameters.AddWithValue("@ClaimID", claimId);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PreApproveClaims(int claimId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();
                    string query = "UPDATE Claims SET claim_status = @Status WHERE claimID = @ClaimID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "Approved");
                        cmd.Parameters.AddWithValue("@ClaimID", claimId);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool RejectClaim(int claimId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.connects()))
                {
                    conn.Open();
                    string query = "UPDATE Claims SET claim_status = @Status WHERE claimID = @ClaimID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", "Rejected");
                        cmd.Parameters.AddWithValue("@ClaimID", claimId);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }







        }
    }
