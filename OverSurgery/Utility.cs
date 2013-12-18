using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverSurgery
{
    class Utility
    {
        public void GetActivePatient()   //WIP
        {



        }


      

     


        // CheckPatientFields Checks the 4 nessasry fields and returns false if at least one is null.
        public Boolean CheckPatientFields(string a,string b, string c, string d) //Name, DOB, Post Code, Address1
        {
            Boolean FieldsOK = true;
            String EntryErrorMessage="";
            StringBuilder EntryErrorMessage_B = new StringBuilder();
            if (a =="")
            {
                EntryErrorMessage_B.Append("\n You must fill in a Name.");
                FieldsOK = false;
            }
           
            if (b == "")
            {
                EntryErrorMessage_B.Append( "\n You must fill in the Date of Birth.");
                FieldsOK = false;
            }
            
            if (c == "")
            {
                EntryErrorMessage_B.Append( "\n You must fill the Post Code.");
                FieldsOK = false;
            }
            
            if (d == "")
            {
                EntryErrorMessage_B.Append("\n You must fill in an address.");
                FieldsOK = false;
            }
            if (!FieldsOK)
            {
                EntryErrorMessage = EntryErrorMessage_B.ToString();
                MessageBox.Show(String.Format("The following fields need to be amend:  {0}", EntryErrorMessage), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                return FieldsOK;
        }

    }
}
