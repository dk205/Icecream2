using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OverSurgery;

//using Mysql.Data.MysqlClient;

namespace OverSurgery
{
    public partial class MainBackGround : Form
    {

        OverSurgery.Utility Utilities = new Utility(); //create a link for the class utility
      
        Boolean ChangingAppointment = false;
        Form formParent = null;
        public MainBackGround(Form par)
        {
            formParent = par;
            InitializeComponent();
            dateTimePickerAP.MinDate = DateTime.Now;         //Setting the Appointments Calendar so you can book days older than the current day and no further the 2 weeks in advance.  
            dateTimePickerAP.MaxDate = DateTime.Now.AddDays(17);
            dateTimePickerRota.MinDate = DateTime.Now;        //Setting the Rota Calendar so you can set the rota from the current day and no further than 6 months in advance.   
            dateTimePickerRota.MaxDate = DateTime.Now.AddMonths(6);
            dateTimePickerNR.Value = DateTime.Now;
            dateTimePickerSearchDOB.MaxDate = DateTime.Now;
            dateTimePickerSearchDOB.Value = DateTime.Now;
            

        }

        public string PreviousName="Free";  //Variables used for the first initiation of the comboboxes in the Timetable Tab
        public bool PreSelectedValue =false;

       

        void FillStaffMenu() // this function loads all the Staff Names into  the 5 combo boxes in the Timetable Tab 
        {
            int i = 1;
            
            cbStaffMenu1.Items.Insert(0,"Free");
            cbStaffMenu2.Items.Insert(0, "Free");
            cbStaffMenu3.Items.Insert(0, "Free");
            cbStaffMenu4.Items.Insert(0, "Free");
            cbStaffMenu5.Items.Insert(0, "Free");
            OverSugerydbaseDataSet.StaffDataTable StaffMenuTable = this.staffTableAdapter.GetData();
            foreach (OverSugerydbaseDataSet.StaffRow row in StaffMenuTable)
            {
                          
              cbStaffMenu1.Items.Insert(i,row.Surname);
              cbStaffMenu2.Items.Insert(i,row.Surname);
              cbStaffMenu3.Items.Insert(i,row.Surname);
              cbStaffMenu4.Items.Insert(i,row.Surname);
              cbStaffMenu5.Items.Insert(i,row.Surname);
              i++;

            }
            cbStaffMenu1.SelectedIndex = 0;
            cbStaffMenu2.SelectedIndex = 0;
            cbStaffMenu3.SelectedIndex = 0;
            cbStaffMenu4.SelectedIndex = 0;
            cbStaffMenu5.SelectedIndex = 0;

        }


        void DestroyMenus() // destroys the comboboxes in the timetable Tab.
        {
            cbStaffMenu1.Items.Clear();
            cbStaffMenu2.Items.Clear();
            cbStaffMenu3.Items.Clear();
            cbStaffMenu4.Items.Clear();
            cbStaffMenu5.Items.Clear();
        }
/*
        private LogIn logIn;

        public MainBackGround(LogIn logIn)
        {
            // TODO: Complete member initialization
            this.logIn = logIn;
        }
 * probably need this at some point
        */
      
        public void setMeVisible(string PanelName)  //this is a small function that is called when I want to move to another pannel
        {                                           //It hides all existing pannels and then set to visible the panel whose name given in the function. The name must be inside brackets.
            PageMainScreen.Visible = false;
            PageNewRegistration.Visible = false;
            PageSelectedPatient.Visible = false;
            PageEditPatientDetails.Visible = false;
            PageEnterTestResults.Visible = false;
            PageMakeAppointment.Visible = false;
            PageAddViewExtendMedication.Visible = false;
            PageViewCancelEditAppointment.Visible = false;
            PageViewPrintTestResults.Visible = false;
            
            var control = Controls.Find(PanelName, true).FirstOrDefault();
            control.Refresh();
            control.Visible = true;

        }
         
        
        public void ClearNewRegistrationFields()  // small function to clear the fields in the new registration page.
        {                                          // its used when the registration is canceled and when the new registration is created.
            txtNRPatientsName.Text = String.Empty;
        cbNRSex.Text = "Please Select";
        txtNRPC.Text = String.Empty;
        txtNRAddress1.Text = String.Empty;
        txtNRAddress2.Text = String.Empty;
        txtNRMobile.Text = String.Empty;
        txtNRLandLine.Text = String.Empty;
        txtNREmail.Text = String.Empty;
        dateTimePickerNR.Value = DateTime.Now;

     
        }

        private void button1_Click(object sender, EventArgs e) //this is the LOG OUT BUTTON
        {
            Application.Exit();

        }

    
        private void NewRegButton_Click(object sender, EventArgs e) //go to the new registration page
        {

            setMeVisible("PageNewRegistration");

        }

      

     

        private void SearchIDButton_Click(object sender, EventArgs e) //user enters a ID number and presses the search button 
        {
            if (Utilities.CheckFieldsSearchByID(txtBoxID.Text))
            {
                OverSugerydbaseDataSet.PatientsDataTable IDSearched = this.patientsTableAdapter.SearchByID(Convert.ToInt32(txtBoxID.Text));  //database querry searches for rows containing the id (we know it is only one, if it exists)
                try //store the values from the sql search 
                {
                    object a = IDSearched.Rows[0]["Patient Name"]; //To get the data you store each value into an object
                    object b = IDSearched.Rows[0]["Id"];
                    setMeVisible("PageSelectedPatient"); // go to the Selected patient page.
                    txtBoxID.Text = string.Empty;   //clear the search id textbox for future use. 

                    txtActiveUserName.Text = Convert.ToString(a); //place the id and the name on the area above the option, so you always know the patient you are handeling
                    txtActiveUserID.Text = Convert.ToString(b);
                    btnClearActivePatient.Visible = true;  // now that there is an active patient, make the clear_active_patient button available
                    Utilities.ActiveUserID = Convert.ToInt32(txtActiveUserID.Text);  //a global variable to store the active patients id.
                }
                catch (Exception) //if there are no values (caused if the id is not found in the database) catch it
                {
                    MessageBox.Show("The Id entered does not belong \n to a registered patient", "Patient not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
         }

       

    

        private void button1_Click_1(object sender, EventArgs e) //takes you to the make appointment page
        {
            cbStaff.Items.Clear();
            this.PageMakeAppointment.Controls.Add(this.cbStaff); //create the staff otions combobox dynamically
            this.cbStaff.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbStaff.FormattingEnabled = true;
            this.cbStaff.Location = new System.Drawing.Point(343, 87);
            this.cbStaff.Name = "cbStaff";
            this.cbStaff.Size = new System.Drawing.Size(121, 21);
            this.cbStaff.TabIndex = 14;
        
              int i = 0;
            
            OverSugerydbaseDataSet.StaffDataTable StaffMenuTable = this.staffTableAdapter.GetData();
            foreach (OverSugerydbaseDataSet.StaffRow row in StaffMenuTable)  //add the staff names as options
            {
               

                cbStaff.Items.Insert(i, row.Surname);
                
                i++;

            }
            cbStaff.Items.Insert(0, "Any GP/Nurse");
            cbStaff.Items.Insert(1, "Any GP");
            cbStaff.Items.Insert(2, "Any Nurse");
            cbStaff.Items.Insert(3, "Any Male GP");
            cbStaff.Items.Insert(4, "Any Female GP");
            cbStaff.SelectedIndex = 0;

            setMeVisible("PageMakeAppointment");  
        }

        
        private void button2_Click(object sender, EventArgs e) //takes you to the edit cancel delete  appointment page
        {
            setMeVisible("PageViewCancelEditAppointment");
        }

      
        private void button3_Click(object sender, EventArgs e) //takes you to the make add/extend medication page
        {
            setMeVisible("PageAddViewExtendMedication");
            this.medicationTableAdapter.FillByPatID(this.overSugerydbaseDataSet.Medication, txtActiveUserID.Text);
        }

      
        private void button4_Click(object sender, EventArgs e)   //takes you to view the Test Results
        {
            setMeVisible("PageViewPrintTestResults");
            this.resultsTableAdapter.FillByResPatID(this.overSugerydbaseDataSet.Results, txtActiveUserID.Text);
        }

        private void button5_Click(object sender, EventArgs e)  //takes you to the  page "enterTest Results"
        {
            setMeVisible("PageEnterTestResults");
        }

       

       

        private void MainBackGround_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Results' table. You can move, or remove it, as needed.
            this.resultsTableAdapter.Fill(this.overSugerydbaseDataSet.Results);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet3.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter2.Fill(this.overSugerydbaseDataSet3.Staff);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet2.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter1.Fill(this.overSugerydbaseDataSet2.Staff);
           
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.TwoActiveWeeks' table. You can move, or remove it, as needed.
            this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Medication' table. You can move, or remove it, as needed.
            this.medicationTableAdapter.Fill(this.overSugerydbaseDataSet.Medication);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Rota' table. You can move, or remove it, as needed.
            this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Week52' table. You can move, or remove it, as needed.
          //  this.week52TableAdapter.Fill(this.overSugerydbaseDataSet.Week52);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.overSugerydbaseDataSet.Patients);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);

        }

       
        private void CreateReg_Click(object sender, EventArgs e)
        {
            

            if (Utilities.CheckPatientFields(txtNRPatientsName.Text,  dateTimePickerNR.Text, txtNRPC.Text, txtNRAddress1.Text, txtNRAddress2.Text, txtNREmail.Text, txtNRLandLine.Text, txtNRMobile.Text,cbNRSex.Text)) //the function in the utility class checks if the fields entered are correct returns "true" if all is ok.
            {
                this.patientsTableAdapter.InsertNewRegistration(txtNRPatientsName.Text, dateTimePickerNR.Text, cbNRSex.Text, txtNRPC.Text, txtNRAddress1.Text, txtNRAddress2.Text, txtNRMobile.Text, txtNRLandLine.Text, txtNREmail.Text); //store the data from the textboxes to the database
                this.patientsTableAdapter.Fill(this.overSugerydbaseDataSet.Patients); //*TEMP this will be removed after testing

                OverSugerydbaseDataSet.PatientsDataTable LastRegistration = this.patientsTableAdapter.GetActivePatientQuery(); //creates a table with 1 row which is the last row just entered (our active patient)
                
                //code to view and store the active patient, it will becaume a  function cause it is used in the search id.
                object a= LastRegistration.Rows[0]["Patient Name"];
                object b = LastRegistration.Rows[0]["Id"];
                Utilities.ActiveUserName = Convert.ToString(a); //store the values ActiveUserId and ActiveUserName in the Utility class.
                Utilities.ActiveUserID = Convert.ToInt32(b);
             
                txtActiveUserName.Text = Utilities.ActiveUserName;   //display the active users name and ID
                txtActiveUserID.Text = Utilities.ActiveUserID.ToString();
               
                setMeVisible("PageSelectedPatient");  //takes you to the selected patient page
                btnClearActivePatient.Visible = true;   //show the clear button so we can remove the active patient if we want.
                
                ClearNewRegistrationFields();  //clear the fields for next new registration
            }

        }

  

      

        private void CancelReg_Click_1(object sender, EventArgs e) //button event if user decides to cancel the registration.
        {
            setMeVisible("PageMainScreen"); //go  to main screen
            ClearNewRegistrationFields(); //remove any data left on the textboxes.
            
               
        }

        private void btnClearActivePatient_Click(object sender, EventArgs e) //when the User has finished with the patient they click on the "clear" button so there is no more an active patient
        {
             setMeVisible("PageMainScreen");  //go to the main screen
            txtActiveUserID.Text = " ";    //clear the two fields above the otions
            txtActiveUserName.Text = " ";
            ChangingAppointment = false;
            Utilities.ActiveUserID = 0;
            btnClearActivePatient.Visible = false; //hide the button
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)  //replaces the values of the database with the ones entered/altered in the text boxes.  
        {
            //CHECK HERE

           // OverSurgery.Utility Utilities = new Utility(); //creat a link for the class utility
            if (Utilities.CheckPatientFields(tbPatientNameEdit.Text, tbDOBEdit.Text, tbPCEdit.Text, tbAddress1Edit.Text, tbAddress2Edit.Text, tbEmailEdit.Text, tbLandLineEdit.Text, tbMobileEdit.Text, tbSexEdit.Text)) // checks the fields same way as with the new registration
            {
                this.patientsTableAdapter.UpdatePatientDetails(tbPatientNameEdit.Text, tbDOBEdit.Text, tbSexEdit.Text, tbPCEdit.Text, tbAddress1Edit.Text, tbAddress2Edit.Text, tbMobileEdit.Text, tbLandLineEdit.Text, tbEmailEdit.Text, Convert.ToInt32(tbIDEdit.Text), Convert.ToInt32(tbIDEdit.Text)); //store the values

                setMeVisible("PageSelectedPatient");
            }
        }

        private void button8_Click(object sender, EventArgs e) //cancel button takes you back to the selected patient page
        {
            setMeVisible("PageSelectedPatient");
        }

        private void goToEditPatient_Click(object sender, EventArgs e) //takes you to edit patient details
        {                                                              //and displays the Patients current details in textboxes
            setMeVisible("PageEditPatientDetails");
            OverSugerydbaseDataSet.PatientsDataTable IDSearched = this.patientsTableAdapter.SearchByID(Utilities.ActiveUserID); //store the row of our active patient into the ActiveID table

            object EditID = IDSearched.Rows[0]["Id"];  //extract each columns first row into a object
            object EditName = IDSearched.Rows[0]["Patient Name"];
            object EditDOB = IDSearched.Rows[0]["Date of Birth"];
            object EditSex = IDSearched.Rows[0]["Sex"];
            object EditPC = IDSearched.Rows[0]["Post Code"];
            object EditAddress1 = IDSearched.Rows[0]["Address 1"];
            object EditAddress2 = IDSearched.Rows[0]["Address 2"];
            object EditMobile = IDSearched.Rows[0]["Mobile Number"];
            object EditLandLine = IDSearched.Rows[0]["LandLine"];
            object EditEmail = IDSearched.Rows[0]["Email"];
            tbIDEdit.Text = Convert.ToString(EditID);             //place all the data into textboxes
            tbPatientNameEdit.Text = Convert.ToString(EditName);
            tbDOBEdit.Text = Convert.ToString(EditDOB);
            tbSexEdit.Text = Convert.ToString(EditSex);
            tbPCEdit.Text = Convert.ToString(EditPC);
            tbAddress1Edit.Text = Convert.ToString(EditAddress1);
            tbAddress2Edit.Text = Convert.ToString(EditAddress2);
            tbMobileEdit.Text = Convert.ToString(EditMobile);
            tbLandLineEdit.Text = Convert.ToString(EditLandLine);
            tbEmailEdit.Text = Convert.ToString(EditEmail);
        }

        private void btnCheckAppointments_Click(object sender, EventArgs e)
        {
            
            labelStaff0.Text= "Blank";  //set the Labels with the Staff names on the timetable to blank
            labelStaff1.Text = "Blank";
            labelStaff2.Text = "Blank";
            labelStaff3.Text = "Blank";
            labelStaff4.Text = "Blank";
            labelStaff5.Text = "Blank";
            labelStaff6.Text = "Blank";
            labelStaff7.Text = "Blank";
            labelStaff8.Text = "Blank";
            labelStaff9.Text = "Blank";

            for (int k = 1; k < 14; k++)  //remove all the objects from row 1 & column 1  in the morning and afternoun tables.
                for (int l = 1; l < 5; l++)
                { 

                    Control TBM = TableMorning.GetControlFromPosition(k, l);
                    Control TBA = TableAfternoun.GetControlFromPosition(k, l);
                    this.TableMorning.Controls.Remove(TBM);
                    this.TableAfternoun.Controls.Remove(TBA);

                }
         

            //declare the table StaffFound
            OverSugerydbaseDataSet.RotaDataTable StaffFound = new OverSugerydbaseDataSet.RotaDataTable();
          
            switch (cbStaff.SelectedIndex)
            {
                case 0:
                    
                        //Selected all staff
                        //make a table  with all doctors that day.
                        
                        StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePickerAP.Value.Date.ToString());
                        break;
                    
                case 1:
                   //All GP
                    //make a table with only staff role=GP   
                    StaffFound = this.rotaTableAdapter.SearchByRoleAndDate("GP", dateTimePickerAP.Value.Date.ToString());
            break;
                case 2:
                    // selected all Nurses
                    //make a table with only staff role=Nurse           
                  StaffFound = this.rotaTableAdapter.SearchByRoleAndDate("Nurse",dateTimePickerAP.Value.Date.ToString());
                    break;
                case 3:
                    // selected all male gp
                    //make a table with only staff role=GP nad sex =Male
                    StaffFound = this.rotaTableAdapter.SearchBySexAndRoleAndDate("Male", dateTimePickerAP.Value.ToString(),"GP");                  
                  break;
                case 4:
                    // selected all female gp
                    //make a table with only staff role=GP and Sex =Female
                    StaffFound = this.rotaTableAdapter.SearchBySexAndRoleAndDate("Female", dateTimePickerAP.Value.ToString(), "GP");   
                    break;
                default:
                    
                    //make a table with the doctors name that date

                     StaffFound = this.rotaTableAdapter.SearchRotaByNameAndDate(cbStaff.Text, dateTimePickerAP.Value.ToString());
                    break;
            }

            Label[,] SlotLabel = new Label[26, 13];  //create dynamically labels and buttons for the timetable
           Button[,] BookButton = new Button[26, 13];
           this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks); //******TEMPORARY
           this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);           //******TEMPORARY

          
               string StaffID = string.Empty;
              
           
               int i = 0;
               //do as many as the staff is in that table
               foreach (OverSugerydbaseDataSet.RotaRow row in StaffFound)
               {
                  
                   //create the names in the labels
                   Label Amorphos = this.Controls.Find("labelStaff" + i, true).FirstOrDefault() as Label ;
                    Amorphos.Refresh();
                    Amorphos.Text = row.Surname;
    
                   //search the two active weeks for that name and date
                   OverSugerydbaseDataSet.TwoActiveWeeksDataTable IDandSlots = this.twoActiveWeeksTableAdapter.SearchTwoActiveWeeksByIDandDate(row.StaffID, dateTimePickerAP.Value.Date.ToString());
                   //make new int array int[] Appointments= new int[13]
                    int[] Appointments= new int[26];
                   //int c=0;
                   foreach(OverSugerydbaseDataSet.TwoActiveWeeksRow row1 in IDandSlots)
                     {               
                        //store the timeslot x=row.slot Appointments[x]=row.id
                         Appointments[row1.TimeSlot] = row1.Id;           
                  
                      }//end of foreach in IDandDate

                   for (int c = 0; c < 26; c++)  //13 is max
                   {
                       
                       if (Appointments[c] == 0)  //if there is no appointment in that slot make a booking button else make a label "not available" (n/a)
                       {
                           //Make SlotButton(c,i)
                          
                           BookButton[c, i] = new Button();
                           BookButton[c, i].Text = "Book";
                           BookButton[c, i].AutoSize = true;
                           BookButton[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                           BookButton[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                           BookButton[c, i].Click += new System.EventHandler(BookButton_Click);
                           if(c<13) //store in the morning table
                           try
                           {
                               TableMorning.Controls.Add(BookButton[c, i], (c + 1), (i + 1));
                           }
                           catch (Exception bs)
                           {
                               MessageBox.Show(bs.Message);
                           }
                           else  //store in the afternoun table
                               try
                               {
                                   TableAfternoun.Controls.Add(BookButton[c, i], (c -12), (i +1));
                               }
                               catch (Exception bs)
                               {
                                   MessageBox.Show(bs.Message);
                                   //MessageBox.Show(String.Format("in button {0}", c.ToString()));
                               }

                       }
                       else
                       {
                           //  Make TimeLabel(c,i)
                           SlotLabel[c, i] = new Label();
                           SlotLabel[c, i].Text = "N/A";
                           SlotLabel[c, i].AutoSize = true;
                           SlotLabel[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                           SlotLabel[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                           try
                           {
                               TableMorning.Controls.Add(SlotLabel[c, i], (c + 1), (i + 1));
                           }
                           catch (Exception bs)
                           {
                               MessageBox.Show(bs.Message);
                              // MessageBox.Show("in label");
                           }
                       }
                   } //end of for c
                   i++;
           }  //end of foreach Stafffound


             
               TableMorning.Visible = true; //start with the morning Table
         
      }  //end of btnCheckAppointments_Click


        private void BookButton_Click(object sender, EventArgs e) //the code for the code generated "book" button
        {
            Label[,] SlotLabelS = new Label[13, 13];
            Button bt = sender as Button;
            var rowx = TableMorning.GetRow(bt); // get the row and column from where the button was created 
            var colx = TableMorning.GetColumn(bt);
            Control control = this.Controls.Find("labelStaff" + (rowx - 1), true).FirstOrDefault() as Label; //get the label that contains the staffs name

            //search if the user has already made a booking (max 1 per 2 weeks)
            

            Int32 count = (Int32)this.twoActiveWeeksTableAdapter.DoubleBooking(Utilities.ActiveUserID);
            if ((Int32)this.twoActiveWeeksTableAdapter.DoubleBooking(Utilities.ActiveUserID) == 0 || ChangingAppointment == true)
            {

                //remove previous booking
                if (ChangingAppointment == true)
                    this.twoActiveWeeksTableAdapter.DeleteBooking(Utilities.ActiveUserID);

                MessageBox.Show("Your booking has been recorded.");

                OverSugerydbaseDataSet.StaffDataTable StaffInfo = this.staffTableAdapter.SearchStaffbySurname(control.Text);  //search by name and date
             
                object a = StaffInfo.Rows[0]["StaffID"];
                int TempStaffID = Convert.ToInt32(a);   //store the id
                object b = StaffInfo.Rows[0]["Surname"];
                string TempSurname = Convert.ToString(b);  //store the Surname
                object c = StaffInfo.Rows[0]["Sex"];
                string TempSex = Convert.ToString(c);  //store the Sex
                object d = StaffInfo.Rows[0]["Staff Role/Title"];
                string TempRole = Convert.ToString(d);  //store the Surname
                string TempDate = dateTimePickerRota.Value.Date.ToString();

                this.twoActiveWeeksTableAdapter.RecordBooking(TempStaffID, TempSurname, TempDate, TempSex, TempRole, colx, Utilities.ActiveUserID);
                this.TableMorning.Controls.Remove(bt);  //remove the button we pressed

                // Delete all bookings older than today, used to keep the Two active WeeksDatabase small in size.
                this.twoActiveWeeksTableAdapter.DeleteOldBookings();// (DateTime.Now.ToShortDateString());    
                this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);
               
              
                // make a label in its place
                SlotLabelS[rowx, colx] = new Label();
                SlotLabelS[rowx, colx].Text = "Booked";
                SlotLabelS[rowx, colx].AutoSize = true;
                SlotLabelS[rowx, colx].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                SlotLabelS[rowx, colx].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                 | System.Windows.Forms.AnchorStyles.Left)
                 | System.Windows.Forms.AnchorStyles.Right)));

                this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks);
                this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);

            }//end of if Doublebooking
            else
            {
                MessageBox.Show(String.Format(" Sorry you already have as booking {0}", count.ToString()));
            }

           
          

            if (TableMorning.Visible == true)
                TableMorning.Visible = false;
            else
                TableAfternoun.Visible = false;
                
            
           
            setMeVisible("PageViewCancelEditAppointment");
        }
       
        
        
        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            cbStaffMenu1.Enabled = true;
            cbStaffMenu2.Enabled = true;
            cbStaffMenu3.Enabled = true;
            cbStaffMenu4.Enabled = true;
            cbStaffMenu5.Enabled = true;

            Boolean IdExists = true;
            if (dateTimePickerRota.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePickerRota.Value.DayOfWeek == DayOfWeek.Sunday)  //do not allow weekeneds to be selected
                MessageBox.Show("Apologies you can not book on weekends.");
            else
            {

                tbBadBox.Text = dateTimePickerRota.Value.DayOfWeek.ToString() + " " + dateTimePickerRota.Value.Date.ToShortDateString();   //diplay the date
                //search for staff already working that week
                OverSugerydbaseDataSet.RotaDataTable Rotas = this.rotaTableAdapter.SearchStaffByDate(dateTimePickerRota.Value.Date.ToString());
                DestroyMenus();
                FillStaffMenu();       //fill the drop down menus
                try
                {
                    object a = Rotas.Rows[0]["Surname"];

                }
                catch (Exception)
                {
                    //MessageBox.Show("no one here before");
                    IdExists = false;
                }
                if (IdExists)
                {

                   // MessageBox.Show("found someone here already");
                    int numberofWorkingStaff = Rotas.Rows.Count;
                   // MessageBox.Show(String.Format(" there are {0} entries found", numberofWorkingStaff));
                    for (int i = 0; i < numberofWorkingStaff; i++)
                    {
                        var control = Controls.Find("cbStaffMenu" + (i + 1), true).FirstOrDefault();
                        PreSelectedValue = true;  //this is used so once the name is loaded it is not reset to "free" because I am setting a value that already is recorded (for display purposes) 
                        object a = Rotas.Rows[i]["Surname"];
                        control.Text = Convert.ToString(a);   ///cbStaffMenu1

                    }
                }
                else
                {
                    DestroyMenus();
                    FillStaffMenu();
                    
                }
                PageRota.Visible = true;


            }
        }


        private void cbStaffMenu1_SelectedIndexChanged(object sender, EventArgs e)   //when one of the five comboboxes value changes
        {
            Boolean IdExists = true;
            ComboBox CB = sender as ComboBox;



            if (CB.Text != "Free") //current entry a name
            {

                this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, dateTimePickerRota.Value.Date.ToString()); //delete old entry
                OverSugerydbaseDataSet.StaffDataTable StaffSearched = this.staffTableAdapter.SearchStaffbySurname(CB.Text);  // cbStaffMenu1 Search to find the
                // selected staff's other details

                object a = StaffSearched.Rows[0]["StaffID"];
                int TempStaffID = Convert.ToInt32(a);   //store the id
                object b = StaffSearched.Rows[0]["Surname"];
                string TempSurname = Convert.ToString(b);  //store the Surname
                object c = StaffSearched.Rows[0]["Sex"];
                string TempSex = Convert.ToString(c);  //store the Sex
                object d = StaffSearched.Rows[0]["Staff Role/Title"];
                string TempRole = Convert.ToString(d);  //store the Surname
                string TempDate = dateTimePickerRota.Value.Date.ToString();   //store the date


                //create a table from the rota that contaigns the same name and date. It means we have a dublicate entry and we do not want to copy it.
                OverSugerydbaseDataSet.RotaDataTable Rotas = this.rotaTableAdapter.SearchbyIDandDate(TempStaffID, dateTimePickerRota.Value.Date.ToString());
                try
                {
                    object t = Rotas.Rows[0]["StaffID"]; //If the table does not exist (the we are good to copy the entry) then it will create an error

                }
                catch (Exception)
                {
                    MessageBox.Show(String.Format("{0} Has been  added to this shift", TempSurname)); //all is good
                    IdExists = false; //so we set the ID exists to false so we can copy
                }

                if (IdExists) //if the id exists already 
                {
                   // MessageBox.Show("already added");  //just tell us its added and do nothing
                    if (PreSelectedValue == false)
                    {
                        CB.SelectedIndex = 0;
                    }
                    PreSelectedValue = false;
                }
                else
                {
                   
                    if (PreviousName != "Free" && PreviousName != String.Empty) //and  the previous value was not free
                    {
                        this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, TempDate);//delete old entry
                        MessageBox.Show(" Old entry Deleted");
                    }

                    this.rotaTableAdapter.AddRotaV3(TempStaffID, TempSurname, TempDate, TempSex, TempRole); // store the values
                    this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota); //reload for us to see.
                }
            }

            else //Current entry is set to free
            {
                // MessageBox.Show(String.Format("the value of Previousname is {0}", PreviousName));
                if (PreviousName != "Free" && PreviousName != String.Empty)
                    try
                    {
                        this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, dateTimePickerRota.Value.Date.ToString());  //delete old entry
                    }
                    catch (Exception gg)
                    {
                        MessageBox.Show(gg.Message, "message");
                    }
            }
            dateTimePickerRota.Focus();
            
        }


        private void ValueAboutToChange(object sender, EventArgs e)  //catch the previous value of the menu 
        {
            ComboBox CB = sender as ComboBox;
            PreviousName = CB.Text;

        }

        private void WhenTabSelected(object sender, EventArgs e)
        {
            TabControl Tab = sender as TabControl;
            if (tabControl1.SelectedTab.ToString() == "TabPage: {Timetable}")
            {
                dateTimePickerRota.Location = new Point(139, 91);
            }

        }



        private void btnBackfromAp_Click(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

        private void btnMorning_Click(object sender, EventArgs e)
        {
            TableMorning.Visible = true;
            TableAfternoun.Visible = false;
            btnAfternoon.Visible = true;
            btnMorning.Visible = false;
        }

        private void btnAfternoon_Click(object sender, EventArgs e)
        {
            TableMorning.Visible = false;
            TableAfternoun.Visible = true;
            btnAfternoon.Visible = false;
            btnMorning.Visible = true;
        }

        private void btnCancelAppointment_Click(object sender, EventArgs e) //canceling an Appointment
        {

            if (this.twoActiveWeeksTableAdapter.DoubleBooking(Utilities.ActiveUserID) <= 0) //if no booking made before
            {
                MessageBox.Show("There is no booking to Cancel");
            }
            else
            {

                if (MessageBox.Show("Are you sure you wish to delete this appointment", "Canceling an Appointment", MessageBoxButtons.YesNo) == DialogResult.Yes)  //get confirmation for deleting
                {
                    try
                    {
                        this.twoActiveWeeksTableAdapter.DeleteBooking(Utilities.ActiveUserID);
                        MessageBox.Show("Your Booking has been Deleted");
                        labelAppointmentDetails.Text = String.Empty;
                        labelAppointmentDetails.Text = "No booking has been made";
                    }

                    catch (Exception NoEntry)
                    {
                        MessageBox.Show(NoEntry.Message);
                    }
                }
            }//end of else doublebooking

        }

        private void buttonBacktoSelectedPatient_Click(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

        private void Fill_Appointment_Label(object sender, EventArgs e)
        {
            if (this.twoActiveWeeksTableAdapter.DoubleBooking(Utilities.ActiveUserID) > 0)
            {
                OverSugerydbaseDataSet.TwoActiveWeeksDataTable StaffSearched = this.twoActiveWeeksTableAdapter.SearchbyId(Utilities.ActiveUserID);
                // selected staff's other details

                String Time;
                object a = StaffSearched.Rows[0]["Surname"];
                string Surname = Convert.ToString(a);  //store the Surname
                object b = StaffSearched.Rows[0]["Date"];
                DateTime Date = Convert.ToDateTime(b);  //store the Sex
                object c = StaffSearched.Rows[0]["TimeSlot"];
                Int32 TimeSlot = Convert.ToInt32(c);  //store the Surname
                switch (TimeSlot)
                {
                    case 1:
                        Time = "8:00";
                        break;
                    case 2:
                        Time = "8:20";
                        break;
                    case 3:
                        Time = "8:40";
                        break;
                    case 4:
                        Time = "9:00";
                        break;
                    case 5:
                        Time = "9:20";
                        break;
                    case 6:
                        Time = "9:40";
                        break;
                    case 7:
                        Time = "10:00";
                        break;
                    case 8:
                        Time = "10:20";
                        break;
                    case 9:
                        Time = "10:40";
                        break;
                    case 10:
                        Time = "11:00";
                        break;
                    case 11:
                        Time = "11:20";
                        break;
                    case 12:
                        Time = "11:40";
                        break;
                    case 13:
                        Time = "12:00";
                        break;
                    case 14:
                        Time = "12:20";
                        break;
                    case 15:
                        Time = "12:40";
                        break;
                    case 16:
                        Time = "13:00";
                        break;
                    case 17:
                        Time = "13:20";
                        break;
                    case 18:
                        Time = "13:40";
                        break;
                    case 19:
                        Time = "14:00";
                        break;
                    case 20:
                        Time = "14:20";
                        break;
                    case 21:
                        Time = "14:40";
                        break;
                    case 22:
                        Time = "15:00";
                        break;
                    case 23:
                        Time = "15:20";
                        break;
                    case 24:
                        Time = "15:40";
                        break;

                    default:
                        Time = "lol";
                        break;
                }

                labelAppointmentDetails.Text = String.Empty;
                labelAppointmentDetails.Text = Date.ToShortDateString() + " at " + Time + " with " + Surname;
            }//End of if Doublebooking
            else
            {
                labelAppointmentDetails.Text = String.Empty;
                labelAppointmentDetails.Text = "No booking has been made";
            }

        }

        private void btnChangeAppointment_Click(object sender, EventArgs e)
        {
            ChangingAppointment = true;
            setMeVisible("PageMakeAppointment");
        }


        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            //check fields in utility
            if (Utilities.CheckFieldsSearchByName(tbSearchName.Text, tbSearchPC.Text))
            {

                OverSugerydbaseDataSet.PatientsDataTable NameSearched = this.patientsTableAdapter.SearchByName(tbSearchName.Text, tbSearchPC.Text, dateTimePickerSearchDOB.Value.Date.ToShortDateString());  //database querry searches for rows containing the id (we know it is only one, if it exists)
                try //store the values from the sql search 
                {
                    object a = NameSearched.Rows[0]["Patient Name"]; //To get the data you store each value into an object
                    object b = NameSearched.Rows[0]["Id"];
                    setMeVisible("PageSelectedPatient"); // go to the Selected patient page.
                    tbSearchName.Text = string.Empty;//clear the search name, post code textboxes  for future use. 
                    tbSearchPC.Text = string.Empty;


                    txtActiveUserName.Text = Convert.ToString(a); //place the id and the name on the area above the option, so you always know the patient you are handeling
                    txtActiveUserID.Text = Convert.ToString(b);
                    btnClearActivePatient.Visible = true;  // now that there is an active patient, make the clear_active_patient button available
                    Utilities.ActiveUserID = Convert.ToInt32(txtActiveUserID.Text);  //a global variable to store the active patients id.
                }
                catch (Exception) //if there are no values (caused if the id is not found in the database) catch it
                {
                    MessageBox.Show("The details entered do not belong \n to a registered patient", "Patient not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }   

  //////////////////////////////////////////////////////////////////////////////////////////  
        //Toms stuff

        private void btnMedAdd_Click(object sender, EventArgs e)
        {

            OverSugerydbaseDataSet.MedicationRow newMedicationRow;
            newMedicationRow = overSugerydbaseDataSet.Medication.NewMedicationRow();
            newMedicationRow.MedName = txtMedName2.Text;
            newMedicationRow.Dose = txtMedDose2.Text;
            newMedicationRow.Start_Date = txtMedStart.Text;
            newMedicationRow.End_Date = txtMedEnd.Text;
            newMedicationRow.Prescribing_GP = txtMedGP.Text;
            newMedicationRow.PatientID = txtActiveUserID.Text;

            this.overSugerydbaseDataSet.Medication.Rows.Add(newMedicationRow);

            this.medicationTableAdapter.Update(this.overSugerydbaseDataSet.Medication);

        }

        private void btnMedSub_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.medicationBindingSource.EndEdit();
            this.medicationTableAdapter.Update(this.overSugerydbaseDataSet);
        }

        private void btnMedCancel_Click_1(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

        private void btnMedUP_Click_1(object sender, EventArgs e)
        {
            medicationBindingSource.MovePrevious();
        }

        private void btnMedDown_Click_1(object sender, EventArgs e)
        {
            medicationBindingSource.MoveNext();
        }

        private void btnMedEdit_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.medicationBindingSource.EndEdit();
            this.medicationTableAdapter.Update(this.overSugerydbaseDataSet);
        }

        private void btnMedDelete_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.medicationBindingSource.RemoveCurrent();
            this.medicationTableAdapter.Update(this.overSugerydbaseDataSet);
        }




        private void btnVResCancel_Click(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

        private void btnResCancel_Click(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

        private void btnResSave_Click(object sender, EventArgs e)
        {
            OverSugerydbaseDataSet.ResultsRow newResultsRow;
            newResultsRow = overSugerydbaseDataSet.Results.NewResultsRow();
            newResultsRow.ResDate = txtResDate.Text;
            newResultsRow.ResType = txtResType.Text;
            newResultsRow.Results = txtResResults.Text;
            newResultsRow.ResGP = txtResGP.Text;
            newResultsRow.ResDetails = txtResDetails.Text;
            newResultsRow.ResPatientID = txtActiveUserID.Text;

            this.overSugerydbaseDataSet.Results.Rows.Add(newResultsRow);

            this.resultsTableAdapter.Update(this.overSugerydbaseDataSet.Results);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////             S T A F F   G P / N U R S E   S E C T I O N         /////////////////////////////////////////////////////////////////// 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        private void staffBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
           // Validate content of row/table to ensure that they are correct by data type and not null as the case may be, 
           // Table and database are updated ater validation 
            this.Validate();
            this.staffBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.overSugerydbaseDataSet);

        }

        // Button click for event section for Check staff duty button. COde here is executed when the Check Staff Duty button is clicked
        private void bntCheckStaffDuty_Click(object sender, EventArgs e)
        {
            
            // set group box that are not relevant to invisible and show the relevant group box for user to interact with
            // eg. Search groupBox, Staff Group box Add Staff Group Box and Delete staff Groupbox are not relevant when checking staff on duty 
            // hence the are made to be invisible
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = false;
           
           //label35 and label 36 show heading for ROTA table and All staff table respectively 
            label35.Visible = true;
            label36.Visible = true;
            
            // set the rota Datagrid table to be visible
            rotaDataGridView1.Visible = true;
        }
        
        // set group box that are not relevant to invisible and show the relevant group box for user to interact with
        // in this case Add new staff group box is made visible for user to interact with while others like delete staff, search staff
        // group boxes are set to invisible
        private void bntAddNewStaff_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = true;
            groupBoxDeleteStaff.Visible = false;

            // set the rota Datagrid table to be invisible as it is not relevant in this case
            label35.Visible = false;
            label36.Visible = false;
            rotaDataGridView1.Visible = false;
            comboBoxNewSex.SelectedIndex = 0;
            comboBoxNewRole.SelectedIndex = 0;

        }
        
        // Delete Staff button click event handler, When user click the delete staff button, the applicaction hide all irrelevant group box and items
        // and displays the necessary items on the window.in this case the delete staff group box is shown and the currently selected staff
        // group box is also shown on the window.
        private void bntDeleteStaff_Click(object sender, EventArgs e)
        {
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);
            label35.Visible = false;
            label36.Visible = false;
            rotaDataGridView1.Visible = false;

            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = true;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = true;
        }


        // Search Staff button click event handler, When user click the search staff button, 
        // Relevant irems are left on the window based on the button clicked
        // in this case seaerch and selected staff group box is shown
        private void btnSearchStaff_Click(object sender, EventArgs e)

        {
            groupBoxSearch.Visible = true;
            groupBoxDeleteStaff.Visible = false;
            groupBoxSearch.Visible = true;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;

            label35.Visible = false;
            label36.Visible = false;
            rotaDataGridView1.Visible = false;
            
            // combobox index is set to 0, to show in the combo box the default text "please select value"
            comboBoxSearchBy.SelectedIndex = 0;
        }

        // show all staff button click event handler, When user click the show all staff button, 
        // Relevant items are left on the window based on the button clicked
        private void btnShowAllStaff_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = true;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = false;

            label35.Visible = false;
            label36.Visible = false;
            rotaDataGridView1.Visible = false;

            // this line of code fetch and display the content of the table from the database and displays 
            // or populate it in the table on the application window 
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);   
        }
        
        
        // the search staff section 
        private void txtSearch_Click(object sender, EventArgs e)
        {
                
                //check to see if the search text or value is empty or not, if empty displays an error message, else if not empty check if the next value to be selected is empty
                if (txtSearchBy.Text == "" ) 
                { MessageBox.Show("You have Not entered any value to search for", "Error: No Value detected");
                }

                //check to see if the search criteria is choosen or not, if empty displays an error message, else it check the staff 
                //table based on the value selected and the search criteria choosen 
                if (comboBoxSearchBy.SelectedItem.ToString() == "")
                { MessageBox.Show("You have not selected a search criteria. Please use the drop down box to select a criteria", "Error: No Value Selected"); }
                else
                { 
                
                    string SearchByValue = comboBoxSearchBy.SelectedItem.ToString();

                    switch (SearchByValue)
                    {

                        
                    // search staff table by surname     
                    case "Surname":
                        try
                        {
                            this.staffTableAdapter.FillBySurname(this.overSugerydbaseDataSet.Staff, txtSearchBy.Text);
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        break;

                    // search staff table by First Name
                    case "First Name":
                        try
                        {
                            this.staffTableAdapter.FillByFName(this.overSugerydbaseDataSet.Staff, txtSearchBy.Text);
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                        break;

                    // search staff table by sex
                    case "Sex":
                        try
                        {
                            this.staffTableAdapter.FillBySex(this.overSugerydbaseDataSet.Staff, txtSearchBy.Text);
                        }
                        catch (System.Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }

                        break;

                    // Default do Nothing. Leave table as it is
                   default:

                       break;
                }
            }
        }
       
       
        private void btnAddNewStaff_Click(object sender, EventArgs e)
        {
            
            string new_Cnum, newSex, new_Role;
                       
            try
            {
                newSex = comboBoxNewSex.SelectedItem.ToString();
                new_Role = comboBoxNewRole.SelectedItem.ToString(); 
                new_Cnum = txtNewCNum.Text;
                staffTableAdapter.InsertQueryAddNewStaff(txtNewSurname.Text, txtNewFName.Text, newSex, new_Role, new_Cnum);


                MessageBox.Show("Success: Staff successfully added to Staff Table", "Success: New Staff Added. ");
                
                
                txtNewSurname.Text = String.Empty;
                txtNewFName.Text = String.Empty;
                comboBoxNewSex.SelectedIndex = 0;
                comboBoxNewRole.SelectedIndex = 0;
                txtNewCNum.Text = String.Empty;


            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
           
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);

            //saving the new row to the database
            this.staffTableAdapter.Update(this.overSugerydbaseDataSet.Staff);
            overSugerydbaseDataSet.Staff.AcceptChanges();
            overSugerydbaseDataSet.Staff.GetChanges();
            lblCount.Text = staffBindingSource.Count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNewSurname.Text = String.Empty;
            txtNewFName.Text = String.Empty;
            comboBoxNewSex.SelectedIndex = 0;
            comboBoxNewRole.SelectedIndex = 0;
            txtNewCNum.Text = String.Empty;
        }


        private void btnAddStaffCancel_Click(object sender, EventArgs e)
        {
            txtNewSurname.Text = String.Empty;
            txtNewFName.Text = String.Empty;
            comboBoxNewSex.SelectedIndex = 0;
            comboBoxNewRole.SelectedIndex = 0;
            txtNewCNum.Text = String.Empty;

            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = false;

            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);   //refresh, or refill the content of the table
        }




        private void btnRefreshStaffTable_Click(object sender, EventArgs e)
        {
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);   //refresh, or refill the content of the table
        }

        private void btnDeleteS_Click(object sender, EventArgs e)
        {
            lblCount.Text = staffBindingSource.Count.ToString();
            staffTableAdapter.DeleteQuery(Convert.ToInt32(staffIDTextBox.Text), surnameTextBox.Text, first_NameTextBox.Text, sexTextBox.Text, staff_Role_TitleTextBox.Text, contact_NumberTextBox.Text);


            this.Validate();
            this.staffBindingSource.EndEdit();
            this.staffTableAdapter.Update(this.overSugerydbaseDataSet.Staff);
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);
      
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);
            groupBoxSearch.Visible = false;
            groupBoxDeleteStaff.Visible = false;
            groupBoxStaff.Visible = true;
            groupBoxAddStaff.Visible = false; 
            btnUpdate.Visible = true;

            surnameTextBox.ReadOnly = false;
            first_NameTextBox.ReadOnly = false;
            sexTextBox.ReadOnly = false;
            staff_Role_TitleTextBox.ReadOnly = false;
            contact_NumberTextBox.ReadOnly = false;

            label35.Visible = false;
            label36.Visible = false;
            rotaDataGridView1.Visible = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        this.Validate();
        this.staffBindingSource.EndEdit();
        this.tableAdapterManager.UpdateAll(this.overSugerydbaseDataSet);

        lblCount.Text = staffBindingSource.Count.ToString();
        }

        private void PageGPNurse_Paint(object sender, PaintEventArgs e)
        {
            lblCount.Text = staffBindingSource.Count.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bindingNavigator1.Enabled = true;

            }
            else
            {
                bindingNavigator1.Enabled = false;
            }

        }

      

        }

     
      
}