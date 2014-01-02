﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OverSurgery
{
    class Utility
    {
        public void GetActivePatient()   //WIP
        {



        }


      

     


        // CheckPatientFields Checks the 4 nessasry fields and returns false if at least one is null.
        public Boolean CheckPatientFields(string Patients_Name, string DOB, string Post_code, string Address1, string Address2, string Email, string LandLine, string Mobile, string Sex) //Name, DOB, Post Code, Address1
        {
            Boolean FieldsOK = true;
            String EntryErrorMessage = "";
            StringBuilder EntryErrorMessage_B = new StringBuilder();
            if (Patients_Name == "")
            {
                EntryErrorMessage_B.Append("\n You must fill in a Name.");
                FieldsOK = false;
            }
            else
            {

                // if (!Regex.IsMatch(a, @"^[a-zA-Z ]$"))
                if (!(Patients_Name.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c))))
                {
                    EntryErrorMessage_B.Append("\n The Patients Name can only contain letters and spaces.");
                    FieldsOK = false;
                }
            }

            if (DOB == "")
            {
                EntryErrorMessage_B.Append("\n You must fill in the Date of Birth.");
                FieldsOK = false;
            }
           

            if (Post_code == "")
            {
                EntryErrorMessage_B.Append("\n You must fill the Post Code.");
                FieldsOK = false;
            }
            else
            {

                if (!Regex.IsMatch(Post_code, @"^[a-zA-Z0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The Post Code can only contain letters and spaces and numbers.");
                    FieldsOK = false;
                }
            }

            if (Address1 == "")
            {
                EntryErrorMessage_B.Append("\n You must fill in an address.");
                FieldsOK = false;
            }

            else
            {

                if (!Regex.IsMatch(Address1, @"^[a-zA-Z0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The Address (line 1) can only contain letters and spaces and numbers.");
                    FieldsOK = false;
                }
            }



            if (!(Address2 == ""))
            {
                if (!Regex.IsMatch(Address2, @"^[a-zA-Z0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The Address (line 2) can only contain letters and spaces and numbers.");
                    FieldsOK = false;
                }
            }


            if (Sex == "Please Select")
            {
                EntryErrorMessage_B.Append("\n Please Select a Sex.");
                FieldsOK = false;

            }
            DateTime expiryDate = DateTime.Parse(DOB);
            if ((expiryDate - DateTime.Now).TotalDays < 14)
            {
                if (MessageBox.Show("The Date of birth of the Patient is less than 14 days, are you sure?", "Date of Birth too close", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    EntryErrorMessage_B.Append("\n The date of birth is less than 14 days.");
                    FieldsOK = false;
                }
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
