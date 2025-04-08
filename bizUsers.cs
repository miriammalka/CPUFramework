using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUFramework
{
        public class bizUsers : bizObject<bizUsers>
        {
            public bizUsers()
            {

            }
            //made firstname and lastname default to blank to fix API issue
            private int _usersId;
            private string _firstname = "";
            private string _lastname = "";
            private string _username;



            public int UsersId
            {
                get { return _usersId; }
                set
                {
                    if (_usersId != value)
                    {
                        _usersId = value;
                        InvokePropertyChanged();
                    }
                }
            }

            public string FirstName
            {
                get { return _firstname; }
                set
                {
                    if (_firstname != value)
                    {
                        _firstname = value;
                        InvokePropertyChanged();
                    }
                }
            }

            public string LastName
            {
                get { return _lastname; }
                set
                {
                    if (_lastname != value)
                    {
                        _lastname = value;
                        InvokePropertyChanged();
                    }
                }
            }

            public string UserName
            {
                get { return _username; }
                set
                {
                    if (_username != value)
                    {
                        _username = value;
                        InvokePropertyChanged();
                    }
                }
            }

            public int RoleId { get; set; }
            public string Password { get; set; } = "";
            public string SessionKey { get; set; } = "";
            public string RoleName { get; set; } = "";
            public int RoleRank { get; set; }

            public void Login()
            {
                SqlCommand cmd = SQLUtility.GetSQLCommand("UsersLogin");
                SQLUtility.SetParamValue(cmd, "username", UserName);
                SQLUtility.SetParamValue(cmd, "password", Password);
                this.Password = "";
                DataTable dt = SQLUtility.GetDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    this.LoadProps(dt.Rows[0]);
                }
            }

            public void Logout()
            {
                SqlCommand cmd = SQLUtility.GetSQLCommand("UsersLogout");
                SQLUtility.SetParamValue(cmd, "username", UserName);
                DataTable dt = SQLUtility.GetDataTable(cmd);
                this.UsersId = 0;
                this.UserName = "";
                this.RoleId = 0;
                this.RoleName = "";
                this.RoleRank = 0;
            }

            public void LoadBySessionKey()
            {
                SqlCommand cmd = SQLUtility.GetSQLCommand("UsersGet");
                SQLUtility.SetParamValue(cmd, "sessionkey", SessionKey);
                DataTable dt = SQLUtility.GetDataTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    this.LoadProps(dt.Rows[0]);
                }
            }
        }
    }

