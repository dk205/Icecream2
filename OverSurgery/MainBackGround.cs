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
        int ActiveUserID = 0;
        Form formParent = null;
        public MainBackGround(Form par)
        {
            formParent = par;
            InitializeComponent();

        }

        public string PreviousName="Free";
        public bool PreSelectedValue =false;

        void FillStaffMenu()
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
               // var OneofMany = Controls.Find(MenuName, true).FirstOrDefault();
             //   OneofMany.Refresh();
             
              cbStaffList.Items.Insert(i,row.Surname);
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


        void DestroyMenus()
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
        {                                           //It hides all existing pannels and then set to visible the panel whose name given in the function. The name must be inside brackets
            PageMainScreen.Visible = false;
            PageNewRegistration.Visible = false;
            PageSelectedPatient.Visible = false;
            PageEditPatientDetails.Visible = false;
            PageEnterTestResults.Visible = false;
            
            
            var control = Controls.Find(PanelName, true).FirstOrDefault();
            control.Refresh();
            control.Visible = true;

        }
         
        
        public void ClearNewRegistrationFields()  // small function to clear the fields in the new registration page.
        {                                          // its used when the registration is canceled and when the new registration is created.
        txtNRPatientsName.Text = String.Empty;
        txtNRDOB.Text = String.Empty;
        txtNRPC.Text = String.Empty;
        txtNRSex.Text = String.Empty;
        txtNRAddress1.Text = String.Empty;
        txtNRAddress2.Text = String.Empty;
        txtNRMobile.Text = String.Empty;
        txtNRLandLine.Text = String.Empty;
        txtNREmail.Text = String.Empty;
         
        }

        private void button1_Click(object sender, EventArgs e) //this is the LOG OUT BUTTON
        {
            Application.Exit();

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewRegistrationPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NewRegButton_Click(object sender, EventArgs e) //go to the new registration page
        {

            setMeVisible("PageNewRegistration");

        }

      

     

        private void SearchIDButton_Click(object sender, EventArgs e) //user enters a ID number and presses the search 
        {

            OverSugerydbaseDataSet.PatientsDataTable IDSearched = this.patientsTableAdapter.SearchByID(Convert.ToInt32(txtBoxID.Text));  //database querry searches for rows containing the id (we know it is only one, if it exists)
            try //store the values from the sql search 
            {
                object a = IDSearched.Rows[0]["Patient Name"]; //To get the data you store each value into an object
                object b = IDSearched.Rows[0]["Id"];
                //String IDfound = Convert.ToString(b);  
               // MessageBox.Show(String.Format("the user was found and his id is {0}", IDfound));
                setMeVisible("PageSelectedPatient"); // go to the Selected patient page.
                txtBoxID.Text = string.Empty;   //clear the search id textbox for future use. 

                txtActiveUserName.Text = Convert.ToString(a); //place the id and the name on the area above the option, so you always know the patient you are handeling
                txtActiveUserID.Text = Convert.ToString(b);
                btnClearActivePatient.Visible = true;  // now that there is an active patient, make the clear_active_patient button available
                
            }
            catch (Exception) //if there are no values (caused if the id is not found in the database) catch it
            {
                MessageBox.Show("The Id entered does not belong \n to a registered patient", "Patient not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ActiveUserID = Convert.ToInt32(txtActiveUserID.Text );  //a global variable to store the active patients id.
            }
         }

       

      //  private void button9_Click(object sender, EventArgs e)
     //   {
     //       DDMenuStaff.Show(DDButtonDoctor, 0, DDButtonDoctor.Height);
     //   }

        private void button1_Click_1(object sender, EventArgs e) //takes you to the make appointment page
        {
            cbStaff.Items.Clear();
            this.PageMakeAppointment.Controls.Add(this.cbStaff);
            this.cbStaff.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbStaff.FormattingEnabled = true;
            this.cbStaff.Location = new System.Drawing.Point(343, 87);
            this.cbStaff.Name = "cbStaff";
            this.cbStaff.Size = new System.Drawing.Size(121, 21);
            this.cbStaff.TabIndex = 14;
            cbStaffList.Items.Insert(0, "Free");
              int i = 0;
            
            OverSugerydbaseDataSet.StaffDataTable StaffMenuTable = this.staffTableAdapter.GetData();
            foreach (OverSugerydbaseDataSet.StaffRow row in StaffMenuTable)
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
        }

        private void button4_Click(object sender, EventArgs e)   //takes you to view the Test Results
        {
            setMeVisible("PageViewPrintTestResults");
        }


       

       

        private void MainBackGround_Load(object sender, EventArgs e)
        {
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
            this.week52TableAdapter.Fill(this.overSugerydbaseDataSet.Week52);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Patients' table. You can move, or remove it, as needed.
            this.patientsTableAdapter.Fill(this.overSugerydbaseDataSet.Patients);
            // TODO: This line of code loads data into the 'overSugerydbaseDataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);

        }

       
        private void CreateReg_Click(object sender, EventArgs e)
        {
            OverSurgery.Utility Utilities = new Utility(); //creat a link for the class utility

            if (Utilities.CheckPatientFields(txtNRPatientsName.Text,txtNRDOB.Text,txtNRPC.Text,txtNRAddress1.Text)) //the function in the utility class checks if the 4 main details have been entered.
            {
                this.patientsTableAdapter.InsertNewRegistration(txtNRPatientsName.Text, txtNRDOB.Text, txtNRSex.Text, txtNRPC.Text, txtNRAddress1.Text, txtNRAddress2.Text, txtNRMobile.Text, txtNRLandLine.Text, txtNREmail.Text); //store the data from the textboxes to the database
                this.patientsTableAdapter.Fill(this.overSugerydbaseDataSet.Patients); //*TEMP this will be removed after testing

                OverSugerydbaseDataSet.PatientsDataTable Monkey = this.patientsTableAdapter.GetActivePatientQuery(); //creates a table with 1 row which is the last row just entered (our active patient)
                
                //code to view and store the active patient, it will becaume a  function cause it is used in the search id.
                object a= Monkey.Rows[0]["Patient Name"];
                object b = Monkey.Rows[0]["Id"];
                txtActiveUserName.Text = Convert.ToString(a);
                txtActiveUserID.Text = Convert.ToString(b);
                ActiveUserID = Convert.ToInt32(b);
                setMeVisible("PageSelectedPatient");
                btnClearActivePatient.Visible = true;
                
                ClearNewRegistrationFields();  //clear the fields for next new registration

            }



        }

        private void CheckLetterField(object sender, EventArgs e)  //WIP checks if the txtboxPatientName contains only letter *it must also allow spaces.
        {
            string testString = txtNRPatientsName.Text;
           
         
                for (int i = 0; i < testString.Length; i++)
                {
                    if (!char.IsLetter(testString[i]))
                    {
                        MessageBox.Show("A name can contaign only letters");
                        txtNRPatientsName.Text = "";
                        return;
                    }
                }
            }
        

        private void CheckIfDate(object sender, EventArgs e) //WIP should only allow letters and add "/" after the first 2 and 4 digits
        {
         //   MessageBox.Show("in to work");
            
            int CursorPosition = txtNRDOB.SelectionStart;
         //   MessageBox.Show(String.Format("the position is {0}", CursorPosition)); 
            string testString = txtNRDOB.Text;
            StringBuilder strB = new StringBuilder(txtNRDOB.Text);




            if (CursorPosition == 2 || CursorPosition == 5)
            {
                strB.Append("/");
               //strB.Length;
            }
       //         CursorPosition = CursorPosition + 1;
       //     txtNRDOB.SelectionStart = CursorPosition;
          
         //   if (!char.IsNumber(strB[CursorPosition-1]))
         //          strB.Remove(CursorPosition-1,1);
           
                //StringBuilder strB = new StringBuilder(testString);
                
          //  strB[] = 'D';
                txtNRDOB.Text =strB.ToString();
                CursorPosition = CursorPosition + 1;
                txtNRDOB.SelectionStart = CursorPosition;
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
          
            btnClearActivePatient.Visible = false; //hide the button
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)  //replaces the values of the database with the ones entered/altered in the text boxes.  
        {
            this.patientsTableAdapter.UpdatePatientDetails(tbPatientNameEdit.Text, tbDOBEdit.Text, tbSexEdit.Text, tbPCEdit.Text, tbAddress1Edit.Text, tbAddress2Edit.Text, tbMobileEdit.Text, tbLandLineEdit.Text, tbEmailEdit.Text, Convert.ToInt32(tbIDEdit.Text), Convert.ToInt32(tbIDEdit.Text));

            setMeVisible("PageSelectedPatient"); 
        }

        private void button8_Click(object sender, EventArgs e) //not sure if needed
        {
            setMeVisible("PageSelectedPatient");
        }

        private void goToEditPatient_Click(object sender, EventArgs e) //takes you to edit patient details
        {                                                              //and displays the Patients current details in textboxes
            setMeVisible("PageEditPatientDetails");
            OverSugerydbaseDataSet.PatientsDataTable IDSearched = this.patientsTableAdapter.SearchByID(ActiveUserID); //store the row of our active patient into the ActiveID table

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
            
            
            
            //declare the table StaffFound
            OverSugerydbaseDataSet.RotaDataTable StaffFound = new OverSugerydbaseDataSet.RotaDataTable();
          
            switch (cbStaff.SelectedIndex)
            {
                case 0:
                    
                        MessageBox.Show("Selected all staff");
                        //make a table  with all doctors that day.
                        
                        StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());
                        break;
                    
                case 1:
                    MessageBox.Show("All GP");
                    //make a table with only staff role=GP
                //  StaffFound= new OverSugerydbaseDataSet.RotaDataTable();
                  StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());
            break;
                case 2:
                    MessageBox.Show(" selected all Nurses");
                    //make a table with only staff role=Nurse
              // StaffFound= new OverSugerydbaseDataSet.RotaDataTable();
                  StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());
                    break;
                case 3:
                    MessageBox.Show(" selected all male gp");
                    //make a table with only staff role=GP nad sex =Male
                 // StaffFound= new OverSugerydbaseDataSet.RotaDataTable();
                  StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());                    break;
                case 4:
                    MessageBox.Show(" selected all female gp");
                    //make a table with only staff role=GP and Sex =Female
              //      OverSugerydbaseDataSet.RotaDataTable StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());
                    break;
                default:
                     MessageBox.Show(" somedoctor");
                    //make a table with the doctors name that date
                     StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());    
                    break;
            }

            Label[,] SlotLabel = new Label[13, 13];
           Button[,] BookButton = new Button[13, 13];
           this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks); //TEMPORARY
           this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);           //TEMPORARY

          
               string StaffID = string.Empty;
              
           
               int i = 0;
               //do as many as the staff is in that table
               foreach (OverSugerydbaseDataSet.RotaRow row in StaffFound)
               {
                   MessageBox.Show((String.Format("number i= {0}", i.ToString())));
                   //create the names in the labels
                   Label Amorphos = this.Controls.Find("labelStaff" + i, true).FirstOrDefault() as Label ;
                    Amorphos.Refresh();
                    MessageBox.Show("in the Foreach rota row");
                    Amorphos.Text = row.Surname;
    
                   //search the two active weeks for that name and date
                   OverSugerydbaseDataSet.TwoActiveWeeksDataTable IDandSlots = this.twoActiveWeeksTableAdapter.SearchTwoActiveWeeksByIDandDate(row.StaffID, dateTimePicker1.Value.Date.ToString());
                   //make new int array int[] Appointments= new int[13]
                    int[] Appointments= new int[26];
                   //int c=0;
                   foreach(OverSugerydbaseDataSet.TwoActiveWeeksRow row1 in IDandSlots)
                     {
                         MessageBox.Show("in the Foreach Two active weeks  row");
                       //store the timeslot x=row.slot Appointments[x]=row.id
                        Appointments[row1.TimeSlot]=row1.Id;
                        
                   //i++;
               }//end of foreach in IDandDate

                   for (int c = 0; c < 13; c++)  //13 is max
                   {
                       //      MessageBox.Show(String.Format("Appointments Value={0}", Appointments[c].ToString()));
                       if (Appointments[c] == 0)
                       {
                           //Make SlotButton(c,i)
                           MessageBox.Show(" i would if i could make a button");
                           BookButton[c, i] = new Button();
                           BookButton[c, i].Text = "Book";
                           BookButton[c, i].AutoSize = true;
                           BookButton[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                           BookButton[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                           BookButton[c, i].Click += new System.EventHandler(BookButton_Click);
                           try
                           {
                               TimetableM.Controls.Add(BookButton[c, i], (c + 1), (i + 1));
                           }
                           catch (Exception bs)
                           {
                               MessageBox.Show(bs.Message);
                           }
                       }
                       else
                       {
                           MessageBox.Show("going to make a label!");
                           //  ????Make TimeLabel(c,i)
                           SlotLabel[c, i] = new Label();
                           SlotLabel[c, i].Text = "N/A";
                           SlotLabel[c, i].AutoSize = true;
                           SlotLabel[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                           SlotLabel[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
                           try
                           {
                               TimetableM.Controls.Add(SlotLabel[c, i], (c + 1), (i + 1));
                           }
                           catch (Exception bs)
                           {
                               MessageBox.Show(bs.Message);
                           }
                       }
                   } //end of for c
                   i++;
           }  //end of foreach Stafffound
           
           
       
               TimetableM.Visible = true;
         
   



            /* Label[,] SlotLabel = new Label[13, 13];
            Button[,] BookButton = new Button[13, 13];
            this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks); //TEMPORARY
            this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);           //TEMPORARY

            if (RBAllDoctors.Checked)
            {
                string StaffID = string.Empty;
                MessageBox.Show("all doctors selected");
                //make a table  with all doctors that day.
                OverSugerydbaseDataSet.RotaDataTable StaffFound = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker1.Value.Date.ToString());
                int i = 0;
                //do as many as the staff is in that table
                foreach (OverSugerydbaseDataSet.RotaRow row in StaffFound)
                {
                    MessageBox.Show((String.Format("number i= {0}", i.ToString())));
                    //create the names in the labels
                    Label Amorphos = this.Controls.Find("labelStaff" + i, true).FirstOrDefault() as Label ;
                     Amorphos.Refresh();
                     MessageBox.Show("in the Foreach rota row");
                     Amorphos.Text = row.Surname;
    
                    //search the two active weeks for that name and date
                    OverSugerydbaseDataSet.TwoActiveWeeksDataTable IDandSlots = this.twoActiveWeeksTableAdapter.SearchTwoActiveWeeksByIDandDate(row.StaffID, dateTimePicker1.Value.Date.ToString());
                    //make new int array int[] Appointments= new int[13]
                     int[] Appointments= new int[26];
                    //int c=0;
                    foreach(OverSugerydbaseDataSet.TwoActiveWeeksRow row1 in IDandSlots)
                      {
                          MessageBox.Show("in the Foreach Two active weeks  row");
                        //store the timeslot x=row.slot Appointments[x]=row.id
                         Appointments[row1.TimeSlot]=row1.Id;
                        
                    //i++;
                }//end of foreach in IDandDate

                    for (int c = 0; c < 13; c++)  //13 is max
                    {
                        //      MessageBox.Show(String.Format("Appointments Value={0}", Appointments[c].ToString()));
                        if (Appointments[c] == 0)
                        {
                            //Make SlotButton(c,i)
                            MessageBox.Show(" i would if i could make a button");
                            BookButton[c, i] = new Button();
                            BookButton[c, i].Text = "Book";
                            BookButton[c, i].AutoSize = true;
                            BookButton[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            BookButton[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                            BookButton[c, i].Click += new System.EventHandler(BookButton_Click);
                            try
                            {
                                TimetableM.Controls.Add(BookButton[c, i], (c + 1), (i + 1));
                            }
                            catch (Exception bs)
                            {
                                MessageBox.Show(bs.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("going to make a label!");
                            //  ????Make TimeLabel(c,i)
                            SlotLabel[c, i] = new Label();
                            SlotLabel[c, i].Text = "N/A";
                            SlotLabel[c, i].AutoSize = true;
                            SlotLabel[c, i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                            SlotLabel[c, i].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                            try
                            {
                                TimetableM.Controls.Add(SlotLabel[c, i], (c + 1), (i + 1));
                            }
                            catch (Exception bs)
                            {
                                MessageBox.Show(bs.Message);
                            }
                        }
                    } //end of for c
                    i++;
            }  //end of foreach Stafffound
           
           
       
                TimetableM.Visible = true;
           }// end of if all doctors selected
     */    }


        private void BookButton_Click(object sender, EventArgs e)
        {
            Label[,] SlotLabelS = new Label[13, 13];
            Button bt = sender as Button;
            var rowx = TimetableM.GetRow(bt);
            var colx = TimetableM.GetColumn(bt);
           // Control control= this.Controls.Find("labelStaff["+0+"]["+rowx+"]", true).FirstOrDefault() as Label ;
            Control control = this.Controls.Find("labelStaff"+ (rowx-1)  , true).FirstOrDefault() as Label;

           OverSugerydbaseDataSet.StaffDataTable StaffInfo= this.staffTableAdapter.SearchStaffbySurname(control.Text);  //search by name and date
           // MessageBox.Show(String.Format("the row is:{0},  the column:{1}", rowx.ToString(), colx.ToString()));
         //  MessageBox.Show(String.Format("the label name is : {0}", control.Name));
            object a = StaffInfo.Rows[0]["StaffID"];
            int TempStaffID = Convert.ToInt32(a);   //store the id
            object b = StaffInfo.Rows[0]["Surname"];
            string TempSurname = Convert.ToString(b);  //store the Surname
            object c = StaffInfo.Rows[0]["Sex"];
            string TempSex = Convert.ToString(c);  //store the Sex
            object d = StaffInfo.Rows[0]["Staff Role/Title"];
            string TempRole = Convert.ToString(d);  //store the Surname
            string TempDate = dateTimePicker1.Value.Date.ToString();

            this.twoActiveWeeksTableAdapter.RecordBooking(TempStaffID, TempSurname, TempDate, TempSex, TempRole, colx, ActiveUserID);
            //this.Controls.Remove(bt);
            this.TimetableM.Controls.Remove(bt);
            
            // make a label
            SlotLabelS[rowx, colx] = new Label();
            SlotLabelS[rowx, colx].Text = "Booked";
            SlotLabelS[rowx, colx].AutoSize = true;
            SlotLabelS[rowx, colx].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            SlotLabelS[rowx, colx].Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.twoActiveWeeksTableAdapter.Fill(this.overSugerydbaseDataSet.TwoActiveWeeks);
            this.rotaTableAdapter.Fill(this.overSugerydbaseDataSet.Rota);
        
        }
        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            cbStaffMenu1.Enabled = true;
            cbStaffMenu2.Enabled = true;
            cbStaffMenu3.Enabled = true;
            cbStaffMenu4.Enabled = true;
            cbStaffMenu5.Enabled = true;

            Boolean IdExists = true;
            if (dateTimePicker2.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePicker2.Value.DayOfWeek == DayOfWeek.Sunday)  //do not allow weekeneds to be selected
                MessageBox.Show("Apologies you can not book on weekends.");
            else
            {

                tbBadBox.Text = dateTimePicker2.Value.DayOfWeek.ToString() + " " + dateTimePicker2.Value.Date.ToShortDateString();   //diplay the date
                //search for staff already working that week
                OverSugerydbaseDataSet.RotaDataTable Rotas = this.rotaTableAdapter.SearchStaffByDate(dateTimePicker2.Value.Date.ToString());
                DestroyMenus();
                FillStaffMenu();       //fill the drop down menus
                try
                {
                    object a = Rotas.Rows[0]["Surname"];

                }
                catch (Exception)
                {
                    MessageBox.Show("no one here before");
                    IdExists = false;
                }
                if (IdExists)
                {

                    MessageBox.Show("found someone here already");
                    int numberofWorkingStaff = Rotas.Rows.Count;
                    MessageBox.Show(String.Format(" there are {0} entries found", numberofWorkingStaff));
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
                    // cbStaffMenu1.SelectedIndex=3;
                }
                PageRota.Visible = true;


            }
        }


        private void cbStaffMenu1_SelectedIndexChanged(object sender, EventArgs e)   //when one of the five comboboxes value changes
        {
            Boolean IdExists = true;
            // var control = Controls.Find("cbStaffMenu" + (i + 1), true).FirstOrDefault();
            // control.Refresh();
            ComboBox CB = sender as ComboBox;



            if (CB.Text != "Free") //current entry a name
            {

                this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, dateTimePicker2.Value.Date.ToString()); //delete old entry
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
                string TempDate = dateTimePicker2.Value.Date.ToString();   //store the date


                //create a table from the rota that contaigns the same name and date. It means we have a dublicate entry and we do not want to copy it.
                OverSugerydbaseDataSet.RotaDataTable Rotas = this.rotaTableAdapter.SearchbyIDandDate(TempStaffID, dateTimePicker2.Value.Date.ToString());
                try
                {
                    object t = Rotas.Rows[0]["StaffID"]; //If the table does not exist (the we are good to copy the entry) then it will create an error

                }
                catch (Exception)
                {
                    MessageBox.Show(String.Format("{0} is going to be   added to this shift", TempSurname)); //all is good
                    IdExists = false; //so we set the ID exists to false so we can copy
                }

                if (IdExists) //if the id exists already 
                {
                    MessageBox.Show("already added");  //just tell us its added and do nothing
                    if (PreSelectedValue == false)
                    {
                        CB.SelectedIndex = 0;
                    }
                    PreSelectedValue = false;
                }
                else
                {
                    //MessageBox.Show("about to delete old entry2");
                    if (PreviousName != "Free" && PreviousName != String.Empty) //and  the previous value was not free
                    {
                        this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, TempDate);//delete old entry
                        MessageBox.Show(" old entry Deleted");
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
                        this.rotaTableAdapter.DeleteStaffFromRota(PreviousName, dateTimePicker2.Value.Date.ToString());  //delete old entry
                    }
                    catch (Exception gg)
                    {
                        MessageBox.Show(gg.Message, "message");
                    }
            }
            dateTimePicker2.Focus();
        }


        private void ValueAboutToChange(object sender, EventArgs e)  //catch the previous value of the menu 
        {
            ComboBox CB = sender as ComboBox;
            PreviousName = CB.Text;

        }

        private void WhenTabSelected(object sender, EventArgs e)
        {
            TabControl Tab = sender as TabControl;
            // MessageBox.Show(String.Format(" the tabcontrol1.selectedTab is: {0}",tabControl1.SelectedTab.ToString()));
            if (tabControl1.SelectedTab.ToString() == "TabPage: {Timetable}")
            {
                dateTimePicker2.Location = new Point(139, 91);
            }

        }



      /*  private void LoadComboboxStaff(object sender, EventArgs e)
        {
            int i = 0;
            
            OverSugerydbaseDataSet.StaffDataTable StaffMenuTable = this.staffTableAdapter.GetData();
            foreach (OverSugerydbaseDataSet.StaffRow row in StaffMenuTable)
            {
                // var OneofMany = Controls.Find(MenuName, true).FirstOrDefault();
                //   OneofMany.Refresh();

                cbStaff.Items.Insert(i, row.Surname);
                
                i++;

            }
            cbStaff.Items.Insert(0, "Any GP/Nurse");
            cbStaff.Items.Insert(1, "Any GP");
            cbStaff.Items.Insert(2, "Any Nurse");
            cbStaff.Items.Insert(3, "Any Male GP");
            cbStaff.Items.Insert(4, "Any Female GP");
            cbStaff.SelectedIndex = 0;
       }
*/ 
        //Toms stuff

        private void btnMedAdd_Click(object sender, EventArgs e)
        {
            DataRow newMedicationRow = overSugerydbaseDataSet.Tables["Medication"].NewRow();
            overSugerydbaseDataSet.Tables["Medication"].Rows.Add(newMedicationRow);
            medicationBindingSource.MoveLast();
            
        }

        private void btnMedSub_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.medicationBindingSource.EndEdit();
            this.medicationTableAdapter.Update(this.overSugerydbaseDataSet);
            /*
            this.medicationTableAdapter.update //(txtNRPatientsName.Text, txtNRDOB.Text, txtNRSex.Text, txtNRPC.Text, txtNRAddress1.Text, txtNRAddress2.Text, txtNRMobile.Text, txtNRLandLine.Text, txtNREmail.Text); //store the data from the textboxes to the database
            this.patientsTableAdapter.Fill(this.overSugerydbaseDataSet.Patients); //*TEMP this will be removed after testing

            OverSugerydbaseDataSet.PatientsDataTable Monkey = this.patientsTableAdapter.GetActivePatientQuery(); //creates a table with 1 row which is the last row just entered (our active patient)
             */ 
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////             S T A F F     S E C T I O N         /////////////////////////////////////////////////////////////////// 
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void staffBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.staffBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.overSugerydbaseDataSet);

        }

        private void staffBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.staffBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.overSugerydbaseDataSet);

        }


        private void bntCheckStaffDuty_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = false;
        }

        private void bntAddNewStaff_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = true;
            groupBoxDeleteStaff.Visible = false;

            /*txtNewStaffID.Text = "Generated automatically";
            txtNewSurname.Text = "";
            txtContactNum.Text = "";
            txtNewFName.Text = "";
            //txtNew */



        }
        private void bntDeleteStaff_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = true;
        }
        private void btnSearchStaff_Click(object sender, EventArgs e) ///comment
        {
            groupBoxSearch.Visible = true;
            groupBoxDeleteStaff.Visible = false;
            groupBoxSearch.Visible = true;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            
        }
        private void btnShowAllStaff_Click(object sender, EventArgs e)
        {
            groupBoxSearch.Visible = false;
            groupBoxStaff.Visible = false;
            groupBoxAddStaff.Visible = false;
            groupBoxDeleteStaff.Visible = false;
                      
            this.staffTableAdapter.Fill(this.overSugerydbaseDataSet.Staff);   //refresh, or refill the content of the table
        }
 private void txtSearch_Click(object sender, EventArgs e)
        {
            string SearchByValue = comboBoxSearchBy.SelectedItem.ToString();

            switch (SearchByValue)
            {

                case "Staff ID":

                    break;


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


                case "First Name":

                    break;


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


                case "Role/Title":


                    break;

                default:


                    break;
            }
        }
       

 private void btnSerchByID_Click(object sender, EventArgs e)
        {

        }

       















        

        private void btnAddNewStaff_Click(object sender, EventArgs e)
        {
            decimal new_Cnum;
            string newSex, new_Role;
            /*             
            staffTableAdapter.RegisterPatient(firstname_txt.Text, lastname_txt.Text, dateofbirth_txt.Text, address_txt.Text, email_txt.Text);
          
　
           
           
             */
            
            newSex = comboBoxNewSex.SelectedItem.ToString();
            new_Role = comboBoxNewRole.SelectedItem.ToString();

            try
            {
                 new_Cnum = Convert.ToDecimal(txtNewCNum.Text);
                 staffTableAdapter.InsertQueryAddNewStaff(txtNewSurname.Text, txtNewFName.Text, newSex, new_Role, new_Cnum);
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

        private void btnBackfromAp_Click(object sender, EventArgs e)
        {
            setMeVisible("PageSelectedPatient");
        }

     

       

      

       
        

     

    }
}