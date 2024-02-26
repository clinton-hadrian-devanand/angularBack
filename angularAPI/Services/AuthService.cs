using angularAPI.Interfaces;
using angularAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace angularAPI.Services
{
    public class AuthService : IAuth 
    {

        private readonly string _userString;
        private IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _userString = config.GetConnectionString("userString");
            _config = config;
        }

        public DataTable Authenticate(AuthModel auth)
        {   
            try
            {

                DataTable dt= new DataTable();

                using(OracleConnection con=new OracleConnection(_userString))
                {
                    con.Open();
                    using (OracleCommand cmd=con.CreateCommand())
                    {

                        cmd.CommandText = "SELECT * FROM users WHERE mail = :email AND user_password = :password";
                        cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = auth.email;
                        cmd.Parameters.Add("password", OracleDbType.Varchar2).Value = auth.password;
                        OracleDataAdapter oda=new OracleDataAdapter(cmd);
                        oda.Fill(dt);
                        con.Close();

                    }
                }
                return dt;

            }catch(Exception e)
            {
                return null;
            }
           
        }

        public DataTable RegisterUser(AuthModel auth)
        {
            try
            {
                DataTable dt = new DataTable();

                using (OracleConnection con = new OracleConnection(_userString))
                {
                    con.Open();
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandTimeout = 10;
                        cmd.CommandType= CommandType.StoredProcedure;

                        cmd.CommandText = "AUTH_PROCEDURE"; 
                        cmd.BindByName = true;
                        cmd.Parameters.Add("ARG_FLAG", OracleDbType.Int32).Value = 2;
                        cmd.Parameters.Add("ARG_FIRSTNAME", OracleDbType.Varchar2).Value = auth.firstName;
                        cmd.Parameters.Add("ARG_LASTNAME", OracleDbType.Varchar2).Value = auth.lastName;
                        cmd.Parameters.Add("ARG_MAIL", OracleDbType.Varchar2).Value = auth.email;
                        cmd.Parameters.Add("ARG_PASSWORD", OracleDbType.Varchar2).Value = auth.password;
                        cmd.Parameters.Add("ARG_CREATEDBY", OracleDbType.Varchar2).Value = auth.firstName + ' ' + auth.lastName;
                        cmd.Parameters.Add("ARG_TOKEN", OracleDbType.Varchar2).Value = auth.token;
                        cmd.Parameters.Add("ARG_OUTPUT", OracleDbType.RefCursor, 100).Direction = ParameterDirection.Output;
                        OracleDataAdapter oda = new OracleDataAdapter(cmd);
                        oda.Fill(dt);
                        con.Close();
                    }
                }

                return dt;

            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
    