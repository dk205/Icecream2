using System;
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
        public int ActiveUserID;
        public string ActiveUserName;


        public int _ActiveUserID
        {
            get { return ActiveUserID; }
            set { ActiveUserID = value; }
        }

        public string _ActiveUserName
        {
            get { return ActiveUserName; }
            set { ActiveUserName = value; }
        }

        public void StoreActiveUser(int currentUserID, string currentUserName)
        {

            ActiveUserID = currentUserID;
            ActiveUserName = currentUserName;
        } 
        
        
        public void GetActivePatient()   //WIP
        {
            Form formParent = null;
            OverSurgery.MainBackGround GUI =new MainBackGround(formParent);
            


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
            if ((DateTime.Now - expiryDate  ).TotalDays < 14)
            {
               // MessageBox.Show(String.Format("expiryDate: {0} DateTime.now: {1}  and  (expiryDate - DateTime.Now).TotalDays: {2}", expiryDate.ToShortDateString(), DateTime.Now.ToShortDateString(), (DateTime.Now - expiryDate).TotalDays.ToString()));
                if (MessageBox.Show("The Date of birth of the Patient is less than 14 days, are you sure?", "Date of Birth too close", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    EntryErrorMessage_B.Append("\n The date of birth is less than 14 days.");
                    FieldsOK = false;
                }
            }

            if (!(LandLine == ""))
            {
                if (!Regex.IsMatch(LandLine, @"^[0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The LandLine Number can only contain numbers and spaces.");
                    FieldsOK = false;
                }
            }

            if (!(Mobile == ""))
            {
                if (!Regex.IsMatch(Mobile, @"^[0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The Mobile Number can only contain numbers and spaces.");
                    FieldsOK = false;
                }
            }

            if (!(Email == ""))
            {
                int n = Email.IndexOf("@");
               // MessageBox.Show(String.Format("n= {0}", n.ToString()));
                if (n==-1)
                {
                    EntryErrorMessage_B.Append("\n The email is invalid, no @ symbol.");
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




        public Boolean CheckFieldsSearchByName(String Patients_Name , String Post_Code ) //check the quality of the name and post code.
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
                if (!(Patients_Name.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c))))
                {
                    EntryErrorMessage_B.Append("\n The Patients Name can only contain letters and spaces.");
                    FieldsOK = false;
                }
            }

            if (!(Post_Code == ""))
            {
              

                if (!Regex.IsMatch(Post_Code, @"^[a-zA-Z0-9 ]+$"))
                {
                    EntryErrorMessage_B.Append("\n The Post Code can only contain letters and spaces and numbers.");
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


        public Boolean CheckFieldsSearchByID(string ID)  //check the id entered contains only numbers
        {

            if (ID.All(c => Char.IsDigit(c)))
            {

                return true;
            }
            else
            {
                MessageBox.Show(" The Patients ID can only contain letters");
                return false;
            }
        }


    }
}
