using System;

namespace part1_poe.Models
{
    public class connectString
    {


        public string connects()
        {

            CreateInstanceDBandTables Connect = new CreateInstanceDBandTables();


           // Connect.InitializeSystem();
         


            string connection = @"Server=(localdb)\claim_system;Database=claims_database";



            return connection;
        }


    }
}
